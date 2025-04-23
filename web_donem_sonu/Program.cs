using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Data;
using WebApi.Models.Entities; // <-- Role ve User için gerekli
using BCrypt.Net;

var builder = WebApplication.CreateBuilder(args);

// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Events = new JwtBearerEvents
		{
			OnMessageReceived = context =>
			{
				if (context.Request.Cookies.ContainsKey("access_token"))
					context.Token = context.Request.Cookies["access_token"];
				return Task.CompletedTask;
			}
		};

		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//  Admin kullanýcýyý seed et
using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

	var adminRole = context.Roles.FirstOrDefault(r => r.RoleName == "Admin");
	if (adminRole == null)
	{
		adminRole = new Role { RoleName = "Admin" };
		context.Roles.Add(adminRole);
		context.SaveChanges();
	}

	var adminEmail = "admin@example.com";
	if (!context.Users.Any(u => u.Email == adminEmail))
	{
		var adminUser = new User
		{
			FullName = "Sistem Admini",
			Email = adminEmail,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
			RoleId = adminRole.RoleId
		};

		context.Users.Add(adminUser);
		context.SaveChanges();
	}
}

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

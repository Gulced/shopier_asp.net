using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Events = new JwtBearerEvents
		{
			OnMessageReceived = context =>
			{
				//  Cookie'den token çek
				if (context.Request.Cookies.ContainsKey("access_token"))
				{
					context.Token = context.Request.Cookies["access_token"];
				}
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

//  3. Authorization
builder.Services.AddAuthorization();

builder.Services.AddHttpClient("WebApi", client =>
{
	client.BaseAddress = new Uri("https://localhost:5001/"); // WebApi'nin adresi
});


//  4. MVC Controller+Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

//  5. Middleware pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // <-- static dosyalar (wwwroot için)

app.UseRouting();

app.UseAuthentication(); //  Authentication middleware EKLENDÝ!
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

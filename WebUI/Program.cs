var builder = WebApplication.CreateBuilder(args);

//  MVC
builder.Services.AddControllersWithViews();

// HttpClient - WebApi ile konuþmak için
builder.Services.AddHttpClient("WebApi", client =>
{
	client.BaseAddress = new Uri("https://localhost:5001/"); // WebAPI URL'ini buraya yaz
});

//  Cookie tabanlý kimlik doðrulama
builder.Services.AddAuthentication("Cookies")
	.AddCookie("Cookies", options =>
	{
		options.LoginPath = "/Account/Login";
		options.AccessDeniedPath = "/Account/AccessDenied";
	});

//  Yetkilendirme
builder.Services.AddAuthorization();

var app = builder.Build();

//  Middleware pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //  Bu çok önemli
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

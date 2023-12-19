using FPTBookShop.DataAccess;
using FPTBookShop.DataAccess.Repository;
using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FPTBookShopWeb.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using FPTBookShop.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(c =>
{
    c.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(20);
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUnitOfWork,UnitOfWorks>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddRazorPages();


builder.Services.ConfigureApplicationCookie(option =>
{
	option.LoginPath = $"/Identity/Account/Login";
	option.LogoutPath = $"/Identity/Account/Logout";
	option.AccessDeniedPath = @"/Identity/Account/AccessDenied";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

using(var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var roles = new[] { "Admin", "StoreOwner", "Customer" };
	foreach(var role in roles)
	{
		if(! roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
		{
			roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
		}
	}
}
using (var scope = app.Services.CreateScope())
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
	string AD_email = "admin@email.com";
	string SO_email = "storeowner@email.com";
	string AD_pass = "!Admin123";
	string SO_pass = "!StoreOwner123";
    string FullName = "Sherlock Holmes";
    string Address = "221B Baker Street";
    if (userManager.FindByEmailAsync(AD_email).GetAwaiter().GetResult() == null)
	{
		var user = new ApplicationUser();
		user.Email = AD_email;
		user.UserName = AD_email;
        user.Full_Name = FullName;
        user.Address = Address;
        user.EmailConfirmed = true;
		userManager.CreateAsync(user, AD_pass).GetAwaiter().GetResult();
		userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
	}
	if (userManager.FindByEmailAsync(SO_email).GetAwaiter().GetResult() == null)
	{
		var user = new ApplicationUser();
		user.Email = SO_email;
		user.UserName = SO_email;
		user.Full_Name = FullName;
		user.Address = Address;
        user.EmailConfirmed = true;
		userManager.CreateAsync(user, SO_pass).GetAwaiter().GetResult();
		userManager.AddToRoleAsync(user, "StoreOwner").GetAwaiter().GetResult();
	}
}

app.Run();

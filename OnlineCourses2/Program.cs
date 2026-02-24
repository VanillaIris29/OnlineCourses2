using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCourses2.Data;
using OnlineCourses2.Data.Seeders;
using OnlineCourses2.Models;



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
    await RoleSeeder.SeedAdminAsync(userManager, roleManager);
    await RoleSeeder.SeedOrganizerAsync(userManager, roleManager);
    await RoleSeeder.SeedUsersAsync(userManager);

    CategorySeeder.SeedCategories(context);
    CourseSeeder.SeedCourses(context);

}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Course}/{action=All}/{id?}");
app.MapRazorPages();

app.Run();


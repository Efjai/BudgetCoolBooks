using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Genres;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>()
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<BookServices>();
builder.Services.AddScoped<AuthorServices>();
builder.Services.AddScoped<ReviewServices>();
builder.Services.AddScoped<GenreServices>();
builder.Services.AddScoped<UserServices>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<UserInitialisation>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    //Initialize admin-role for users with role "admin" on startup of application.
    using (var scope = app.Services.CreateScope()) 
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<UserInitialisation>();
        //await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
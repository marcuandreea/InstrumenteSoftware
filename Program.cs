using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc.Data;
using mvc.IRepository;
using mvc.Models;
using mvc.Repository;
using mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect to a custom Access Denied page
});

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEspressoareRepository, EspressoareRepository>();
builder.Services.AddScoped<IEspressoareService, EspressoareService>();
builder.Services.AddScoped<ITipuriCafeaRepository, TipuriCafeaRepository>();
builder.Services.AddScoped<ITipuriCafeaService, TipuriCafeaService>();
builder.Services.AddScoped<ICosCumparaturiRepository, CosCumparaturiRepository>();
builder.Services.AddScoped<ICosCumparaturiService, CosCumparaturiService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure this comes before UseAuthorization
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "contact",
    pattern: "Contact/{action=Index}/{id?}",
    defaults: new { controller = "Contact" });


app.MapControllerRoute(
    name: "subscription",
    pattern: "Subscription/{action=Index}/{id?}",
    defaults: new { controller = "Subscription" });


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ensure roles exist
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await EnsureRolesExist(roleManager);
}

app.Run();

// Function to ensure roles exist
static async Task EnsureRolesExist(RoleManager<IdentityRole> roleManager)
{
    var roles = new[] { "Vizitator", "Abonat", "Admin" }; // Define your roles here
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
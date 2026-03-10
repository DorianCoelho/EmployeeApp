using EmployeApp;
using EmployeeApp.Infrastructure.Data;
using EmployeeApp.Infrastructure.Data.Seeders;
using EmployeeApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using NHibernate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
    options.LoginPath = "/Index";
    options.AccessDeniedPath = "/Error";
    options.Cookie.Name = "UserSessionCookie";
});

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddFluentValidators();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
var app = builder.Build();

// Crear/actualizar tablas en la BD al arrancar (desde los mappings de NHibernate)
using (var scope = app.Services.CreateScope())
{
    var sessionFactory = scope.ServiceProvider.GetRequiredService<ISessionFactory>();
    await DataSeeder.SeedAdminUser(sessionFactory);}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();
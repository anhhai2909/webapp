var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(
    name: "",
    pattern: "{controller=Student}/{action=}/"
    );

app.Run();

using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Добавьте строку подключения из appsettings.json
builder.Services.AddDbContext<UtulekDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"),
        new MySqlServerVersion(new Version(9, 1, 0)))); // Убедитесь, что версия соответствует вашей MySQL

// Добавьте службы контроллеров с представлениями
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Настройка HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Настройка маршрутов
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

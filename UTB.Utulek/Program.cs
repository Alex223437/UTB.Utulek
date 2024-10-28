using Microsoft.EntityFrameworkCore;
using UTB.Utulek.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// �������� ������ ����������� �� appsettings.json
builder.Services.AddDbContext<UtulekDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"),
        new MySqlServerVersion(new Version(9, 1, 0)))); // ���������, ��� ������ ������������� ����� MySQL

// �������� ������ ������������ � ���������������
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ��������� HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ��������� ���������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

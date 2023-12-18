using Core.Entities;
using Core.Intrefaces;
using Infrastructue.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//Так як ми всередині самого Program.cs, то для того, щоб доступитися до наших сервісів
//ми використовуємо дану конструкцію. І фактично вона робить очевидні речі: створює групку сервісів,
//які є Scope type, і ми тут використовуємо using, для того, щоб виконувалося автоматичне вивільнення (IDisposable)
//наших сервісів з пам'яті після завершення використання (там трохи ще не докінця зрозуміло, де воно завершується, але
//це не є важливо.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

// Майже те саме, що і dependency injection. Просто використовуємо класи з потрібних нам сервісів
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();

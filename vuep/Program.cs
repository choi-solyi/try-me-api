//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//       new WeatherForecast
//       (
//           DateTime.Now.AddDays(index),
//           Random.Shared.Next(-20, 55),
//           summaries[Random.Shared.Next(summaries.Length)]
//       ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

//app.Run();

//internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("NLDB3Connection");
//builder.Services.AddDbContext<TestDb>(opt => opt.UseInMemoryDatabase("TestList"));
//builder.Services.AddDbContext<DbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<TestDb>(c =>
    c.UseSqlServer(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/getUser", async (TestDb db) => await db.pd.ToListAsync());
app.MapGet("/test",  (TestDb db) =>  db.tests.ToList());
////app.MapGet("/test", async (TestDb db) => await db.tt.DefaultIfEmpty().ToListAsync());
app.Run();

public class Pd
{
    public int Id { get; set; }
    public string? Name { get; set; }
}
public class Tests
{
    public int Id { get; set; }
    public string? Subject { get; set; }
}
//public class Tests
//{
//    int Id { get; set; }
//    string? Subject { get; set; }
//    int Pd { get; set; }
//    int Answer_type { get; set; }   
//    int Count { get; set; }
//    //DateTime created { get; set; }
//    //DateTime updated { get; set; }

//    //public virtual Pd Pdpdpd { get; set; }
//}
public class  question
{
    int test_id { get; set; }
    int q_id { get; set; }  
    int q_title { get; set; }   
}
public class TestDb : DbContext
{
    public TestDb(DbContextOptions<TestDb> options)
        : base(options) { }
    public DbSet<Pd> pd => Set<Pd>();
    public DbSet<Tests> tests => Set<Tests>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Pd>(e => e.HasNoKey());
        modelBuilder.Entity<Tests>(e => e.HasNoKey());
    }
}
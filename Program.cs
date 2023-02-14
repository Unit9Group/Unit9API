using AutoApi.Models;
using AutoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AutoDatabaseSettings>(builder.Configuration.GetSection("VehiclesDatabase"));

builder.Services.Configure<AutoDatabaseSettings>(builder.Configuration.GetSection(nameof(AutoDatabaseSettings.DatabaseName)));
builder.Services.AddSingleton<CarService>();
builder.Services.AddSingleton<MotorcycleService>();
builder.Services.AddSingleton<TruckService>();

builder.Services.AddControllers()
     .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.WebHost.UseKestrel(serverOptions =>
{
    //serverOptions.ListenAnyIP(7273);
    serverOptions.ListenAnyIP(7272, listenOptions => listenOptions.UseHttps());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
   // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

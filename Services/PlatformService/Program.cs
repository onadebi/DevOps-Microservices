using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

if (builder.Build().Environment.IsProduction())
{
    System.Console.WriteLine("--> Using Sql Server Db");
    builder.Services.AddDbContext<AppDbContext>(options=> options.UseSqlServer(builder.Build().Configuration.GetConnectionString("PlatformsConn")));
}
else
{
    System.Console.WriteLine("--> Using InMem Db");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMem"));
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
Console.WriteLine($"--> CommandService Endpoint {app.Configuration["CommandService"]}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrebDb.PrepPopulation(app);

app.Run();

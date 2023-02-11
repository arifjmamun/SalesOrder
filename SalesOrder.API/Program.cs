using Microsoft.EntityFrameworkCore;
using SalesOrder.API.Helpers;
using SalesOrder.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterAutoMappingProfiles();
builder.Services.RegisterDependencies();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString(nameof(ApplicationDbContext)),
        x => x.MigrationsAssembly("SalesOrder.Data")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
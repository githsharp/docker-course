using Microsoft.EntityFrameworkCore;
using movieflix_api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MovieContext>(options =>
{
    // options.UseSqlServer("Data Source=localhost;Initial Catalog=FavoriteMovies;User Id=sa;Password=EWMnZS22!;Persist Security Info=False;Encrypt=False");
    // options.UseSqlServer("Data Source=host.docker.internal;Initial Catalog=FavoriteMovies;User Id=sa;Password=EWMnZS22!;Persist Security Info=False;Encrypt=False");
    options.UseSqlServer("Data Source=movieflix-server;Initial Catalog=FavoriteMovies;User Id=sa;Password=EWMnZS22!;Persist Security Info=False;Encrypt=False");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<MovieContext>();
await context.Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

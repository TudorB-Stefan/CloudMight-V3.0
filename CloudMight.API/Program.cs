using CloudMight.API.Data;
using CloudMight.API.Entities;
using CloudMight.API.Interfaces;
using CloudMight.API.Repositories;
using CloudMight.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IStorageService,StorageService>();
builder.Services.AddScoped<IMemberRepository,MemberRepository>();
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequiredUniqueChars = 2;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireDigit = true;
    opt.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));
app.UseHttpsRedirection();
app.MapControllers();
// app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> sources) =>
//     string.Join("\n", sources.SelectMany(s => s.Endpoints)
//         .Select(e => e.DisplayName)));

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

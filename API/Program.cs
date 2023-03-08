using API.Contexts;
using API_CodeFirst.Repositories;
using API_CodeFirst.Repositories.Data;
using API_CodeFirst.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IGeneralRepository<,>), typeof(GeneralRepository<,>));
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IProfilingRepository, ProfilingRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

//Configure DbContext to SQL Server
var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<MyContext>(o =>
{
    o.UseSqlServer(connectionString);
    //o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

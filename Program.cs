using HMCTSTaskManager.API.Data;
using Microsoft.EntityFrameworkCore;
using HMCTSTaskManager.API.Repositories;
using HMCTSTaskManager.API.Services;
using HMCTSTaskManager.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Register controllers

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//Enable API endpoint discovery for swagger
builder.Services.AddEndpointsApiExplorer();

//Configure swagger and enable annotations
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

//Register SQL Server database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

//Register repository for dependecy injection
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

//Register service layer for dependecy injection
builder.Services.AddScoped<ITaskService, TaskService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();

    app.UseSwaggerUI();
}

//Globel exception handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
//Serve default files (index.html)
app.UseDefaultFiles();

//Enable static files from wwwroot
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();

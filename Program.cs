using EngineMonitoring.Models;
using EngineMonitoring.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EngineMonitoring.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers(options =>
            {
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status401Unauthorized));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            }).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
    {
        options.AddPolicy(MyAllowSpecificOrigins,
            builder =>
                {
                    builder
                        .WithOrigins("*")
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH");
                });
    });
builder.Services.AddScoped<IOperationsRepository, OperationsRepository>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<ISendEmailService, SendEmailService>();
builder.Services.AddScoped<IEngineEventRepository, EngineEventRepository>();
builder.Services.AddScoped<IEngineEventService, EngineEventService>();
builder.Services.AddScoped<ISwitchedOnRepository, SwitchedOnRepository>();
builder.Services.AddScoped<ITakeOffRepository, TakeOffRepository>();
builder.Services.AddScoped<IClimbRepository, ClimbRepository>();
builder.Services.AddScoped<ICruiseRepository, CruiseRepository>();
builder.Services.AddScoped<IDeclineRepository, DeclineRepository>();
builder.Services.AddScoped<ICrewRepository, CrewRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"Templates")),
        RequestPath = new PathString("/Templates")
    });

app.UseAuthorization();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
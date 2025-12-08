using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using System;
using UtusGrpcService.GrpcServices;
using AutoMapper;
using Infrastructure.EntityFramework;
using MassTransit.RabbitMqTransport;
using MassTransit;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddGrpc();

//// AutoMapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

////// EF + репозитории
////builder.Services.AddDbContext<AppDbContext>(options =>
////    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//// MassTransit
//builder.Services.AddMassTransit(x =>
//{
//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host(builder.Configuration["RabbitMq:Host"], h =>
//        {
//            h.Username(builder.Configuration["RabbitMq:User"]);
//            h.Password(builder.Configuration["RabbitMq:Password"]);
//        });
//    });
//});

//// DatabaseContext
//builder.Services.AddDbContext<DatabaseContext>(options =>
//{
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

//// Repositories
//builder.Services.AddScoped<ICourseRepository, CourseRepository>();
//builder.Services.AddScoped<ILessonRepository, LessonRepository>();

//// UnitOfWork 
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//// Application services
//builder.Services.AddScoped<ICourseService, CourseService>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//app.MapGrpcService<CourseGrpcController>();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//app.Run();

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// 1. gRPC
// -----------------------------
builder.Services.AddGrpc();

// -----------------------------
// 2. AutoMapper
// -----------------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// -----------------------------
// 3. Entity Framework Core
// -----------------------------
// Используем InMemory DB, чтобы не нужна была БД на этом этапе.
// Можно заменить на UseNpgsql(...) позже.
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseInMemoryDatabase("TestDb");
});

// -----------------------------
// 4. Репозитории и UnitOfWork
// -----------------------------
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// -----------------------------
// 5. Сервисы (бизнес-логика)
// -----------------------------
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

// -----------------------------
// 6. Маршрутизация gRPC
// -----------------------------
app.MapGrpcService<CourseGrpcController>();

app.MapGet("/", () =>
    "This gRPC server only accepts calls via gRPC clients.");

app.Run();
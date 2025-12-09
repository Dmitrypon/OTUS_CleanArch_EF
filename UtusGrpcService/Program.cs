using AutoMapper;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using System;
using UtusGrpcService.GrpcServices;
using UtusGrpcService.Hubs;


var builder = WebApplication.CreateBuilder(args);

// gRPC
builder.Services.AddGrpc();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Entity Framework Core
// Используем InMemory DB, чтобы не нужна была БД.
// Можно заменить на UseNpgsql(...) позже).
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseInMemoryDatabase("TestDb");
});

// Репозитории и UnitOfWork
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Сервисы (бизнес-логика)
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

// Регистрация SignalR
builder.Services.AddSignalR();

// Маршрутизация хаба
app.MapHub<NotificationsHub>("/notificationsHub");

// Маршрутизация gRPC
app.MapGrpcService<CourseGrpcController>();

app.MapGet("/", () =>
    "gRPC + SignalR service ready!");

app.Run();
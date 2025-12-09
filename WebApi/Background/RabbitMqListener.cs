using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Hubs;
using WebApi.Models.Course;

namespace WebApi.Background
{
    public class RabbitMqListener : BackgroundService
    {
        private readonly IHubContext<NotificationsHub> _hub;
        private readonly ConnectionFactory _factory;

        public RabbitMqListener(IHubContext<NotificationsHub> hub)
        {
            _hub = hub;
            _factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("course-created", false, false, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (_, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = System.Text.Json.JsonSerializer.Deserialize<CourseCreatedEvent>(json);

                // отправляем всем клиентам SignalR
                if (message != null)
                {
                    await _hub.Clients.All.SendAsync("courseCreated", message);
                }
            };

            channel.BasicConsume("course-created", true, consumer);

            return Task.CompletedTask;
        }
    }
}
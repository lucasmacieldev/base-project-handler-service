using Application.Client.Commands.CreateClient;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Create.Client.Processor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var tarefa = new CreateClientCommandRequest();
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "minhafila1",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);
                            tarefa = JsonSerializer.Deserialize<CreateClientCommandRequest>(message);

                            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                            Console.WriteLine("========================================================");
                            Console.WriteLine($"Nome:{tarefa.Nome}, type:{tarefa.Type}, ");
                            Console.WriteLine("========================================================\n");
                        }
                        catch (Exception e)
                        {
                            channel.BasicNack(deliveryTag: ea.DeliveryTag, false, true);
                            throw e;
                        }
                    };

                    channel.BasicConsume(queue: "minhafila1",
                                         autoAck: false,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
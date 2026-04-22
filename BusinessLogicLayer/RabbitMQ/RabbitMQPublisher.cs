using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BusinessLogicLayer.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly ConnectionFactory _connectionFactory;
    private IChannel? _channel;
    private IConnection? _connection;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        _configuration = configuration;

        string hostName = _configuration["RABBITMQ_HOST"]!;
        string userName = _configuration["RABBITMQ_USER"]!;
        string password = _configuration["RABBITMQ_PASSWORD"]!;
        string port = _configuration["RABBITMQ_PORT"]!;

        _connectionFactory = new()
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
            Port = Convert.ToInt32(port)
        };
    }

    public async Task InitializeConnection() 
    {
        _connection = await _connectionFactory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
    }

    public async Task Publish<T>(string routingKey, T message)
    {
        if(_channel == null) await InitializeConnection();

        if (_channel is not null)
        {
            string messageJson = JsonSerializer.Serialize(message);
            byte[] messageBodyInBytes = Encoding.UTF8.GetBytes(messageJson);

            // Create exchange if it doesn't exist
            string exchangeName = _configuration["RABBITMQ_PRODUCTS_EXCHANGE"]!;
            await _channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct, durable: true);

            // Publish the message to the exchange with the specified routing key
            await _channel.BasicPublishAsync(exchange: exchangeName, routingKey: routingKey, body: messageBodyInBytes);
        }
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}

// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Runtime.InteropServices;
using System.Text;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://zrdbzcyq:VAy_ksplaQG0kI-pza5_AzM_k-so7Tcz@possum.lmq.cloudamqp.com/zrdbzcyq");

var connection = factory.CreateConnection();

var channel = connection.CreateModel();


channel.ExchangeDeclare("fanout-type", ExchangeType.Fanout, true, false, null);

//channel.QueueDeclare("myqueue", true, false, false, null);

Enumerable.Range(1, 50).ToList().ForEach(x =>
{

    string message = $"Message {x}";

    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("fanout-type", string.Empty, null, messageBody);

    Console.WriteLine($"Mesaj gönderilmiştir. = {message}");

});


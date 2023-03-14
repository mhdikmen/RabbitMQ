// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("mesajlar dinleniyor....");
var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://zrdbzcyq:VAy_ksplaQG0kI-pza5_AzM_k-so7Tcz@possum.lmq.cloudamqp.com/zrdbzcyq");

var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.BasicQos(0, 10, true);


var randomQueue = channel.QueueDeclare(exclusive: false, durable: false, autoDelete: true);


channel.QueueBind(randomQueue.QueueName, "fanout-type", string.Empty, null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, args) =>
{

	try
	{
        var message = Encoding.UTF8.GetString(args.Body.ToArray());
        Console.WriteLine($"Gelen mesaj:{message}");
        Thread.Sleep(500);
		channel.BasicAck(args.DeliveryTag, false);
    }
	catch (Exception)
	{

		throw;
	}
  

};

channel.BasicConsume(randomQueue.QueueName, false, consumer);

Console.ReadLine();

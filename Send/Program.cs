﻿using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                for (int i = 0; i < 1000; i++)
                {
                    channel.QueueDeclare(queue: "hello",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                    string message = "Hello World! Counter is at " + i;
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message); 
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
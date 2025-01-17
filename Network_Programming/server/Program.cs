﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace np_sync_sockets
{
    class Program
    {
        static int port = 8080; // порт для приема входящих запросов
        static void Main(string[] args)
        {
            Dictionary<string, string> conversation = new Dictionary<string, string>();
            conversation.Add("Hello", "Hi");
            conversation.Add("How are you", "Fine, hope, your doing well");
            conversation.Add("What is your name", "My name is eho");
            conversation.Add("What is 2+2", "It is 4");
            conversation.Add("Bye", "Goodbye");
            // получаем адреса для запуска сокета
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1"); //localhost
            IPEndPoint ipPoint = new IPEndPoint(iPAddress, port);

            // об'єкт для отримання адреси відправника
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);
                Console.WriteLine("Server started! Waiting for connection...");

                while (true)
                {
                    // получаем сообщение
                    int bytes = 0;
                    byte[] data = new byte[1024];
                    bytes = listenSocket.ReceiveFrom(data, ref remoteEndPoint);

                    string msg = Encoding.Unicode.GetString(data, 0, bytes);
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");
                    string message = "";
                    foreach (var item in conversation)
                    {
                        if (item.Key == msg)
                        {
                            message = item.Value;
                        }

                    }

                    // отправляем ответ

                    data = Encoding.Unicode.GetBytes(message);
                    listenSocket.SendTo(data, remoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
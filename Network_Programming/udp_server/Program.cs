using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udp_server
{
    public class ChatServer
    {
        const short port = 4040;
        const string address = "127.0.0.1";
        TcpListener listener = null;
        IPEndPoint clientEndPoint = null;

        public ChatServer()
        {
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(address), port));

        }
        public void Start()
        {
            listener.Start();
            Console.WriteLine("Waiting for connection");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Connection");
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            while (true)
            {
                string message = reader.ReadLine();
                Console.WriteLine($"Message : {message} from : {client.Client.LocalEndPoint}. " +
                    $"Date : {DateTime.Now.ToShortTimeString()}");

                writer.WriteLine("Thanks!!!!");
                writer.Flush();
            }
        }

    }
    internal class Program
    {

        static void Main(string[] args)
        {

            ChatServer server = new ChatServer();
            server.Start();

        }
    }
}
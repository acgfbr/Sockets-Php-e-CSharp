using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket_Bolado
{
    class Program
    {
        private const int BUFFER_SIZE = 1024;

        private static void Main()
        {
            Console.Title = "Conexão Socket com C# e PHP";
            
            var port = 1818;

            TcpListener listener = null;

            try
            {
                listener = new TcpListener(IPAddress.Loopback, port); // padrao de loopback é 127.0.0.1 ( pelo menos na minha maquina )
                listener.Start();
            }
            catch (SocketException e)
            {
                //Console.WriteLine("{0}: {1}", e.ErrorCode, e.Message);
                //Environment.Exit(e.ErrorCode);
            }

            var buffer = new byte[BUFFER_SIZE];

            while (true)
            {
                TcpClient client = null;
                NetworkStream stream = null;

                try
                {
                    Console.WriteLine("Aguardando client: \n");

                    client = listener.AcceptTcpClient();
                    stream = client.GetStream();

                    while (true)
                    {
                        var bytesReceived = stream.Read(buffer, 0, BUFFER_SIZE);
                        Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bytesReceived));
                        if (bytesReceived != BUFFER_SIZE) break;
                    }

                    var response = Encoding.ASCII.GetBytes(string.Format("Oi, escrevi no php pelo c#) !"));

                    stream.Write(response, 0, response.Length);

                    Console.WriteLine("\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    stream.Close();
                    client.Close();
                }
            }
        }
    }
}

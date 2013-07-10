using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketServerTest
{
	class Program
	{
		static TcpListener listener;

		static void Main(string[] args)
		{
			IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
			Thread thListener = new Thread(new ThreadStart(ListenerThread));

			try
			{
				listener = new TcpListener(ipAddress, 27016);
				listener.Start();

				thListener.IsBackground = true;
				thListener.Name = "networkingThread";
				thListener.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("Escuchando...");
			Console.WriteLine("Presione cualquier tecla para finalizar");
			Console.ReadKey(true);
		}

		static void ListenerThread()
		{
			Console.WriteLine("listener = " + listener);

			while (true)
			{
				TcpClient client = listener.AcceptTcpClient();
				NetworkStream stream = client.GetStream();

				int byteValue = 0;

				while (byteValue != -1)
				{
					byteValue = stream.ReadByte();
					Console.Write((char)byteValue);
				}
				
				Console.WriteLine("\ncliente desconectado");
			}
		}
	}
}

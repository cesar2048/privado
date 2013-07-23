using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SpeechAnalyzer.Testing
{
	class NetServer
	{
		static TcpListener listener;

		static void Start()
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
				System.Diagnostics.Debug.WriteLine(e.ToString());
			}

			System.Diagnostics.Debug.WriteLine("Escuchando...");
			System.Diagnostics.Debug.WriteLine("Presione cualquier tecla para finalizar");
		}

		static void ListenerThread()
		{
			System.Diagnostics.Debug.WriteLine("listener = " + listener);

			while (true)
			{
				TcpClient client = listener.AcceptTcpClient();
				NetworkStream stream = client.GetStream();

				int byteValue = 0;

				while (byteValue != -1)
				{
					byteValue = stream.ReadByte();
					System.Diagnostics.Debug.Write((char)byteValue);
				}
				
				System.Diagnostics.Debug.WriteLine("\ncliente desconectado");
			}
		}
	}
}

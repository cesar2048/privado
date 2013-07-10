using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpeechAnalyzer.Model;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Net;

namespace SpeechAnalyzer
{
	public partial class Form1 : Form
	{
		private TcpClient client;
		private BackgroundWorker _bkgSocketWorker;

		private void btSocketConnect_Click(object sender, EventArgs e)
		{
			if (_bkgSocketWorker == null)
			{
				_bkgSocketWorker = new BackgroundWorker();
				_bkgSocketWorker.DoWork += new DoWorkEventHandler(_bkgSocketWorker_DoWork);
				_bkgSocketWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkgSocketWorker_RunWorkerCompleted);
			}

			if (String.IsNullOrWhiteSpace(txtIP.Text))
			{
				lblSocketStatus.Text = "IP error: la cadena no puede ser vacia";
			}
			else if (client == null || !client.Connected)
			{
				int port = 0;
				if (Int32.TryParse(txtPort.Text, out port))
				{
					try
					{
						lblSocketStatus.Text = "Conectando...";
						btSocketConnect.Enabled = false;
						_bkgSocketWorker.RunWorkerAsync((Int32) port);
					}
					catch (Exception ex)
					{
						lblSocketStatus.Text = "Error: " + ex.Message;
					}
				}
				else
				{
					lblSocketStatus.Text = "Puerto erroneo: formato invalido";
				}
			}
			else if (client != null && client.Connected)
			{
				desconectar();
			}
		}

		void _bkgSocketWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			lblSocketStatus.Text = e.Result.ToString();
			btSocketConnect.Enabled = true;

			if (client != null && client.Connected)
			{
				enviar("start");
				lblSocketStatus.Text = "Conectado :)";
				txtCommand.Enabled = true;
				txtCommand.Focus();
				btSocketConnect.Text = "Desconectar";
			}
		}

		void _bkgSocketWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = "";
			try
			{
				client = new TcpClient(txtIP.Text, (Int32)e.Argument);

			}
			catch (Exception ex)
			{
				e.Result = ex.Message;
			}
		}

		private void txtCommand_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				String msg = txtCommand.Text;
				txtCommand.Text = "";

				System.Diagnostics.Debug.WriteLine("val = [" + msg + "]");
				enviar(msg);
			}
		}

		private void enviar(String msg)
		{
			if (client != null && client.Connected)
			{
				try
				{
					StreamWriter sw = new StreamWriter(client.GetStream());
					sw.Write(msg);
					sw.Flush();
					txtCommandHistory.AppendText(msg + "\r\n");
				}
				catch (Exception)
				{
					client = null;
					desconectar();
				}
			}
		}

		private void desconectar()
		{
			if (client != null && client.Connected)
			{
				enviar("exit");
				try
				{
					client.Close();
					client = null;
				}
				catch (Exception)
				{
				}
			}

			txtCommand.Enabled = false;
			lblSocketStatus.Text = "Desconectado";
			btSocketConnect.Enabled = true;
			btSocketConnect.Text = "Conectar";
			txtCommand.Enabled = false;
		}


		Color colDefault;
		private void txtCommand_Enter(object sender, EventArgs e)
		{
			colDefault = txtCommand.BackColor;
			txtCommand.BackColor = Color.LightGreen;
		}

		private void txtCommand_Leave(object sender, EventArgs e)
		{
			txtCommand.BackColor = colDefault;
		}
	}
}

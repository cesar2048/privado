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
using System.Threading;
using NAudio.Wave;
using System.Diagnostics;
using NAudio.CoreAudioApi;
using System.Collections.ObjectModel;

namespace SpeechAnalyzer
{
	public partial class Form1 : Form
	{

		private void btSocketConnect_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Hola");
		}

		private void txtCommand_Enter(object sender, EventArgs e)
		{
			((TextBox)sender).BackColor = Color.LightGreen;
		}

		private void txtCommand_Leave(object sender, EventArgs e)
		{
			((TextBox)sender).BackColor = Color.White;
		}
	}
}

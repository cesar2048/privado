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

namespace SpeechAnalyzer
{
	public partial class Form1 : Form
	{
		DataAnalysis analysis;

		public Form1()
		{
			InitializeComponent();
			this.analysis = new DataAnalysis();

			String dir = Directory.GetCurrentDirectory();
			System.Diagnostics.Debug.WriteLine(dir);

		}

		private void btGen_Click(object sender, EventArgs e)
		{
			this.analysis.ReadTrainingFiles("data");
		}

	}
}

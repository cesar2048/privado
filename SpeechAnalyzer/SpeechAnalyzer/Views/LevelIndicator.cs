using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace SpeechAnalyzer.Views
{
	public partial class LevelIndicator : UserControl
	{
		private int _level;
		
		public Int32 Level { 
			get {
				return _level;
			}
			set{
				this._level = value;
				this.Refresh();
			}
		}

		public Int32 Maximum { get; set; }

		public Int32 Minimum { get; set; }

		public LevelIndicator()
		{
			InitializeComponent();

			this.Level = 50;
			this.Maximum = 100;
			this.Minimum = 0;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			// Create a local version of the graphics object for the PictureBox.
			Graphics g = e.Graphics;

			// Draw a string on the PictureBox.
			g.DrawString(this.Level.ToString(),
				new Font("Arial", 10), System.Drawing.Brushes.White, new Point(5, 30));
			// Draw a line in the PictureBox.
			float val = (float)Level / (float)Maximum;

			g.FillRectangle(Brushes.GreenYellow, new Rectangle(1, (int)(this.Height * (1 - val)), this.Width - 2, this.Height));
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoAudio
{
    public partial class Form1 : Form
    {
        private Audio ad = new Audio();
        private string ficheroSalida="";
        private bool flag_grabando = false;

        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Archivos Wav (*.wav)|*.wav | Archivos Mp3 (*.mp3)|*.mp3";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!flag_grabando)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        flag_grabando = true;
                        ficheroSalida = saveFileDialog1.FileName;
                        label2.Text = ficheroSalida;
                        ad.Grabar(ficheroSalida);
                        label1.Text = "Grabando...";
                    }
                }
                else
                {
                    MessageBox.Show("Hay una grabación en proceso...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label1.Text = "Error...";
            }
        }


        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }
    }
}

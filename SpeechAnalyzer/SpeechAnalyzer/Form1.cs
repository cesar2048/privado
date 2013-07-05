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
		DataAnalysis analysis;
		Config config;
        private IWaveIn waveIn;
        private WaveFileWriter writer;
        private string outputFilename="";
        private readonly string outputFolder;
        private float time = 30.0f;
        private string[] filePaths;
        private IWavePlayer player;
        private WaveFileReader reader;
        private Boolean thold = false;
        private int playPause = 1;
        private string filePlayed;
        private int tHoldValue = 0;
        private int tHoldInicio = 0;

		public Form1()
		{
			InitializeComponent();
            Disposed += OnRecordingPanelDisposed;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                LoadWasapiDevicesCombo();
            }
			String dir = Directory.GetCurrentDirectory();
			System.Diagnostics.Debug.WriteLine(dir);

			this.config = Config.ReadConfigFile();
			this.analysis = new DataAnalysis(config.DataDirectory, config.TempDirectory, config.SonicAnnotator);

            outputFolder = config.DataDirectory;
            filePaths = Directory.GetFiles(@config.DataDirectory, "*.wav");
            filePaths = withOutSlash(filePaths);
            listBoxRecordings.Items.AddRange(filePaths);
        }

        private string[] withOutSlash(string[] filePaths)
        {
            int cont = 0;
            while (cont < filePaths.Length)
            {
                char[] separadores = { '\\' };
                string[] arrayFile = filePaths[cont].Split(separadores);
                filePaths[cont] = arrayFile[1];
                cont++;
            }
            return filePaths;
        }

        private void LoadWasapiDevicesCombo()
        {
            var deviceEnum = new MMDeviceEnumerator();
            var devices = deviceEnum.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();

            comboWasapiDevices.DataSource = devices;
            comboWasapiDevices.DisplayMember = "FriendlyName";
        }
        void OnRecordingPanelDisposed(object sender, EventArgs e)
        {
            Cleanup();
        }
        private void Cleanup()
        {
            if (waveIn != null) 
            {
                waveIn.Dispose();
                waveIn = null;
            }
            if (writer != null)
            {
                writer.Close();
                writer = null;
            }
        }
		private void btGen_Click(object sender, EventArgs e)
		{
			this.analysis.TrainNeuralNetwork( );
		}

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                //Debug.WriteLine("Data Available");
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
            }
            else
            {
                if (chkThold.Checked == false)
                {
                    writer.Write(e.Buffer, 0, e.BytesRecorded);
                    float secondsRecorded = (float)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);
                    if (secondsRecorded >= time)
                    {
                        progressBar1.Value = 100;
                        StopRecording();
                    }
                    else
                    {
                        progressBar1.Value = (int)((secondsRecorded / time) * 100);
                    }
                }
                else
                {
                    if (tHoldInicio == 0)
                    {
                        byte[] bufAux = e.Buffer;
                        int bRc = e.BytesRecorded;
                        int startIndex = -1;
                        for (int index = 0; index < bRc && startIndex == 0; index += 2)
                        {
                            byte[] auxByte = new byte[2] { bufAux[index + 1], bufAux[index] };
                            int sample = BitConverter.ToUInt16(auxByte, 0);
                            int sampleI = (int)((sample / 65535) * 100);
                            if (sampleI > tHoldValue)
                            {
                                startIndex = index;
                            }
                        }
                        if (startIndex > 0)
                        {
                            writer.Write(bufAux, startIndex, bRc - startIndex);
                            tHoldInicio = 1;
                            System.Diagnostics.Debug.WriteLine("emplieza a guardar datos SampleI:"+tHoldValue);
                        }
                    }
                    else if (tHoldInicio==1)
                    {
                        writer.Write(e.Buffer, 0, e.BytesRecorded);
                        float secondsRecorded = (float)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);
                        if (secondsRecorded >= time)
                        {
                            progressBar1.Value = 100;
                            StopRecording();
                        }
                        else
                        {
                            progressBar1.Value = (int)((secondsRecorded / time) * 100);
                        }
                    }
                }
            }
        }

        private void StartRecording_Click(object sender, EventArgs e)
        {
            if (waveIn == null)
            {
                if (outputFilename == "" || outputFilename.StartsWith("muestra"))
                    {
                        outputFilename = String.Format("muestra{0:yyy-mm-ddHH-mm-ss}.wav", DateTime.Now);
                    }                
                    waveIn = new WaveIn();
                    waveIn.WaveFormat = new WaveFormat(8000, 1);
               

                writer = new WaveFileWriter(Path.Combine(outputFolder, outputFilename), waveIn.WaveFormat);

                waveIn.DataAvailable += OnDataAvailable;
                waveIn.RecordingStopped += OnRecordingStopped;
                waveIn.StartRecording();
                StartRecording.Enabled = false;
            }
        }
        void StopRecording()
        {
            Debug.WriteLine("StopRecording");
            waveIn.StopRecording();
        }
        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<StoppedEventArgs>(OnRecordingStopped), sender, e);
            }
            else
            {
                Cleanup();
                StartRecording.Enabled = true;
                progressBar1.Value = 0;
                if (e.Exception != null)
                {
                    MessageBox.Show(String.Format("A problem was encountered during recording {0}",
                                                  e.Exception.Message));
                }
                int newItemIndex = listBoxRecordings.Items.Add(outputFilename);
                listBoxRecordings.SelectedIndex = newItemIndex;
            }
        }

        private void ButtonStopRecording_Click(object sender, EventArgs e)
        {
            if (waveIn != null)
            {
                StopRecording();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (player != null)
            {
                player.Stop();
               
            }
            btnPlay.Text = "Reproducir";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxRecordings.SelectedItem != null)
            {
                try
                {
                    File.Delete(Path.Combine(outputFolder, (string)listBoxRecordings.SelectedItem));
                    listBoxRecordings.Items.Remove(listBoxRecordings.SelectedItem);
                    if (listBoxRecordings.Items.Count > 0)
                    {
                        listBoxRecordings.SelectedIndex = 0;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not delete recording");
                }
            }
        }

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(outputFolder);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (listBoxRecordings.SelectedItem != null)
            {
                Process.Start(Path.Combine(outputFolder, (string)listBoxRecordings.SelectedItem));
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtTiempo_TextChanged(object sender, EventArgs e)
        {
            time =(float) Convert.ToDouble(txtTiempo.Text.Replace('.',','));
            System.Diagnostics.Debug.WriteLine("variable time" + time + "texto=" + txtTiempo.Text);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            outputFilename = txtNombre.Text + ".wav";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (filePlayed != listBoxRecordings.SelectedItem.ToString() || playPause==0)
            {
                filePlayed = listBoxRecordings.SelectedItem.ToString();
                reader = new WaveFileReader("data/" + listBoxRecordings.SelectedItem);
                player = new WaveOut(WaveCallbackInfo.FunctionCallback());
                player.Init(reader);
                player.PlaybackStopped += Playback_Stopped;
                playPause = 1;
            }
            if (playPause == 1)
            {
                player.Play();
                btnPlay.Text = "Pausa";
                playPause = 2;
            }
            else
            {
                player.Pause();
                btnPlay.Text = "Reproducir";
                playPause = 1;
            }
        }
        void Playback_Stopped(object sender, StoppedEventArgs e)
        {
            player.Dispose();
            btnPlay.Text = "Reproducir";
            playPause = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (thold == false)
            {
                ButtonStopRecording.Enabled = false;
                nupHold.Enabled = true;
                thold = true;
            }
            else
            {
                ButtonStopRecording.Enabled = true;
                nupHold.Enabled = false;
                thold = false;
            }
        }

        private void nupHold_ValueChanged(object sender, EventArgs e)
        {
            tHoldValue = (int) nupHold.Value;
        }
	}
}

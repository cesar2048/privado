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
		Thread workerThread;

        private IWaveIn waveIn;
        private WaveFileWriter writer;
        private string outputFilename="";
        private readonly string outputFolder;
        
        private string[] filePaths;
        private IWavePlayer player;
        private WaveFileReader reader;
        private int playPause = 1;
        private string filePlayed;

		private float time = 30.0f;
		private int tHoldValue = 0;
        private bool tHoldInicio = false;

		public Form1()
		{
			InitializeComponent();
            Disposed += OnRecordingPanelDisposed;
            if (Environment.OSVersion.Version.Major >= 6)
            {
                LoadWasapiDevicesCombo();
            }

			this.config		= Config.ReadConfigFile();
			this.analysis	= new DataAnalysis(config.DataDirectory, config.TempDirectory, config.SonicAnnotator);
			this.analysis.Finished += new EventHandler(analysis_Finished);
			this.analysis.Progress += new EventHandler(analysis_Progress);

            outputFolder	= config.DataDirectory;
            filePaths		= withOutSlash(Directory.GetFiles(@config.DataDirectory, "*.wav"));

			// components
			this.listBoxRecordings.Items.AddRange(filePaths);
			this.txtTiempo.Text		= "5";
			this.nupHold.Value		= 30;
			this.picWorking.Visible = false;
        }

		private string[] withOutSlash(string[] filePaths)
        {
			return (from name in filePaths
					select Path.GetFileName(name)).ToArray();
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
			this.lblNetStatus.Text = "Working...";
			this.workerThread = new Thread(new ThreadStart(this.analysis.TrainNeuralNetwork));
			this.workerThread.Start();
			this.picWorking.Visible = true;
			this.btGen.Enabled = false;
		}

		void analysis_Finished(object sender, EventArgs e)
		{
			this.lblNetStatus.Text = "Finished, Cost = " + analysis.FinalCostValue;
			this.picWorking.Visible = false;
			this.btGen.Enabled = true;
		}

		void analysis_Progress(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.BeginInvoke(new EventHandler<EventArgs>(analysis_Progress), sender, e);
			}
			else
			{
				this.txtNetConsole.Text = this.analysis.ConsoleOut;
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
				waveIn.WaveFormat			= new WaveFormat(8000, 1);
				waveIn.DataAvailable		+= OnDataAvailable;
				waveIn.RecordingStopped		+= OnRecordingStopped;

				writer = new WaveFileWriter(Path.Combine(outputFolder, outputFilename), waveIn.WaveFormat);
				waveIn.StartRecording();

				btStartRecording.Enabled = false;
			}
		}


        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
            }
            else
            {
				bool oldTHold = tHoldInicio;
                if (chkThold.Checked == false)
                {
					tHoldInicio = true;
                }
                else
                {
					MemoryStream ms = new MemoryStream(e.Buffer);
					BinaryReader br = new BinaryReader(ms);
					try
					{
						double max = 0;
						for (int i = 0; i < e.Buffer.Length / 2; i++)
						{
							double val = Math.Abs((br.ReadInt16() * 100.0) / 0x8FFF);
							if (val > max) max = val;
						}
						if (max > tHoldValue) { tHoldInicio = true; }
						this.levelIndicator1.Level = (int)max;
					} catch (EndOfStreamException /*eos*/) {
						// ignored
					} finally {
						br.Close();
					}
                }

				if (oldTHold != tHoldInicio && tHoldInicio == true)
				{
					lblStatus.Text = "Grabando...";
				}

				if (tHoldInicio)
				{
					writer.Write(e.Buffer, 0, e.BytesRecorded);
					float secondsRecorded = (float)writer.Length / (float)writer.WaveFormat.AverageBytesPerSecond;
					if (secondsRecorded >= time)
					{
						progressBar1.Value = 100;
						tHoldInicio = false;
						StopRecording();
					}
					else
					{
						progressBar1.Value = (int)((secondsRecorded / time) * 100);
					}
				}
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
                btStartRecording.Enabled = true;
                progressBar1.Value = 0;
				lblStatus.Text = "Esperando..";

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

        private void btnDelete_Click(object sender, EventArgs e)
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

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(outputFolder);
        }

        private void btnPlayExtern_Click(object sender, EventArgs e)
        {
            if (listBoxRecordings.SelectedItem != null)
            {
                Process.Start(Path.Combine(outputFolder, (string)listBoxRecordings.SelectedItem));
            }
        }
		
        private void btnPlay_Click(object sender, EventArgs e)
        {
			if (listBoxRecordings.SelectedItem == null)
			{
				MessageBox.Show("No audio selected");
				return;
			}

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
			reader.Close();
            player.Dispose();
            btnPlay.Text = "Reproducir";
            playPause = 0;
        }


		//															//
		// ---------------- on change - events -------------------- //
		//															//

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkThold.Checked)
            {
                btStopRecording.Enabled = false;
                nupHold.Enabled = true;
            }
            else
            {
                btStopRecording.Enabled = true;
                nupHold.Enabled = false;
            }
        }

        private void nupHold_ValueChanged(object sender, EventArgs e)
        {
            tHoldValue = (int) nupHold.Value;
        }


		private void txtTiempo_TextChanged(object sender, EventArgs e)
		{
			time = (float)Convert.ToDouble(txtTiempo.Text.Replace('.', ','));
			System.Diagnostics.Debug.WriteLine("variable time" + time + "texto=" + txtTiempo.Text);
		}

		private void txtNombre_TextChanged(object sender, EventArgs e)
		{
			outputFilename = txtNombre.Text + ".wav";
		}


		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (player != null)
			{
				player.Stop();
			}
			btnPlay.Text = "Reproducir";
		}

	}
}

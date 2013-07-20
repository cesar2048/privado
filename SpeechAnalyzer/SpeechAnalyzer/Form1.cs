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

		private IWaveIn waveIn, waveIn2;
		private WaveFileWriter writer,writer2;
		private string outputFilename = "",outputFilename2;
        private readonly string outputFolder, outputFolder2;

		private string[] filePaths;
		private IWavePlayer player;
		private WaveFileReader reader;
		private int playPause = 1;
		private string filePlayed;
        private string extra;

		private float time,time2 = 30.0f;
        private int tHoldValue, tHoldValue2 = 0;
        private bool tHoldInicio, tHoldInicio2 = false;
        private int muestrasSegundo = 44100;
        

		BackgroundWorker _bkgDataGeneration;
		BackgroundWorker _bkgTraining;
		BackgroundWorker _bkgSvm;

		public Form1()
		{
			InitializeComponent();
			Disposed += OnRecordingPanelDisposed;
			if (Environment.OSVersion.Version.Major >= 6)
			{
				LoadWasapiDevicesCombo();
			}
           
			this.config = Config.ReadConfigFile();
			this.outputFolder = config.DataDirectory;
            this.outputFolder2 = config.TempDirectory;

			this.analysis = new DataAnalysis(
				config.DataDirectory, 
				config.TempDirectory, 
				config.SonicAnnotator, 
				(str) => this.ConsoleAppend(str) 
			);

			_bkgDataGeneration = new BackgroundWorker();
			_bkgDataGeneration.WorkerReportsProgress = true;
			_bkgDataGeneration.DoWork += new DoWorkEventHandler(_bkgDataGeneration_DoWork);
			_bkgDataGeneration.ProgressChanged += new ProgressChangedEventHandler(_bkgDataGeneration_ProgressChanged);
			_bkgDataGeneration.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkgDataGeneration_RunWorkerCompleted);


			_bkgTraining = new BackgroundWorker();
			_bkgTraining.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkgTraining_RunWorkerCompleted);
			_bkgTraining.WorkerReportsProgress = true;
			_bkgTraining.ProgressChanged += new ProgressChangedEventHandler(_bkgTraining_ProgressChanged);
			_bkgTraining.DoWork += new DoWorkEventHandler(_bkgTraining_DoWork);

			_bkgSvm = new BackgroundWorker();
			_bkgSvm.DoWork += new DoWorkEventHandler(_bkgSvm_DoWork);
			_bkgSvm.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bkgSvm_RunWorkerCompleted);
		}



		private void Form1_Load(object sender, EventArgs e)
		{
			// tab grabaciones

			filePaths = withOutSlash(Directory.GetFiles(@config.DataDirectory, "*.wav"));
			this.listBoxRecordings.Items.AddRange(filePaths);

            time = (float)Convert.ToDouble(txtTiempo.Text);
			this.nupHold.Value = 30;
            this.tHoldValue2 = 30;
			// tab neural network
			
			this.picWorking.Visible = false;
			this.txtLambda.Text = analysis.Lambda.ToString();
			this.analysis.Iterations = Convert.ToInt32(this.txtIteraciones.Text);

			// tab datos
			this.progDataGen.Visible = false;

			try
			{
				var funcionesList = analysis.LoadFeatures().ToArray();
				this.listFunciones.Items.AddRange(funcionesList);
				this.listFunciones.SelectedIndex = 0;
				this.analysis.UpdateProblemName(funcionesList[0]);

			}
			catch (Exception /*e*/) { }

			lblFeatStatusUpdate();
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























		//															//
		// ---------------- neural network code ------------------- //
		//															//

		private void btTrain_Click(object sender, EventArgs e)
		{
			this.lblNetStatus.Text = "Working...";
			this.btTrain.Enabled = false;
			this.txtLambda.Enabled = false;
			this.picWorking.Visible = true;
			this.progDataGen.Value = 0;
			this.progDataGen.Visible = true;

			this._bkgTraining.RunWorkerAsync();
		}

		void _bkgTraining_DoWork(object sender, DoWorkEventArgs e)
		{
			try	{
				bool useLambdaSet = radLambdaSet.Checked;
				this.analysis.TrainNeuralNetwork( 
					val => ((BackgroundWorker)sender).ReportProgress(val), 
					useLambdaSet
				);
				e.Result = this.analysis.FinalCostValue;
			} catch (Exception) {
				e.Result = "No se han generado las features";
			}
		}
		
		void _bkgTraining_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progDataGen.Value = e.ProgressPercentage;
			
			// plot error graph
			foreach (var serie in chartCurves.Series)
			{
				serie.Points.Clear();
			}
			for (int i = 0; i < analysis.CostTest.Count; i++)
			{
				chartCurves.Series["Training"].Points.AddXY(i, analysis.CostTrain[i]);
				chartCurves.Series["Testing"].Points.AddXY(i, analysis.CostTest[i]);
				
				chartCurves.Series["ErrTraining"].Points.AddXY(i, analysis.ErrorTrain[i]);
				chartCurves.Series["ErrTesting"].Points.AddXY(i, analysis.ErrorTest[i]);
			}
		}

		void _bkgTraining_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Result is String) {
				this.lblNetStatus.Text = e.Result.ToString();
			} else {
				this.lblNetStatus.Text = String.Format("Cost = {0,5:f} accuracy = {1,8:p}",
						analysis.FinalCostValue,
						analysis.FinalAccuracy);
			}
			
			this.btTrain.Enabled = true;
			this.txtLambda.Enabled = true;
			this.picWorking.Visible = false;
			this.progDataGen.Visible = false;
		}

		private void btPredecir_Click(object sender, EventArgs e)
		{
			if (listBoxRecordings.SelectedItem == null)
			{
				MessageBox.Show("No audio selected");
				return;
			}

			String file = listBoxRecordings.SelectedItem.ToString();
			String label = this.analysis.TestNeuralNetwork(Path.Combine(config.DataDirectory, file));
			MessageBox.Show("Tipo:" + label);
		}

		private void btSavePlot_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
			sfd.FilterIndex = 0;
			sfd.RestoreDirectory = true;

			Stream outStream = null;
			if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				if ((outStream = sfd.OpenFile()) != null)
				{
					chartCurves.SaveImage(outStream, System.Drawing.Imaging.ImageFormat.Png);
					outStream.Close();
				}
			}
		}

		// -------------- SVM TEST -------------------------- //

		private void btSvm_Click(object sender, EventArgs e)
		{
			picWorking.Visible = true;
			btSvm.Enabled = false;
			_bkgSvm.RunWorkerAsync();
		}

		void _bkgSvm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			btSvm.Enabled = true;
			picWorking.Visible = false;
			lblNetStatus.Text = String.Format("SVM Accuracy =  {0,8:p}", e.Result);
		}

		void _bkgSvm_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = Model.LibSvmTest.TestOnData();
		}

		public void ConsoleAppend(String str)
		{
			if (this.InvokeRequired) { this.BeginInvoke(new Action<String>(ConsoleAppend), new Object[] { str }); }
			else
			{
				this.txtConsola.AppendText("\r\n" + str);
			}
		}



















		//															//
		// ---------------- recording ----------------------------- //
		//															//

		private void StartRecording_Click(object sender, EventArgs e)
		{
			if (waveIn == null)
			{
                
                if (outputFilename == "" || outputFilename.StartsWith("muestra"))
                {
                    outputFilename = String.Format("muestra{0:yyy-mm-ddHH-mm-ss}.wav", DateTime.Now);
                    extra = "";
                }
                else
                {
                    extra = String.Format("{0:yyy-mm-ddHH-mm-ss}.wav", DateTime.Now);
                }

				waveIn = new WaveIn();
				waveIn.WaveFormat = new WaveFormat(muestrasSegundo, 1);
				waveIn.DataAvailable += OnDataAvailable;
				waveIn.RecordingStopped += OnRecordingStopped;

				writer = new WaveFileWriter(Path.Combine(outputFolder, outputFilename+ extra), waveIn.WaveFormat);
				waveIn.StartRecording();

				btStartRecording.Enabled = false;
			}
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
					}
					catch (EndOfStreamException /*eos*/)
					{
						// ignored
					}
					finally
					{
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
				int newItemIndex = listBoxRecordings.Items.Add(outputFilename+extra);
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



















		//															//
		// ---------------- sounds list playback------------------- //
		//															//

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (listBoxRecordings.SelectedItem != null)
			{
				try
				{
					File.Delete(Path.Combine(outputFolder, (string)listBoxRecordings.SelectedItem));
					listBoxRecordings.Items.Remove(listBoxRecordings.SelectedItem);

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

			if (filePlayed != listBoxRecordings.SelectedItem.ToString() || playPause == 0)
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
			tHoldValue = (int)nupHold.Value;
		}


		private void txtTiempo_TextChanged(object sender, EventArgs e)
		{
			time = (float)Convert.ToDouble(txtTiempo.Text.Replace('.', '.'));
			//System.Diagnostics.Debug.WriteLine("variable time" + time + "texto=" + txtTiempo.Text);
		}

		private void txtNombre_TextChanged(object sender, EventArgs e)
		{
			outputFilename = txtNombre.Text ;
		}


		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (player != null)
			{
				player.Stop();
			}
			btnPlay.Text = "Reproducir";
		}


		private void txtLambda_TextChanged(object sender, EventArgs e)
		{
			double lambda = this.analysis.Lambda;
			if (Double.TryParse(this.txtLambda.Text, out lambda))
			{
				this.analysis.Lambda = lambda;
				txtLambda.BackColor = Color.White;
			}
			else
			{
				txtLambda.BackColor = Color.Red;
			}
		}

		private void txtIteraciones_TextChanged(object sender, EventArgs e)
		{
			Int32 iter = this.analysis.Iterations;
			if (Int32.TryParse(this.txtIteraciones.Text, out iter))
			{
				this.analysis.Iterations = iter;
			}
		}

















		//
		// -------------------- tab Neural Network ---------------------
		//

		private void btRemoveFeatures_Click(object sender, EventArgs e)
		{
			if (analysis.trainingFile.Exists)
			{
				analysis.trainingFile.Delete();
				lblFeatStatus.Text = "Vacio";
			}
		}

		private void btGenFeatures_Click(object sender, EventArgs e)
		{
			this.progDataGen.Visible = true;
			this.btTrain.Enabled = false;
			this.btGenFeatures.Enabled = false;
			this.btRemoveFeatures.Enabled = false;
			this._bkgDataGeneration.RunWorkerAsync();
		}

		void _bkgDataGeneration_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				BackgroundWorker w = ((BackgroundWorker)sender);
				this.analysis.GenerateTrainingFeatures((p) => { if (w.WorkerReportsProgress) w.ReportProgress(p); });
				e.Result = Boolean.TrueString;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				e.Result = Boolean.FalseString;
			}
		}

		void _bkgDataGeneration_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progDataGen.Value = e.ProgressPercentage;
		}

		void _bkgDataGeneration_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.progDataGen.Visible = false;
			this.btTrain.Enabled = true;
			this.btGenFeatures.Enabled = true;
			this.btRemoveFeatures.Enabled = true;

			if (String.Equals(e.Result, Boolean.TrueString))
			{
				this.lblFeatStatus.Text = "Listo";
			}
			else
			{
				this.lblFeatStatus.Text = "Vacio";
			}
		}

		//
		// ------------------------- TAB: Escuchar  ------------------------------ //
		//


        private void btnEscuchar_Click(object sender, EventArgs e)
        {
            time2 = (float)Convert.ToDouble(txtTiempo2.Text.Replace('.','.'));
            //System.Diagnostics.Debug.WriteLine("variable time" + time2 + "texto=" + txtTiempo2.Text);
            if (waveIn2 == null)
            {
                outputFilename2 = String.Format("temp.wav", DateTime.Now);
                waveIn2 = new WaveIn();
                waveIn2.WaveFormat = new WaveFormat(muestrasSegundo, 1);
                waveIn2.DataAvailable += OnDataAvailable2;
                waveIn2.RecordingStopped += OnRecordingStopped2;

                writer2 = new WaveFileWriter(Path.Combine(outputFolder2, outputFilename2), waveIn2.WaveFormat);
                waveIn2.StartRecording();

               // btStartRecording.Enabled = false;
            }

        }
        void OnDataAvailable2(object sender, WaveInEventArgs e)
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
                    if (max > tHoldValue2) { tHoldInicio2 = true; }
                    this.levelIndicator2.Level = (int)max;
                }
                catch (EndOfStreamException /*eos*/)
                {
                    // ignored
                }
                finally
                {
                    br.Close();
                }
                if (tHoldInicio2)
                {
                    writer2.Write(e.Buffer, 0, e.BytesRecorded);
                    float secondsRecorded2 = (float)writer2.Length / (float)writer2.WaveFormat.AverageBytesPerSecond;
                    if (secondsRecorded2 >= time2)
                    {            
                        tHoldInicio2 = false;
                        stopWriting2();
                        progressBar2.Value = 0;
                    }
                    else
                    {
                        progressBar2.Value = (int)((secondsRecorded2 / time2) * 100);
                    }
                }
        }
        
		private void stopWriting2(){
        
                if (writer2 != null)
                {
                    writer2.Close();
                    String label = this.analysis.TestNeuralNetwork(Path.Combine(config.TempDirectory, "temp.wav"));
                   // MessageBox.Show("Tipo:" + label);
                    lblPrediction.Text = label ;
                   //File.Delete(Path.Combine(outputFolder2, outputFilename2));
                    writer2=new WaveFileWriter(Path.Combine(outputFolder2, outputFilename2), waveIn2.WaveFormat);

					enviar(label);
                }
        }
        
		private void button1_Click(object sender, EventArgs e)
        {
            if(waveIn2!=null){
                 waveIn2.Dispose();
                 waveIn2 = null;
            }
            if(writer2!=null){
                writer2.Close();
                writer2 = null;
            }
        }

        private void Thold2_ValueChanged(object sender, EventArgs e)
        {
            tHoldValue2 = (int)nupHold.Value;
        }

        private void OnRecordingStopped2(object sender, EventArgs e){
            if(waveIn2!= null)
            {
               waveIn2.Dispose();
               waveIn2 = null;
            }
            if(writer2!=null)
            {
                writer2.Close();
                writer2 = null;
            }
        }


		private void button4_Click(object sender, EventArgs e)
		{
			if (txNombreFuncion.Text != "")
			{
				listFunciones.Items.Add(txNombreFuncion.Text);
				txNombreFuncion.Text = "";
				this.analysis.SaveFeature(txNombreFuncion.Text);
			}
			else
			{
				MessageBox.Show("Ingrese un nombre para la funcion");
			}
		}






		private void lblFeatStatusUpdate()
		{
			lblFeatStatus.Text = "(No generado)";
			if (analysis.trainingFile.Exists)
			{
				lblFeatStatus.Text = "Datos listos";
			}
		}

		private void listFunciones_SelectedIndexChanged(object sender, EventArgs e)
		{
			string funcion = listFunciones.SelectedItem.ToString();
			List<String> etiquetas=analysis.LoadLabelFeatures(funcion);
			
			listEtiquetasFuncion.Items.Clear();
			listEtiquetasFuncion.Items.AddRange(etiquetas.ToArray());

			analysis.UpdateProblemName(funcion);
			lblFeatStatusUpdate();
		}

		private void btAddLabel_Click(object sender, EventArgs e)
		{
			if (txtLabel.Text.Trim().Length > 0)
			{
				analysis.SaveFeatureLabel(listFunciones.SelectedItem.ToString(), txtLabel.Text.Trim());
				listEtiquetasFuncion.Items.Add( txtLabel.Text.Trim() );
				txtLabel.Text = "";
			}
			else
			{
				MessageBox.Show("Seleccione una etiqueta y una funcion");
			}
		}

		private void btRemoveLabel_Click(object sender, EventArgs e)
		{
			if (listFunciones.SelectedIndex >= 0 && listEtiquetasFuncion.SelectedIndex >= 0)
			{
				analysis.DeleteFeatureLabel(listFunciones.SelectedItem.ToString(), listEtiquetasFuncion.SelectedItem.ToString());
				listEtiquetasFuncion.Items.RemoveAt(listEtiquetasFuncion.SelectedIndex);
			}
			else
			{
				MessageBox.Show("Seleccione una etiqueta y una funcion");
			}
		}

		private void radLambda_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void radLambdaSingle_CheckedChanged(object sender, EventArgs e)
		{
			if (radLambdaSet.Checked)
			{
				txtLambda.Enabled = false;
			}
			else
			{
				txtLambda.Enabled = true;
			}
		}

	}
}

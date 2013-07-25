namespace SpeechAnalyzer
{
	partial class Form1
	{
		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén utilizando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.comboWasapiDevices = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btStartRecording = new System.Windows.Forms.Button();
			this.btStopRecording = new System.Windows.Forms.Button();
			this.listBoxRecordings = new System.Windows.Forms.ListBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnOpenFolder = new System.Windows.Forms.Button();
			this.btnPlayExtern = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtTiempo = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtNombre = new System.Windows.Forms.TextBox();
			this.btnPlay = new System.Windows.Forms.Button();
			this.chkThreshold = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.nupHold = new System.Windows.Forms.NumericUpDown();
			this.lblStatus = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.lblTestPredict = new System.Windows.Forms.Label();
			this.btPredecir = new System.Windows.Forms.Button();
			this.levelIndicator1 = new SpeechAnalyzer.Views.LevelIndicator();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.checkPlotTestErr = new System.Windows.Forms.CheckBox();
			this.checkPlotTrainErr = new System.Windows.Forms.CheckBox();
			this.checkPlotTestCost = new System.Windows.Forms.CheckBox();
			this.checkPlotTrainCost = new System.Windows.Forms.CheckBox();
			this.btRemoveFunction = new System.Windows.Forms.Button();
			this.picWorking = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radLambdaSingle = new System.Windows.Forms.RadioButton();
			this.radLambdaSet = new System.Windows.Forms.RadioButton();
			this.btSavePlot = new System.Windows.Forms.Button();
			this.txtConsola = new System.Windows.Forms.TextBox();
			this.btRemoveData = new System.Windows.Forms.Button();
			this.chartCurves = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.btAddFunction = new System.Windows.Forms.Button();
			this.txtFunction = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.btRemoveLabel = new System.Windows.Forms.Button();
			this.btAddLabel = new System.Windows.Forms.Button();
			this.listLabels = new System.Windows.Forms.ListBox();
			this.listFunctions = new System.Windows.Forms.ListBox();
			this.btSvm = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.txtIteraciones = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtLambda = new System.Windows.Forms.TextBox();
			this.lblNetStatus = new System.Windows.Forms.Label();
			this.btTrain = new System.Windows.Forms.Button();
			this.progDataGen = new System.Windows.Forms.ProgressBar();
			this.lblFeatStatus = new System.Windows.Forms.Label();
			this.btGenData = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtLabel = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtCommandHistory = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.txtCommand = new System.Windows.Forms.TextBox();
			this.btSocketConnect = new System.Windows.Forms.Button();
			this.lblSocketStatus = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.txtIP = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.lblPrediction = new System.Windows.Forms.Label();
			this.Thold2 = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.txtTiempo2 = new System.Windows.Forms.TextBox();
			this.btnDetener = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.btnEscuchar = new System.Windows.Forms.Button();
			this.levelIndicator2 = new SpeechAnalyzer.Views.LevelIndicator();
			((System.ComponentModel.ISupportInitialize)(this.nupHold)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picWorking)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chartCurves)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Thold2)).BeginInit();
			this.SuspendLayout();
			// 
			// comboWasapiDevices
			// 
			this.comboWasapiDevices.FormattingEnabled = true;
			resources.ApplyResources(this.comboWasapiDevices, "comboWasapiDevices");
			this.comboWasapiDevices.Name = "comboWasapiDevices";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// btStartRecording
			// 
			resources.ApplyResources(this.btStartRecording, "btStartRecording");
			this.btStartRecording.Name = "btStartRecording";
			this.btStartRecording.UseVisualStyleBackColor = true;
			this.btStartRecording.Click += new System.EventHandler(this.StartRecording_Click);
			// 
			// btStopRecording
			// 
			resources.ApplyResources(this.btStopRecording, "btStopRecording");
			this.btStopRecording.Name = "btStopRecording";
			this.btStopRecording.UseVisualStyleBackColor = true;
			this.btStopRecording.Click += new System.EventHandler(this.ButtonStopRecording_Click);
			// 
			// listBoxRecordings
			// 
			resources.ApplyResources(this.listBoxRecordings, "listBoxRecordings");
			this.listBoxRecordings.FormattingEnabled = true;
			this.listBoxRecordings.Name = "listBoxRecordings";
			this.listBoxRecordings.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// progressBar1
			// 
			resources.ApplyResources(this.progressBar1, "progressBar1");
			this.progressBar1.Name = "progressBar1";
			// 
			// btnDelete
			// 
			resources.ApplyResources(this.btnDelete, "btnDelete");
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnOpenFolder
			// 
			resources.ApplyResources(this.btnOpenFolder, "btnOpenFolder");
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.UseVisualStyleBackColor = true;
			this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
			// 
			// btnPlayExtern
			// 
			resources.ApplyResources(this.btnPlayExtern, "btnPlayExtern");
			this.btnPlayExtern.Name = "btnPlayExtern";
			this.btnPlayExtern.UseVisualStyleBackColor = true;
			this.btnPlayExtern.Click += new System.EventHandler(this.btnPlayExtern_Click);
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// txtTiempo
			// 
			resources.ApplyResources(this.txtTiempo, "txtTiempo");
			this.txtTiempo.Name = "txtTiempo";
			this.txtTiempo.TextChanged += new System.EventHandler(this.txtTiempo_TextChanged);
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// txtNombre
			// 
			resources.ApplyResources(this.txtNombre, "txtNombre");
			this.txtNombre.Name = "txtNombre";
			this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
			// 
			// btnPlay
			// 
			resources.ApplyResources(this.btnPlay, "btnPlay");
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.UseVisualStyleBackColor = true;
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// chkThreshold
			// 
			resources.ApplyResources(this.chkThreshold, "chkThreshold");
			this.chkThreshold.Name = "chkThreshold";
			this.chkThreshold.UseVisualStyleBackColor = true;
			this.chkThreshold.CheckedChanged += new System.EventHandler(this.chkTreshold_CheckedChanged);
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// nupHold
			// 
			resources.ApplyResources(this.nupHold, "nupHold");
			this.nupHold.Name = "nupHold";
			this.nupHold.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.nupHold.ValueChanged += new System.EventHandler(this.nupHold_ValueChanged);
			// 
			// lblStatus
			// 
			resources.ApplyResources(this.lblStatus, "lblStatus");
			this.lblStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblStatus.Name = "lblStatus";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			resources.ApplyResources(this.tabControl1, "tabControl1");
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.lblTestPredict);
			this.tabPage1.Controls.Add(this.btPredecir);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.comboWasapiDevices);
			this.tabPage1.Controls.Add(this.lblStatus);
			this.tabPage1.Controls.Add(this.btStartRecording);
			this.tabPage1.Controls.Add(this.levelIndicator1);
			this.tabPage1.Controls.Add(this.btStopRecording);
			this.tabPage1.Controls.Add(this.nupHold);
			this.tabPage1.Controls.Add(this.listBoxRecordings);
			this.tabPage1.Controls.Add(this.label4);
			this.tabPage1.Controls.Add(this.progressBar1);
			this.tabPage1.Controls.Add(this.chkThreshold);
			this.tabPage1.Controls.Add(this.btnDelete);
			this.tabPage1.Controls.Add(this.btnPlay);
			this.tabPage1.Controls.Add(this.btnOpenFolder);
			this.tabPage1.Controls.Add(this.txtNombre);
			this.tabPage1.Controls.Add(this.btnPlayExtern);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.txtTiempo);
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// lblTestPredict
			// 
			resources.ApplyResources(this.lblTestPredict, "lblTestPredict");
			this.lblTestPredict.Name = "lblTestPredict";
			// 
			// btPredecir
			// 
			resources.ApplyResources(this.btPredecir, "btPredecir");
			this.btPredecir.Name = "btPredecir";
			this.btPredecir.UseVisualStyleBackColor = true;
			this.btPredecir.Click += new System.EventHandler(this.btPredecir_Click);
			// 
			// levelIndicator1
			// 
			this.levelIndicator1.BackColor = System.Drawing.Color.Black;
			this.levelIndicator1.Level = 0;
			resources.ApplyResources(this.levelIndicator1, "levelIndicator1");
			this.levelIndicator1.Maximum = 100;
			this.levelIndicator1.Minimum = 0;
			this.levelIndicator1.Name = "levelIndicator1";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.checkPlotTestErr);
			this.tabPage3.Controls.Add(this.checkPlotTrainErr);
			this.tabPage3.Controls.Add(this.checkPlotTestCost);
			this.tabPage3.Controls.Add(this.checkPlotTrainCost);
			this.tabPage3.Controls.Add(this.btRemoveFunction);
			this.tabPage3.Controls.Add(this.picWorking);
			this.tabPage3.Controls.Add(this.groupBox1);
			this.tabPage3.Controls.Add(this.btSavePlot);
			this.tabPage3.Controls.Add(this.txtConsola);
			this.tabPage3.Controls.Add(this.btRemoveData);
			this.tabPage3.Controls.Add(this.chartCurves);
			this.tabPage3.Controls.Add(this.btAddFunction);
			this.tabPage3.Controls.Add(this.txtFunction);
			this.tabPage3.Controls.Add(this.label9);
			this.tabPage3.Controls.Add(this.btRemoveLabel);
			this.tabPage3.Controls.Add(this.btAddLabel);
			this.tabPage3.Controls.Add(this.listLabels);
			this.tabPage3.Controls.Add(this.listFunctions);
			this.tabPage3.Controls.Add(this.btSvm);
			this.tabPage3.Controls.Add(this.label10);
			this.tabPage3.Controls.Add(this.txtIteraciones);
			this.tabPage3.Controls.Add(this.label7);
			this.tabPage3.Controls.Add(this.label5);
			this.tabPage3.Controls.Add(this.txtLambda);
			this.tabPage3.Controls.Add(this.lblNetStatus);
			this.tabPage3.Controls.Add(this.btTrain);
			this.tabPage3.Controls.Add(this.progDataGen);
			this.tabPage3.Controls.Add(this.lblFeatStatus);
			this.tabPage3.Controls.Add(this.btGenData);
			this.tabPage3.Controls.Add(this.label8);
			this.tabPage3.Controls.Add(this.label6);
			this.tabPage3.Controls.Add(this.txtLabel);
			resources.ApplyResources(this.tabPage3, "tabPage3");
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// checkPlotTestErr
			// 
			resources.ApplyResources(this.checkPlotTestErr, "checkPlotTestErr");
			this.checkPlotTestErr.Checked = true;
			this.checkPlotTestErr.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPlotTestErr.Name = "checkPlotTestErr";
			this.checkPlotTestErr.UseVisualStyleBackColor = true;
			this.checkPlotTestErr.CheckedChanged += new System.EventHandler(this.checkPlot_CheckedChanged);
			// 
			// checkPlotTrainErr
			// 
			resources.ApplyResources(this.checkPlotTrainErr, "checkPlotTrainErr");
			this.checkPlotTrainErr.Checked = true;
			this.checkPlotTrainErr.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPlotTrainErr.Name = "checkPlotTrainErr";
			this.checkPlotTrainErr.UseVisualStyleBackColor = true;
			this.checkPlotTrainErr.CheckedChanged += new System.EventHandler(this.checkPlot_CheckedChanged);
			// 
			// checkPlotTestCost
			// 
			resources.ApplyResources(this.checkPlotTestCost, "checkPlotTestCost");
			this.checkPlotTestCost.Checked = true;
			this.checkPlotTestCost.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPlotTestCost.Name = "checkPlotTestCost";
			this.checkPlotTestCost.UseVisualStyleBackColor = true;
			this.checkPlotTestCost.CheckedChanged += new System.EventHandler(this.checkPlot_CheckedChanged);
			// 
			// checkPlotTrainCost
			// 
			resources.ApplyResources(this.checkPlotTrainCost, "checkPlotTrainCost");
			this.checkPlotTrainCost.Checked = true;
			this.checkPlotTrainCost.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkPlotTrainCost.Name = "checkPlotTrainCost";
			this.checkPlotTrainCost.UseVisualStyleBackColor = true;
			this.checkPlotTrainCost.CheckedChanged += new System.EventHandler(this.checkPlot_CheckedChanged);
			// 
			// btRemoveFunction
			// 
			resources.ApplyResources(this.btRemoveFunction, "btRemoveFunction");
			this.btRemoveFunction.Name = "btRemoveFunction";
			this.btRemoveFunction.UseVisualStyleBackColor = true;
			this.btRemoveFunction.Click += new System.EventHandler(this.btRemoveFunction_Click);
			// 
			// picWorking
			// 
			this.picWorking.Image = global::SpeechAnalyzer.Properties.Resources.multilockon;
			resources.ApplyResources(this.picWorking, "picWorking");
			this.picWorking.Name = "picWorking";
			this.picWorking.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radLambdaSingle);
			this.groupBox1.Controls.Add(this.radLambdaSet);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// radLambdaSingle
			// 
			resources.ApplyResources(this.radLambdaSingle, "radLambdaSingle");
			this.radLambdaSingle.Checked = true;
			this.radLambdaSingle.Name = "radLambdaSingle";
			this.radLambdaSingle.TabStop = true;
			this.radLambdaSingle.UseVisualStyleBackColor = true;
			this.radLambdaSingle.CheckedChanged += new System.EventHandler(this.radLambdaSingle_CheckedChanged);
			// 
			// radLambdaSet
			// 
			resources.ApplyResources(this.radLambdaSet, "radLambdaSet");
			this.radLambdaSet.Name = "radLambdaSet";
			this.radLambdaSet.UseVisualStyleBackColor = true;
			// 
			// btSavePlot
			// 
			resources.ApplyResources(this.btSavePlot, "btSavePlot");
			this.btSavePlot.Name = "btSavePlot";
			this.btSavePlot.UseVisualStyleBackColor = true;
			this.btSavePlot.Click += new System.EventHandler(this.btSavePlot_Click);
			// 
			// txtConsola
			// 
			resources.ApplyResources(this.txtConsola, "txtConsola");
			this.txtConsola.Name = "txtConsola";
			this.txtConsola.ReadOnly = true;
			this.txtConsola.TabStop = false;
			// 
			// btRemoveData
			// 
			resources.ApplyResources(this.btRemoveData, "btRemoveData");
			this.btRemoveData.Name = "btRemoveData";
			this.btRemoveData.UseVisualStyleBackColor = true;
			this.btRemoveData.Click += new System.EventHandler(this.btRemoveData_Click);
			// 
			// chartCurves
			// 
			resources.ApplyResources(this.chartCurves, "chartCurves");
			chartArea1.Name = "ChartArea1";
			this.chartCurves.ChartAreas.Add(chartArea1);
			legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
			legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
			legend1.Name = "Legend1";
			legend1.TableStyle = System.Windows.Forms.DataVisualization.Charting.LegendTableStyle.Tall;
			this.chartCurves.Legends.Add(legend1);
			this.chartCurves.Name = "chartCurves";
			this.chartCurves.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
			series1.BorderWidth = 2;
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series1.LabelBorderWidth = 2;
			series1.Legend = "Legend1";
			series1.Name = "Training";
			series1.YValuesPerPoint = 2;
			series2.BorderWidth = 2;
			series2.ChartArea = "ChartArea1";
			series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series2.Legend = "Legend1";
			series2.Name = "Testing";
			series3.BorderWidth = 2;
			series3.ChartArea = "ChartArea1";
			series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series3.Legend = "Legend1";
			series3.Name = "ErrTraining";
			series4.BorderWidth = 2;
			series4.ChartArea = "ChartArea1";
			series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series4.Legend = "Legend1";
			series4.Name = "ErrTesting";
			this.chartCurves.Series.Add(series1);
			this.chartCurves.Series.Add(series2);
			this.chartCurves.Series.Add(series3);
			this.chartCurves.Series.Add(series4);
			// 
			// btAddFunction
			// 
			resources.ApplyResources(this.btAddFunction, "btAddFunction");
			this.btAddFunction.Name = "btAddFunction";
			this.btAddFunction.UseVisualStyleBackColor = true;
			this.btAddFunction.Click += new System.EventHandler(this.btAddFunction_Click);
			// 
			// txtFunction
			// 
			resources.ApplyResources(this.txtFunction, "txtFunction");
			this.txtFunction.Name = "txtFunction";
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			// 
			// btRemoveLabel
			// 
			resources.ApplyResources(this.btRemoveLabel, "btRemoveLabel");
			this.btRemoveLabel.Name = "btRemoveLabel";
			this.btRemoveLabel.UseVisualStyleBackColor = true;
			this.btRemoveLabel.Click += new System.EventHandler(this.btRemoveLabel_Click);
			// 
			// btAddLabel
			// 
			resources.ApplyResources(this.btAddLabel, "btAddLabel");
			this.btAddLabel.Name = "btAddLabel";
			this.btAddLabel.UseVisualStyleBackColor = true;
			this.btAddLabel.Click += new System.EventHandler(this.btAddLabel_Click);
			// 
			// listLabels
			// 
			this.listLabels.FormattingEnabled = true;
			resources.ApplyResources(this.listLabels, "listLabels");
			this.listLabels.Name = "listLabels";
			// 
			// listFunctions
			// 
			this.listFunctions.FormattingEnabled = true;
			resources.ApplyResources(this.listFunctions, "listFunctions");
			this.listFunctions.Name = "listFunctions";
			this.listFunctions.SelectedIndexChanged += new System.EventHandler(this.listFunciones_SelectedIndexChanged);
			// 
			// btSvm
			// 
			resources.ApplyResources(this.btSvm, "btSvm");
			this.btSvm.Name = "btSvm";
			this.btSvm.UseVisualStyleBackColor = true;
			this.btSvm.Click += new System.EventHandler(this.btSvm_Click);
			// 
			// label10
			// 
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			// 
			// txtIteraciones
			// 
			resources.ApplyResources(this.txtIteraciones, "txtIteraciones");
			this.txtIteraciones.Name = "txtIteraciones";
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// txtLambda
			// 
			resources.ApplyResources(this.txtLambda, "txtLambda");
			this.txtLambda.Name = "txtLambda";
			// 
			// lblNetStatus
			// 
			this.lblNetStatus.ForeColor = System.Drawing.Color.Teal;
			resources.ApplyResources(this.lblNetStatus, "lblNetStatus");
			this.lblNetStatus.Name = "lblNetStatus";
			// 
			// btTrain
			// 
			resources.ApplyResources(this.btTrain, "btTrain");
			this.btTrain.Name = "btTrain";
			this.btTrain.UseVisualStyleBackColor = true;
			this.btTrain.Click += new System.EventHandler(this.btTrain_Click);
			// 
			// progDataGen
			// 
			resources.ApplyResources(this.progDataGen, "progDataGen");
			this.progDataGen.Name = "progDataGen";
			// 
			// lblFeatStatus
			// 
			resources.ApplyResources(this.lblFeatStatus, "lblFeatStatus");
			this.lblFeatStatus.ForeColor = System.Drawing.Color.Purple;
			this.lblFeatStatus.Name = "lblFeatStatus";
			// 
			// btGenData
			// 
			resources.ApplyResources(this.btGenData, "btGenData");
			this.btGenData.Name = "btGenData";
			this.btGenData.UseVisualStyleBackColor = true;
			this.btGenData.Click += new System.EventHandler(this.btGenData_Click);
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// txtLabel
			// 
			resources.ApplyResources(this.txtLabel, "txtLabel");
			this.txtLabel.Name = "txtLabel";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtCommandHistory);
			this.tabPage2.Controls.Add(this.label20);
			this.tabPage2.Controls.Add(this.txtCommand);
			this.tabPage2.Controls.Add(this.btSocketConnect);
			this.tabPage2.Controls.Add(this.lblSocketStatus);
			this.tabPage2.Controls.Add(this.label18);
			this.tabPage2.Controls.Add(this.label17);
			this.tabPage2.Controls.Add(this.txtPort);
			this.tabPage2.Controls.Add(this.txtIP);
			this.tabPage2.Controls.Add(this.label16);
			this.tabPage2.Controls.Add(this.label15);
			this.tabPage2.Controls.Add(this.progressBar2);
			this.tabPage2.Controls.Add(this.lblPrediction);
			this.tabPage2.Controls.Add(this.Thold2);
			this.tabPage2.Controls.Add(this.label13);
			this.tabPage2.Controls.Add(this.label12);
			this.tabPage2.Controls.Add(this.txtTiempo2);
			this.tabPage2.Controls.Add(this.btnDetener);
			this.tabPage2.Controls.Add(this.label11);
			this.tabPage2.Controls.Add(this.btnEscuchar);
			this.tabPage2.Controls.Add(this.levelIndicator2);
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtCommandHistory
			// 
			resources.ApplyResources(this.txtCommandHistory, "txtCommandHistory");
			this.txtCommandHistory.Name = "txtCommandHistory";
			this.txtCommandHistory.ReadOnly = true;
			this.txtCommandHistory.TabStop = false;
			// 
			// label20
			// 
			resources.ApplyResources(this.label20, "label20");
			this.label20.Name = "label20";
			// 
			// txtCommand
			// 
			resources.ApplyResources(this.txtCommand, "txtCommand");
			this.txtCommand.Name = "txtCommand";
			this.txtCommand.Enter += new System.EventHandler(this.txtCommand_Enter);
			this.txtCommand.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCommand_KeyUp);
			this.txtCommand.Leave += new System.EventHandler(this.txtCommand_Leave);
			// 
			// btSocketConnect
			// 
			resources.ApplyResources(this.btSocketConnect, "btSocketConnect");
			this.btSocketConnect.Name = "btSocketConnect";
			this.btSocketConnect.TabStop = false;
			this.btSocketConnect.UseVisualStyleBackColor = true;
			this.btSocketConnect.Click += new System.EventHandler(this.btSocketConnect_Click);
			// 
			// lblSocketStatus
			// 
			resources.ApplyResources(this.lblSocketStatus, "lblSocketStatus");
			this.lblSocketStatus.Name = "lblSocketStatus";
			// 
			// label18
			// 
			resources.ApplyResources(this.label18, "label18");
			this.label18.Name = "label18";
			// 
			// label17
			// 
			resources.ApplyResources(this.label17, "label17");
			this.label17.Name = "label17";
			// 
			// txtPort
			// 
			resources.ApplyResources(this.txtPort, "txtPort");
			this.txtPort.Name = "txtPort";
			this.txtPort.TabStop = false;
			// 
			// txtIP
			// 
			resources.ApplyResources(this.txtIP, "txtIP");
			this.txtIP.Name = "txtIP";
			this.txtIP.TabStop = false;
			// 
			// label16
			// 
			resources.ApplyResources(this.label16, "label16");
			this.label16.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label16.Name = "label16";
			// 
			// label15
			// 
			resources.ApplyResources(this.label15, "label15");
			this.label15.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label15.Name = "label15";
			// 
			// progressBar2
			// 
			resources.ApplyResources(this.progressBar2, "progressBar2");
			this.progressBar2.Name = "progressBar2";
			// 
			// lblPrediction
			// 
			resources.ApplyResources(this.lblPrediction, "lblPrediction");
			this.lblPrediction.Name = "lblPrediction";
			// 
			// Thold2
			// 
			resources.ApplyResources(this.Thold2, "Thold2");
			this.Thold2.Name = "Thold2";
			this.Thold2.TabStop = false;
			this.Thold2.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.Thold2.ValueChanged += new System.EventHandler(this.Thold2_ValueChanged);
			// 
			// label13
			// 
			resources.ApplyResources(this.label13, "label13");
			this.label13.Name = "label13";
			// 
			// label12
			// 
			resources.ApplyResources(this.label12, "label12");
			this.label12.Name = "label12";
			// 
			// txtTiempo2
			// 
			resources.ApplyResources(this.txtTiempo2, "txtTiempo2");
			this.txtTiempo2.Name = "txtTiempo2";
			this.txtTiempo2.TabStop = false;
			// 
			// btnDetener
			// 
			resources.ApplyResources(this.btnDetener, "btnDetener");
			this.btnDetener.Name = "btnDetener";
			this.btnDetener.TabStop = false;
			this.btnDetener.UseVisualStyleBackColor = true;
			this.btnDetener.Click += new System.EventHandler(this.button1_Click);
			// 
			// label11
			// 
			resources.ApplyResources(this.label11, "label11");
			this.label11.Name = "label11";
			// 
			// btnEscuchar
			// 
			resources.ApplyResources(this.btnEscuchar, "btnEscuchar");
			this.btnEscuchar.Name = "btnEscuchar";
			this.btnEscuchar.TabStop = false;
			this.btnEscuchar.UseVisualStyleBackColor = true;
			this.btnEscuchar.Click += new System.EventHandler(this.btnEscuchar_Click);
			// 
			// levelIndicator2
			// 
			this.levelIndicator2.BackColor = System.Drawing.Color.Black;
			this.levelIndicator2.Level = 0;
			resources.ApplyResources(this.levelIndicator2, "levelIndicator2");
			this.levelIndicator2.Maximum = 100;
			this.levelIndicator2.Minimum = 0;
			this.levelIndicator2.Name = "levelIndicator2";
			// 
			// Form1
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.nupHold)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picWorking)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.chartCurves)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Thold2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboWasapiDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btStartRecording;
        private System.Windows.Forms.Button btStopRecording;
        private System.Windows.Forms.ListBox listBoxRecordings;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Button btnPlayExtern;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTiempo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.CheckBox chkThreshold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nupHold;
		private Views.LevelIndicator levelIndicator1;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btPredecir;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox txtLabel;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btRemoveData;
		private System.Windows.Forms.Button btGenData;
		private System.Windows.Forms.Label lblFeatStatus;
		private System.Windows.Forms.ProgressBar progDataGen;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtIteraciones;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtLambda;
		private System.Windows.Forms.PictureBox picWorking;
		private System.Windows.Forms.Label lblNetStatus;
		private System.Windows.Forms.Button btTrain;
        private System.Windows.Forms.TabPage tabPage2;
        private Views.LevelIndicator levelIndicator2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnEscuchar;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.NumericUpDown Thold2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTiempo2;
        private System.Windows.Forms.Label lblPrediction;
		private System.Windows.Forms.Button btSvm;
        private System.Windows.Forms.Button btAddFunction;
        private System.Windows.Forms.TextBox txtFunction;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btRemoveLabel;
        private System.Windows.Forms.Button btAddLabel;
        private System.Windows.Forms.ListBox listLabels;
        private System.Windows.Forms.ListBox listFunctions;
        private System.Windows.Forms.ProgressBar progressBar2;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txtIP;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Button btSocketConnect;
		private System.Windows.Forms.Label lblSocketStatus;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox txtCommand;
		private System.Windows.Forms.TextBox txtCommandHistory;
		private System.Windows.Forms.DataVisualization.Charting.Chart chartCurves;
		private System.Windows.Forms.TextBox txtConsola;
		private System.Windows.Forms.Button btSavePlot;
		private System.Windows.Forms.RadioButton radLambdaSingle;
		private System.Windows.Forms.RadioButton radLambdaSet;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btRemoveFunction;
		private System.Windows.Forms.CheckBox checkPlotTestErr;
		private System.Windows.Forms.CheckBox checkPlotTrainErr;
		private System.Windows.Forms.CheckBox checkPlotTestCost;
		private System.Windows.Forms.CheckBox checkPlotTrainCost;
		private System.Windows.Forms.Label lblTestPredict;

	}
}


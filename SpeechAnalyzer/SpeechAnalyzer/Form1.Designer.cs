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
            this.chkThold = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nupHold = new System.Windows.Forms.NumericUpDown();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btPredecir = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIteraciones = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLambda = new System.Windows.Forms.TextBox();
            this.picWorking = new System.Windows.Forms.PictureBox();
            this.lblNetStatus = new System.Windows.Forms.Label();
            this.btTrain = new System.Windows.Forms.Button();
            this.progDataGen = new System.Windows.Forms.ProgressBar();
            this.lblFeatStatus = new System.Windows.Forms.Label();
            this.btGenFeatures = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btRemoveFeatures = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listLabels = new System.Windows.Forms.ListBox();
            this.btAddLabel = new System.Windows.Forms.Button();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.btRemoveLabel = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Thold2 = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTiempo2 = new System.Windows.Forms.TextBox();
            this.btnDetener = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnEscuchar = new System.Windows.Forms.Button();
            this.lblPrediction = new System.Windows.Forms.Label();
            this.levelIndicator1 = new SpeechAnalyzer.Views.LevelIndicator();
            this.levelIndicator2 = new SpeechAnalyzer.Views.LevelIndicator();
            ((System.ComponentModel.ISupportInitialize)(this.nupHold)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWorking)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Thold2)).BeginInit();
            this.SuspendLayout();
            // 
            // comboWasapiDevices
            // 
            this.comboWasapiDevices.FormattingEnabled = true;
            this.comboWasapiDevices.Location = new System.Drawing.Point(6, 29);
            this.comboWasapiDevices.Name = "comboWasapiDevices";
            this.comboWasapiDevices.Size = new System.Drawing.Size(262, 21);
            this.comboWasapiDevices.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dispositivos de Entrada";
            // 
            // btStartRecording
            // 
            this.btStartRecording.Location = new System.Drawing.Point(7, 172);
            this.btStartRecording.Name = "btStartRecording";
            this.btStartRecording.Size = new System.Drawing.Size(75, 23);
            this.btStartRecording.TabIndex = 4;
            this.btStartRecording.Text = "Grabar";
            this.btStartRecording.UseVisualStyleBackColor = true;
            this.btStartRecording.Click += new System.EventHandler(this.StartRecording_Click);
            // 
            // btStopRecording
            // 
            this.btStopRecording.Location = new System.Drawing.Point(88, 172);
            this.btStopRecording.Name = "btStopRecording";
            this.btStopRecording.Size = new System.Drawing.Size(75, 23);
            this.btStopRecording.TabIndex = 5;
            this.btStopRecording.Text = "Detener";
            this.btStopRecording.UseVisualStyleBackColor = true;
            this.btStopRecording.Click += new System.EventHandler(this.ButtonStopRecording_Click);
            // 
            // listBoxRecordings
            // 
            this.listBoxRecordings.FormattingEnabled = true;
            this.listBoxRecordings.Location = new System.Drawing.Point(330, 13);
            this.listBoxRecordings.Name = "listBoxRecordings";
            this.listBoxRecordings.Size = new System.Drawing.Size(158, 212);
            this.listBoxRecordings.TabIndex = 6;
            this.listBoxRecordings.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 201);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(263, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(503, 201);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(87, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(330, 227);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(87, 23);
            this.btnOpenFolder.TabIndex = 9;
            this.btnOpenFolder.Text = "Abrir Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // btnPlayExtern
            // 
            this.btnPlayExtern.Location = new System.Drawing.Point(503, 172);
            this.btnPlayExtern.Name = "btnPlayExtern";
            this.btnPlayExtern.Size = new System.Drawing.Size(87, 23);
            this.btnPlayExtern.TabIndex = 10;
            this.btnPlayExtern.Text = "Reproducir/E";
            this.btnPlayExtern.UseVisualStyleBackColor = true;
            this.btnPlayExtern.Click += new System.EventHandler(this.btnPlayExtern_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tiempo (s)";
            // 
            // txtTiempo
            // 
            this.txtTiempo.Location = new System.Drawing.Point(106, 65);
            this.txtTiempo.Name = "txtTiempo";
            this.txtTiempo.Size = new System.Drawing.Size(163, 20);
            this.txtTiempo.TabIndex = 12;
            this.txtTiempo.Text = "1.5";
            this.txtTiempo.TextChanged += new System.EventHandler(this.txtTiempo_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Nomre del Archivo";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(106, 104);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(163, 20);
            this.txtNombre.TabIndex = 14;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(503, 13);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(87, 23);
            this.btnPlay.TabIndex = 15;
            this.btnPlay.Text = "Reproducir";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // chkThold
            // 
            this.chkThold.AutoSize = true;
            this.chkThold.Location = new System.Drawing.Point(9, 143);
            this.chkThold.Name = "chkThold";
            this.chkThold.Size = new System.Drawing.Size(73, 17);
            this.chkThold.TabIndex = 16;
            this.chkThold.Text = "Threshold";
            this.chkThold.UseVisualStyleBackColor = true;
            this.chkThold.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Valor Threshold:";
            // 
            // nupHold
            // 
            this.nupHold.Enabled = false;
            this.nupHold.Location = new System.Drawing.Point(193, 140);
            this.nupHold.Name = "nupHold";
            this.nupHold.Size = new System.Drawing.Size(79, 20);
            this.nupHold.TabIndex = 19;
            this.nupHold.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nupHold.ValueChanged += new System.EventHandler(this.nupHold_ValueChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Location = new System.Drawing.Point(6, 227);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(58, 13);
            this.lblStatus.TabIndex = 21;
            this.lblStatus.Text = "Esperando";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(611, 281);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
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
            this.tabPage1.Controls.Add(this.chkThold);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnPlay);
            this.tabPage1.Controls.Add(this.btnOpenFolder);
            this.tabPage1.Controls.Add(this.txtNombre);
            this.tabPage1.Controls.Add(this.btnPlayExtern);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtTiempo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(603, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Grabaciones";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btPredecir
            // 
            this.btPredecir.Location = new System.Drawing.Point(503, 43);
            this.btPredecir.Name = "btPredecir";
            this.btPredecir.Size = new System.Drawing.Size(87, 23);
            this.btPredecir.TabIndex = 22;
            this.btPredecir.Text = "Predecir";
            this.btPredecir.UseVisualStyleBackColor = true;
            this.btPredecir.Click += new System.EventHandler(this.btPredecir_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.txtIteraciones);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.txtLambda);
            this.tabPage3.Controls.Add(this.picWorking);
            this.tabPage3.Controls.Add(this.lblNetStatus);
            this.tabPage3.Controls.Add(this.btTrain);
            this.tabPage3.Controls.Add(this.progDataGen);
            this.tabPage3.Controls.Add(this.lblFeatStatus);
            this.tabPage3.Controls.Add(this.btGenFeatures);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.btRemoveFeatures);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.listLabels);
            this.tabPage3.Controls.Add(this.btAddLabel);
            this.tabPage3.Controls.Add(this.txtLabel);
            this.tabPage3.Controls.Add(this.btRemoveLabel);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(603, 255);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Machine Learning";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(387, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 17);
            this.label10.TabIndex = 40;
            this.label10.Text = "3. Red Neural";
            // 
            // txtIteraciones
            // 
            this.txtIteraciones.Location = new System.Drawing.Point(452, 51);
            this.txtIteraciones.Name = "txtIteraciones";
            this.txtIteraciones.Size = new System.Drawing.Size(71, 20);
            this.txtIteraciones.TabIndex = 39;
            this.txtIteraciones.Text = "10000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(383, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Iteraciones";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(386, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 20);
            this.label5.TabIndex = 37;
            this.label5.Text = "λ";
            // 
            // txtLambda
            // 
            this.txtLambda.Location = new System.Drawing.Point(452, 25);
            this.txtLambda.Name = "txtLambda";
            this.txtLambda.Size = new System.Drawing.Size(71, 20);
            this.txtLambda.TabIndex = 36;
            this.txtLambda.Text = "0.1";
            // 
            // picWorking
            // 
            this.picWorking.Image = global::SpeechAnalyzer.Properties.Resources._120x120px_LS_482a3971_multilockon;
            this.picWorking.Location = new System.Drawing.Point(395, 157);
            this.picWorking.Name = "picWorking";
            this.picWorking.Size = new System.Drawing.Size(128, 91);
            this.picWorking.TabIndex = 35;
            this.picWorking.TabStop = false;
            // 
            // lblNetStatus
            // 
            this.lblNetStatus.Location = new System.Drawing.Point(383, 104);
            this.lblNetStatus.Name = "lblNetStatus";
            this.lblNetStatus.Size = new System.Drawing.Size(183, 23);
            this.lblNetStatus.TabIndex = 34;
            this.lblNetStatus.Text = "...";
            // 
            // btTrain
            // 
            this.btTrain.Location = new System.Drawing.Point(452, 77);
            this.btTrain.Name = "btTrain";
            this.btTrain.Size = new System.Drawing.Size(87, 24);
            this.btTrain.TabIndex = 33;
            this.btTrain.Text = "Entrenar";
            this.btTrain.UseVisualStyleBackColor = true;
            this.btTrain.Click += new System.EventHandler(this.btTrain_Click);
            // 
            // progDataGen
            // 
            this.progDataGen.Location = new System.Drawing.Point(174, 70);
            this.progDataGen.Name = "progDataGen";
            this.progDataGen.Size = new System.Drawing.Size(156, 23);
            this.progDataGen.TabIndex = 11;
            // 
            // lblFeatStatus
            // 
            this.lblFeatStatus.AutoSize = true;
            this.lblFeatStatus.ForeColor = System.Drawing.Color.Purple;
            this.lblFeatStatus.Location = new System.Drawing.Point(208, 25);
            this.lblFeatStatus.Name = "lblFeatStatus";
            this.lblFeatStatus.Size = new System.Drawing.Size(43, 13);
            this.lblFeatStatus.TabIndex = 10;
            this.lblFeatStatus.Text = "(Status)";
            // 
            // btGenFeatures
            // 
            this.btGenFeatures.Location = new System.Drawing.Point(255, 41);
            this.btGenFeatures.Name = "btGenFeatures";
            this.btGenFeatures.Size = new System.Drawing.Size(75, 23);
            this.btGenFeatures.TabIndex = 9;
            this.btGenFeatures.Text = "Generar";
            this.btGenFeatures.UseVisualStyleBackColor = true;
            this.btGenFeatures.Click += new System.EventHandler(this.btGenFeatures_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(174, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Status";
            // 
            // btRemoveFeatures
            // 
            this.btRemoveFeatures.Location = new System.Drawing.Point(174, 41);
            this.btRemoveFeatures.Name = "btRemoveFeatures";
            this.btRemoveFeatures.Size = new System.Drawing.Size(75, 23);
            this.btRemoveFeatures.TabIndex = 7;
            this.btRemoveFeatures.Text = "Eliminar";
            this.btRemoveFeatures.UseVisualStyleBackColor = true;
            this.btRemoveFeatures.Click += new System.EventHandler(this.btRemoveFeatures_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(171, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 6;
            this.label8.Text = "2. Datos";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "1. Etiquetas";
            // 
            // listLabels
            // 
            this.listLabels.FormattingEnabled = true;
            this.listLabels.Location = new System.Drawing.Point(12, 23);
            this.listLabels.Name = "listLabels";
            this.listLabels.Size = new System.Drawing.Size(133, 173);
            this.listLabels.TabIndex = 4;
            this.listLabels.SelectedIndexChanged += new System.EventHandler(this.listLabels_SelectedIndexChanged);
            // 
            // btAddLabel
            // 
            this.btAddLabel.Enabled = false;
            this.btAddLabel.Location = new System.Drawing.Point(82, 225);
            this.btAddLabel.Name = "btAddLabel";
            this.btAddLabel.Size = new System.Drawing.Size(63, 23);
            this.btAddLabel.TabIndex = 3;
            this.btAddLabel.Text = "Agregar";
            this.btAddLabel.UseVisualStyleBackColor = true;
            this.btAddLabel.Click += new System.EventHandler(this.btAddLabel_Click);
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(8, 228);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(68, 20);
            this.txtLabel.TabIndex = 2;
            this.txtLabel.TextChanged += new System.EventHandler(this.txtLabel_TextChanged);
            // 
            // btRemoveLabel
            // 
            this.btRemoveLabel.Enabled = false;
            this.btRemoveLabel.Location = new System.Drawing.Point(82, 202);
            this.btRemoveLabel.Name = "btRemoveLabel";
            this.btRemoveLabel.Size = new System.Drawing.Size(63, 23);
            this.btRemoveLabel.TabIndex = 1;
            this.btRemoveLabel.Text = "Eliminar";
            this.btRemoveLabel.UseVisualStyleBackColor = true;
            this.btRemoveLabel.Click += new System.EventHandler(this.btRemoveLabel_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblPrediction);
            this.tabPage2.Controls.Add(this.Thold2);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.txtTiempo2);
            this.tabPage2.Controls.Add(this.btnDetener);
            this.tabPage2.Controls.Add(this.levelIndicator2);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.btnEscuchar);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(603, 255);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Escuchar";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Thold2
            // 
            this.Thold2.Location = new System.Drawing.Point(432, 40);
            this.Thold2.Name = "Thold2";
            this.Thold2.Size = new System.Drawing.Size(79, 20);
            this.Thold2.TabIndex = 26;
            this.Thold2.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.Thold2.ValueChanged += new System.EventHandler(this.Thold2_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(331, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Valor Threshold:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(221, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Tiempo:";
            // 
            // txtTiempo2
            // 
            this.txtTiempo2.Location = new System.Drawing.Point(272, 39);
            this.txtTiempo2.Name = "txtTiempo2";
            this.txtTiempo2.Size = new System.Drawing.Size(42, 20);
            this.txtTiempo2.TabIndex = 23;
            this.txtTiempo2.Text = "1.25";
            this.txtTiempo2.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnDetener
            // 
            this.btnDetener.Location = new System.Drawing.Point(130, 37);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(75, 23);
            this.btnDetener.TabIndex = 22;
            this.btnDetener.Text = "Detener";
            this.btnDetener.UseVisualStyleBackColor = true;
            this.btnDetener.Click += new System.EventHandler(this.button1_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(46, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Predicción:";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // btnEscuchar
            // 
            this.btnEscuchar.Location = new System.Drawing.Point(49, 37);
            this.btnEscuchar.Name = "btnEscuchar";
            this.btnEscuchar.Size = new System.Drawing.Size(75, 23);
            this.btnEscuchar.TabIndex = 0;
            this.btnEscuchar.Text = "Escuchar";
            this.btnEscuchar.UseVisualStyleBackColor = true;
            this.btnEscuchar.Click += new System.EventHandler(this.btnEscuchar_Click);
            // 
            // lblPrediction
            // 
            this.lblPrediction.AutoSize = true;
            this.lblPrediction.Location = new System.Drawing.Point(112, 81);
            this.lblPrediction.Name = "lblPrediction";
            this.lblPrediction.Size = new System.Drawing.Size(51, 13);
            this.lblPrediction.TabIndex = 27;
            this.lblPrediction.Text = "unknown";
            // 
            // levelIndicator1
            // 
            this.levelIndicator1.BackColor = System.Drawing.Color.Black;
            this.levelIndicator1.Level = 0;
            this.levelIndicator1.Location = new System.Drawing.Point(278, 29);
            this.levelIndicator1.Maximum = 100;
            this.levelIndicator1.Minimum = 0;
            this.levelIndicator1.Name = "levelIndicator1";
            this.levelIndicator1.Size = new System.Drawing.Size(26, 195);
            this.levelIndicator1.TabIndex = 20;
            // 
            // levelIndicator2
            // 
            this.levelIndicator2.BackColor = System.Drawing.Color.Black;
            this.levelIndicator2.Level = 0;
            this.levelIndicator2.Location = new System.Drawing.Point(8, 37);
            this.levelIndicator2.Maximum = 100;
            this.levelIndicator2.Minimum = 0;
            this.levelIndicator2.Name = "levelIndicator2";
            this.levelIndicator2.Size = new System.Drawing.Size(26, 195);
            this.levelIndicator2.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 281);
            this.Controls.Add(this.tabControl1);
            this.MaximumSize = new System.Drawing.Size(630, 320);
            this.Name = "Form1";
            this.Text = "Eliminar";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nupHold)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWorking)).EndInit();
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
        private System.Windows.Forms.CheckBox chkThold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nupHold;
		private Views.LevelIndicator levelIndicator1;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btPredecir;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button btRemoveLabel;
		private System.Windows.Forms.Button btAddLabel;
		private System.Windows.Forms.TextBox txtLabel;
		private System.Windows.Forms.ListBox listLabels;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btRemoveFeatures;
		private System.Windows.Forms.Button btGenFeatures;
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

	}
}


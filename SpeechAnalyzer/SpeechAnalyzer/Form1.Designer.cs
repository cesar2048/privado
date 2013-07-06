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
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtIteraciones = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtIntentos = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtLambda = new System.Windows.Forms.TextBox();
			this.txtNetConsole = new System.Windows.Forms.TextBox();
			this.picWorking = new System.Windows.Forms.PictureBox();
			this.lblNetStatus = new System.Windows.Forms.Label();
			this.btGen = new System.Windows.Forms.Button();
			this.levelIndicator1 = new SpeechAnalyzer.Views.LevelIndicator();
			((System.ComponentModel.ISupportInitialize)(this.nupHold)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picWorking)).BeginInit();
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
			this.btnOpenFolder.Location = new System.Drawing.Point(503, 172);
			this.btnOpenFolder.Name = "btnOpenFolder";
			this.btnOpenFolder.Size = new System.Drawing.Size(87, 23);
			this.btnOpenFolder.TabIndex = 9;
			this.btnOpenFolder.Text = "abrir";
			this.btnOpenFolder.UseVisualStyleBackColor = true;
			this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
			// 
			// btnPlayExtern
			// 
			this.btnPlayExtern.Location = new System.Drawing.Point(503, 143);
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
			this.btnPlay.Location = new System.Drawing.Point(503, 8);
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
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(611, 282);
			this.tabControl1.TabIndex = 23;
			// 
			// tabPage1
			// 
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
			this.tabPage1.Size = new System.Drawing.Size(603, 256);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Grabaciones";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtIteraciones);
			this.tabPage2.Controls.Add(this.label7);
			this.tabPage2.Controls.Add(this.txtIntentos);
			this.tabPage2.Controls.Add(this.label6);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.txtLambda);
			this.tabPage2.Controls.Add(this.txtNetConsole);
			this.tabPage2.Controls.Add(this.picWorking);
			this.tabPage2.Controls.Add(this.lblNetStatus);
			this.tabPage2.Controls.Add(this.btGen);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(603, 256);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Red Neural";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtIteraciones
			// 
			this.txtIteraciones.Location = new System.Drawing.Point(77, 103);
			this.txtIteraciones.Name = "txtIteraciones";
			this.txtIteraciones.Size = new System.Drawing.Size(71, 20);
			this.txtIteraciones.TabIndex = 32;
			this.txtIteraciones.Text = "10000";
			this.txtIteraciones.TextChanged += new System.EventHandler(this.txtIteraciones_TextChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 106);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 13);
			this.label7.TabIndex = 31;
			this.label7.Text = "Iteraciones";
			// 
			// txtIntentos
			// 
			this.txtIntentos.Location = new System.Drawing.Point(77, 77);
			this.txtIntentos.Name = "txtIntentos";
			this.txtIntentos.Size = new System.Drawing.Size(71, 20);
			this.txtIntentos.TabIndex = 30;
			this.txtIntentos.Text = "100";
			this.txtIntentos.TextChanged += new System.EventHandler(this.txtIntentos_TextChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 80);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 13);
			this.label6.TabIndex = 29;
			this.label6.Text = "Repetir";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(11, 51);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(18, 20);
			this.label5.TabIndex = 28;
			this.label5.Text = "λ";
			// 
			// txtLambda
			// 
			this.txtLambda.Location = new System.Drawing.Point(77, 51);
			this.txtLambda.Name = "txtLambda";
			this.txtLambda.Size = new System.Drawing.Size(71, 20);
			this.txtLambda.TabIndex = 27;
			this.txtLambda.TextChanged += new System.EventHandler(this.txtLambda_TextChanged);
			// 
			// txtNetConsole
			// 
			this.txtNetConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtNetConsole.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.txtNetConsole.Location = new System.Drawing.Point(235, 7);
			this.txtNetConsole.Multiline = true;
			this.txtNetConsole.Name = "txtNetConsole";
			this.txtNetConsole.ReadOnly = true;
			this.txtNetConsole.Size = new System.Drawing.Size(362, 241);
			this.txtNetConsole.TabIndex = 26;
			// 
			// picWorking
			// 
			this.picWorking.Image = global::SpeechAnalyzer.Properties.Resources._120x120px_LS_482a3971_multilockon;
			this.picWorking.Location = new System.Drawing.Point(11, 159);
			this.picWorking.Name = "picWorking";
			this.picWorking.Size = new System.Drawing.Size(128, 91);
			this.picWorking.TabIndex = 25;
			this.picWorking.TabStop = false;
			// 
			// lblNetStatus
			// 
			this.lblNetStatus.Location = new System.Drawing.Point(12, 143);
			this.lblNetStatus.Name = "lblNetStatus";
			this.lblNetStatus.Size = new System.Drawing.Size(193, 13);
			this.lblNetStatus.TabIndex = 24;
			this.lblNetStatus.Text = "...";
			// 
			// btGen
			// 
			this.btGen.Location = new System.Drawing.Point(8, 6);
			this.btGen.Name = "btGen";
			this.btGen.Size = new System.Drawing.Size(118, 38);
			this.btGen.TabIndex = 23;
			this.btGen.Text = "Compute Features && Neural network";
			this.btGen.UseVisualStyleBackColor = true;
			this.btGen.Click += new System.EventHandler(this.btGen_Click);
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(611, 282);
			this.Controls.Add(this.tabControl1);
			this.MaximumSize = new System.Drawing.Size(630, 320);
			this.Name = "Form1";
			this.Text = "Eliminar";
			((System.ComponentModel.ISupportInitialize)(this.nupHold)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picWorking)).EndInit();
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
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label lblNetStatus;
		private System.Windows.Forms.Button btGen;
		private System.Windows.Forms.PictureBox picWorking;
		private System.Windows.Forms.TextBox txtNetConsole;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtLambda;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtIntentos;
		private System.Windows.Forms.TextBox txtIteraciones;
		private System.Windows.Forms.Label label7;

	}
}


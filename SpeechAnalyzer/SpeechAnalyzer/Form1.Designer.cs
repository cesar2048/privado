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
            this.components = new System.ComponentModel.Container();
            this.btGen = new System.Windows.Forms.Button();
            this.denseMatrixBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataTheta1 = new System.Windows.Forms.DataGridView();
            this.comboWasapiDevices = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StartRecording = new System.Windows.Forms.Button();
            this.ButtonStopRecording = new System.Windows.Forms.Button();
            this.listBoxRecordings = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTiempo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.chkThold = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nupHold = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.denseMatrixBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTheta1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupHold)).BeginInit();
            this.SuspendLayout();
            // 
            // btGen
            // 
            this.btGen.Location = new System.Drawing.Point(13, 31);
            this.btGen.Name = "btGen";
            this.btGen.Size = new System.Drawing.Size(75, 42);
            this.btGen.TabIndex = 0;
            this.btGen.Text = "Generate Features";
            this.btGen.UseVisualStyleBackColor = true;
            this.btGen.Click += new System.EventHandler(this.btGen_Click);
            // 
            // denseMatrixBindingSource
            // 
            this.denseMatrixBindingSource.DataSource = typeof(MathNet.Numerics.LinearAlgebra.Double.DenseMatrix);
            // 
            // dataTheta1
            // 
            this.dataTheta1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTheta1.Location = new System.Drawing.Point(94, 31);
            this.dataTheta1.Name = "dataTheta1";
            this.dataTheta1.Size = new System.Drawing.Size(400, 42);
            this.dataTheta1.TabIndex = 1;
            // 
            // comboWasapiDevices
            // 
            this.comboWasapiDevices.FormattingEnabled = true;
            this.comboWasapiDevices.Location = new System.Drawing.Point(12, 148);
            this.comboWasapiDevices.Name = "comboWasapiDevices";
            this.comboWasapiDevices.Size = new System.Drawing.Size(262, 21);
            this.comboWasapiDevices.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dispositivos de Entrada";
            // 
            // StartRecording
            // 
            this.StartRecording.Location = new System.Drawing.Point(13, 291);
            this.StartRecording.Name = "StartRecording";
            this.StartRecording.Size = new System.Drawing.Size(75, 23);
            this.StartRecording.TabIndex = 4;
            this.StartRecording.Text = "Grabar";
            this.StartRecording.UseVisualStyleBackColor = true;
            this.StartRecording.Click += new System.EventHandler(this.StartRecording_Click);
            // 
            // ButtonStopRecording
            // 
            this.ButtonStopRecording.Location = new System.Drawing.Point(94, 291);
            this.ButtonStopRecording.Name = "ButtonStopRecording";
            this.ButtonStopRecording.Size = new System.Drawing.Size(75, 23);
            this.ButtonStopRecording.TabIndex = 5;
            this.ButtonStopRecording.Text = "Detener";
            this.ButtonStopRecording.UseVisualStyleBackColor = true;
            this.ButtonStopRecording.Click += new System.EventHandler(this.ButtonStopRecording_Click);
            // 
            // listBoxRecordings
            // 
            this.listBoxRecordings.FormattingEnabled = true;
            this.listBoxRecordings.Location = new System.Drawing.Point(336, 132);
            this.listBoxRecordings.Name = "listBoxRecordings";
            this.listBoxRecordings.Size = new System.Drawing.Size(158, 212);
            this.listBoxRecordings.TabIndex = 6;
            this.listBoxRecordings.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 320);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(263, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(509, 320);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(87, 23);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Text = "Eliminar";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Location = new System.Drawing.Point(509, 291);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(87, 23);
            this.buttonOpenFolder.TabIndex = 9;
            this.buttonOpenFolder.Text = "abrir";
            this.buttonOpenFolder.UseVisualStyleBackColor = true;
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(509, 262);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(87, 23);
            this.buttonPlay.TabIndex = 10;
            this.buttonPlay.Text = "Reproducir/E";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tiempo (s)";
            // 
            // txtTiempo
            // 
            this.txtTiempo.Location = new System.Drawing.Point(112, 184);
            this.txtTiempo.Name = "txtTiempo";
            this.txtTiempo.Size = new System.Drawing.Size(163, 20);
            this.txtTiempo.TabIndex = 12;
            this.txtTiempo.Text = "30";
            this.txtTiempo.TextChanged += new System.EventHandler(this.txtTiempo_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Nomre del Archivo";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(112, 223);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(163, 20);
            this.txtNombre.TabIndex = 14;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(509, 127);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(87, 23);
            this.btnPlay.TabIndex = 15;
            this.btnPlay.Text = "Reproducir";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // chkThold
            // 
            this.chkThold.AutoSize = true;
            this.chkThold.Location = new System.Drawing.Point(15, 262);
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
            this.label4.Location = new System.Drawing.Point(109, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Valor Threshold:";
            // 
            // nupHold
            // 
            this.nupHold.Enabled = false;
            this.nupHold.Location = new System.Drawing.Point(199, 259);
            this.nupHold.Name = "nupHold";
            this.nupHold.Size = new System.Drawing.Size(79, 20);
            this.nupHold.TabIndex = 19;
            this.nupHold.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nupHold.ValueChanged += new System.EventHandler(this.nupHold_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 371);
            this.Controls.Add(this.nupHold);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkThold);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTiempo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonOpenFolder);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBoxRecordings);
            this.Controls.Add(this.ButtonStopRecording);
            this.Controls.Add(this.StartRecording);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboWasapiDevices);
            this.Controls.Add(this.dataTheta1);
            this.Controls.Add(this.btGen);
            this.Name = "Form1";
            this.Text = "Eliminar";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.denseMatrixBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTheta1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupHold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btGen;
		private System.Windows.Forms.BindingSource denseMatrixBindingSource;
		private System.Windows.Forms.DataGridView dataTheta1;
        private System.Windows.Forms.ComboBox comboWasapiDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartRecording;
        private System.Windows.Forms.Button ButtonStopRecording;
        private System.Windows.Forms.ListBox listBoxRecordings;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonOpenFolder;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTiempo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.CheckBox chkThold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nupHold;

	}
}


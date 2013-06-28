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
			this.btGen = new System.Windows.Forms.Button();
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
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.btGen);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btGen;

	}
}


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
			((System.ComponentModel.ISupportInitialize)(this.denseMatrixBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTheta1)).BeginInit();
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
			this.dataTheta1.Location = new System.Drawing.Point(13, 79);
			this.dataTheta1.Name = "dataTheta1";
			this.dataTheta1.Size = new System.Drawing.Size(240, 150);
			this.dataTheta1.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(530, 353);
			this.Controls.Add(this.dataTheta1);
			this.Controls.Add(this.btGen);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.denseMatrixBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTheta1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btGen;
		private System.Windows.Forms.BindingSource denseMatrixBindingSource;
		private System.Windows.Forms.DataGridView dataTheta1;

	}
}


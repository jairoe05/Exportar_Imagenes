namespace Save_Pictures
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPicture = new System.Windows.Forms.Button();
            this.pgBarCarga = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnPicture
            // 
            this.btnPicture.Location = new System.Drawing.Point(30, 62);
            this.btnPicture.Name = "btnPicture";
            this.btnPicture.Size = new System.Drawing.Size(136, 23);
            this.btnPicture.TabIndex = 0;
            this.btnPicture.Text = "Exportar Imagenes";
            this.btnPicture.UseVisualStyleBackColor = true;
            this.btnPicture.Click += new System.EventHandler(this.btnPicture_Click);
            // 
            // pgBarCarga
            // 
            this.pgBarCarga.Location = new System.Drawing.Point(12, 110);
            this.pgBarCarga.Name = "pgBarCarga";
            this.pgBarCarga.Size = new System.Drawing.Size(186, 13);
            this.pgBarCarga.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 133);
            this.Controls.Add(this.pgBarCarga);
            this.Controls.Add(this.btnPicture);
            this.Name = "Form1";
            this.Text = "Guardar Imagenes en LocalHost";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPicture;
        private System.Windows.Forms.ProgressBar pgBarCarga;
    }
}


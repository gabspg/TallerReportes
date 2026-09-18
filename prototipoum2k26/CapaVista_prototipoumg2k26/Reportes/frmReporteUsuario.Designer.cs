namespace CapaVista_prototipoumg2k26.Reportes
{
    partial class frmReporteUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewUsuario = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewUsuario
            // 
            this.reportViewUsuario.Location = new System.Drawing.Point(12, 12);
            this.reportViewUsuario.Name = "reportViewUsuario";
            this.reportViewUsuario.ServerReport.BearerToken = null;
            this.reportViewUsuario.Size = new System.Drawing.Size(1426, 604);
            this.reportViewUsuario.TabIndex = 0;
            // 
            // frmReporteUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1460, 628);
            this.Controls.Add(this.reportViewUsuario);
            this.Name = "frmReporteUsuario";
            this.Text = "frmReporteUsuario";
            this.Load += new System.EventHandler(this.frmReporteUsuario_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewUsuario;
    }
}
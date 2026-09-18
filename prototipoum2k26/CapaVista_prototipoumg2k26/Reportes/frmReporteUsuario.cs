using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Se llama a la capa Controlador.
using CapaControlador_prototipoumg2k26;

namespace CapaVista_prototipoumg2k26.Reportes
{
    
    public partial class frmReporteUsuario : Form
    {
        // Se crea un objeto de ModeloUsuario para obtener la información de los usuarios.
        private ModeloUsuario usuario = new ModeloUsuario();
        public frmReporteUsuario()
        {
            InitializeComponent();
        }

        private void frmReporteUsuario_Load(object sender, EventArgs e)
        { 
            // Se obtienen todos los usuarios y se utilizan como datos para el reporte.
            ReportDataSource reportDataSource = new ReportDataSource("ReporteUsuario", usuario.GetAll());
            
            // Se indica la ubicación del archivo ReporteUsuario.rdlc
            reportViewUsuario.LocalReport.ReportEmbeddedResource = "CapaVista_prototipoumg2k26.Reportes.ReporteUsuario.rdlc";

            // Se limpian las fuentes de datos anteriores.
            reportViewUsuario.LocalReport.DataSources.Clear();

            // Se agregan los datos de los usuarios al reporte.
            reportViewUsuario.LocalReport.DataSources.Add(reportDataSource);

            // Se actualiza el reporte para mostrar la información.
            this.reportViewUsuario.RefreshReport();
        }
    }
}

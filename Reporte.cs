using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Linq;

namespace RRHH
{
    public partial class Reporte : Form
    {
        private DataTable datosEmpleado;

        public Reporte(DataTable datos)
        {
            InitializeComponent();
            datosEmpleado = datos;
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            // Verificar si el DataTable tiene datos antes de configurarlo
            if (datosEmpleado == null || datosEmpleado.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para mostrar en el reporte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Configurar el ReportViewer
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            // Verificar si el recurso embebido existe
            if (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Contains("RRHH.Curriculum_Vitae.rdlc"))
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "RRHH.Curriculum_Vitae.rdlc";
            }
            else
            {
                MessageBox.Show("El archivo de informe 'Curriculum_Vitae.rdlc' no está embebido como recurso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", datosEmpleado));
            reportViewer1.RefreshReport();
        }

        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = reportViewer1.LocalReport.Render("PDF");
                string carpetaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string archivoPDF = Path.Combine(carpetaDocumentos, $"CV_{DateTime.Now.Ticks}.pdf");
                File.WriteAllBytes(archivoPDF, bytes);
                MessageBox.Show($"El archivo PDF se ha guardado en: {archivoPDF}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
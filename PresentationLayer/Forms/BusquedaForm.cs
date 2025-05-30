﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;


namespace RRHH
{
    public partial class BusquedaForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-RBBHGTO\\SQLEXPRESS;Initial Catalog=RRHH;User ID=sa;Password=wasawasa;TrustServerCertificate=True";

        public BusquedaForm()
        {
            InitializeComponent();
            CargarCriterios(); // Carga los criterios al iniciar el formulario
        }

        private void CargarCriterios()
        {
            cmbCriterio.Items.Clear();
            cmbCriterio.Items.AddRange(new object[]
            {
                "NombreCompleto",   // Nombre del colaborador
                "Departamento",     // Departamento del colaborador
                "Habilidad",        // Habilidades asociadas
                "Competencia",      // Competencias asociadas
                "Email",            // Dirección de correo electrónico
                "Telefono",         // Número de teléfono
                "FechaIngreso",     // Fecha de ingreso
                "EstadoActivo"      // Estado (activo/inactivo)
            });
            cmbCriterio.SelectedIndex = 0; // Selección predeterminada
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario de búsqueda
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = cmbCriterio.SelectedItem.ToString();
            string valor = txtBusqueda.Text.Trim();

            if (string.IsNullOrEmpty(valor))
            {
                MessageBox.Show("Por favor, ingrese un valor para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new ConexionBD().AbrirConexion())
            {
                // Consulta SQL mejorada para evitar duplicados
                string query = @"
        SELECT c.ColaboradorID AS ID, 
               c.NombreCompleto AS Nombre, 
               c.Departamento, 
               c.Email, 
               c.Telefono, 
               c.FechaIngreso, 
               c.EstadoActivo, 
               c.Foto,
               (
                   SELECT STRING_AGG(fa.Institucion + ' - ' + fa.Titulo, '; ')
                   FROM FormacionAcademica fa
                   WHERE fa.ColaboradorID = c.ColaboradorID
               ) AS FormacionAcademica,
               (
                   SELECT STRING_AGG(ep.Empresa + ' - ' + ep.Puesto, '; ')
                   FROM ExperienciaProfesional ep
                   WHERE ep.ColaboradorID = c.ColaboradorID
               ) AS ExperienciaProfesional,
               (
                   SELECT STRING_AGG(h.Habilidad, ', ')
                   FROM Habilidades h
                   WHERE h.ColaboradorID = c.ColaboradorID
               ) AS Habilidades,
               (
                   SELECT STRING_AGG(comp.Competencia, ', ')
                   FROM Competencias comp
                   WHERE comp.ColaboradorID = c.ColaboradorID
               ) AS Competencias,
               (
                   SELECT STRING_AGG(ref.Nombre + ' (' + ref.TipoReferencia + ')', ', ')
                   FROM Referencias ref
                   WHERE ref.ColaboradorID = c.ColaboradorID
               ) AS Referencias
        FROM Colaboradores c
        WHERE c." + criterio + @" LIKE @Valor";

                // Configuración del comando SQL
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Valor", "%" + valor + "%");

                // Llenar el DataTable con los resultados
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Asignar datos al DataGridView
                dgvResultados.DataSource = dt;

                // Ocultar la columna Foto en el DataGridView si existe
                if (dgvResultados.Columns.Contains("Foto"))
                {
                    dgvResultados.Columns["Foto"].Visible = false;
                }

                // Configurar encabezados de columnas
                if (dgvResultados.Columns.Contains("FormacionAcademica"))
                    dgvResultados.Columns["FormacionAcademica"].HeaderText = "Formación Académica";

                if (dgvResultados.Columns.Contains("ExperienciaProfesional"))
                    dgvResultados.Columns["ExperienciaProfesional"].HeaderText = "Experiencia Profesional";

                if (dgvResultados.Columns.Contains("Habilidades"))
                    dgvResultados.Columns["Habilidades"].HeaderText = "Habilidades";

                if (dgvResultados.Columns.Contains("Competencias"))
                    dgvResultados.Columns["Competencias"].HeaderText = "Competencias";

                if (dgvResultados.Columns.Contains("Referencias"))
                    dgvResultados.Columns["Referencias"].HeaderText = "Referencias";
            }
        }



        private void dgvResultados_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResultados.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dgvResultados.SelectedRows[0];

                if (filaSeleccionada.Cells["Foto"].Value != DBNull.Value)
                {
                    byte[] fotoBytes = (byte[])filaSeleccionada.Cells["Foto"].Value;
                    using (MemoryStream ms = new MemoryStream(fotoBytes))
                    {
                        pbFoto.Image = System.Drawing.Image.FromStream(ms); // Cargar la imagen en el PictureBox
                    }
                }
                else
                {
                    pbFoto.Image = null; // Limpiar el PictureBox si no hay foto
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvResultados.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow filaSeleccionada = dgvResultados.SelectedRows[0];

                    // Crear un DataTable para los datos del empleado
                    DataTable dt = new DataTable();
                    dt.Columns.Add("NombreCompleto", typeof(string));
                    dt.Columns.Add("Email", typeof(string));
                    dt.Columns.Add("TelefonoMovil", typeof(string));
                    dt.Columns.Add("FechaIngreso", typeof(string));
                    dt.Columns.Add("EstadoActivo", typeof(bool));
                    dt.Columns.Add("FormacionAcademica", typeof(string));
                    dt.Columns.Add("ExperienciaProfesional", typeof(string));
                    dt.Columns.Add("Habilidad", typeof(string));
                    dt.Columns.Add("Competencias", typeof(string));
                    dt.Columns.Add("Referencias", typeof(string));
                    dt.Columns.Add("Departamento", typeof(string));
                    dt.Columns.Add("Foto", typeof(byte[]));

                    // Llenar el DataTable con los datos de la fila seleccionada
                    DataRow row = dt.NewRow();
                    row["NombreCompleto"] = filaSeleccionada.Cells["Nombre"].Value?.ToString() ?? "Sin nombre";
                    row["Departamento"] = filaSeleccionada.Cells["Departamento"].Value?.ToString() ?? "Sin departamento";
                    row["Email"] = filaSeleccionada.Cells["Email"].Value?.ToString() ?? "Sin email";
                    row["TelefonoMovil"] = filaSeleccionada.Cells["Telefono"].Value?.ToString() ?? "Sin teléfono";
                    row["FechaIngreso"] = filaSeleccionada.Cells["FechaIngreso"].Value?.ToString() ?? "Sin fecha";
                    row["EstadoActivo"] = filaSeleccionada.Cells["EstadoActivo"].Value != DBNull.Value &&
                                          Convert.ToBoolean(filaSeleccionada.Cells["EstadoActivo"].Value);
                    row["FormacionAcademica"] = filaSeleccionada.Cells["FormacionAcademica"].Value?.ToString() ?? "No especificada";
                    row["ExperienciaProfesional"] = filaSeleccionada.Cells["ExperienciaProfesional"].Value?.ToString() ?? "No especificada";
                    row["Habilidad"] = filaSeleccionada.Cells["Habilidades"].Value?.ToString() ?? "No especificadas";
                    row["Competencias"] = filaSeleccionada.Cells["Competencias"].Value?.ToString() ?? "No especificadas";
                    row["Referencias"] = filaSeleccionada.Cells["Referencias"].Value?.ToString() ?? "No especificadas";

                    // Manejo de la foto desde la base de datos
                    row["Foto"] = filaSeleccionada.Cells["Foto"].Value ?? DBNull.Value; // Ajusta "Foto" al nombre real de la columna

                    dt.Rows.Add(row);

                    // Abrir el formulario Reporte y pasar el DataTable
                    Reporte formReporte = new Reporte(dt);
                    formReporte.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir el reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro para imprimir.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}

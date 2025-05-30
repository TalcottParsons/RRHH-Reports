﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using static RRHH.LoginForm;



namespace RRHH
{
    public partial class ColaboradoresForm : Form
    {
        // Constructor
        public ColaboradoresForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(ColaboradoresForm_Load);
            dgvColaboradores.SelectionChanged += new EventHandler(dgvColaboradores_SelectionChanged);
        }

        // Evento que se ejecuta al cargar el formulario
        private void ColaboradoresForm_Load(object sender, EventArgs e)
        {
            CargarColaboradores();
        }


        // Método para cargar colaboradores al DataGridView
        private void CargarColaboradores()
        {
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "SELECT ColaboradorID, NombreCompleto, Telefono, Email, Departamento, EstadoActivo, Objetivo FROM Colaboradores";
                SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dgvColaboradores.DataSource = dt;
            }
        }

        // Evento que se dispara al hacer clic en una celda del DataGridView de colaboradores.
        // Carga los datos del colaborador seleccionado en los campos correspondientes del formulario.
        private void dgvColaboradores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificar que no se haga clic en una fila vacía
                if (e.RowIndex >= 0 && dgvColaboradores.Rows[e.RowIndex].Cells["ColaboradorID"].Value != null)
                {
                    // Obtener los valores de la fila seleccionada
                    DataGridViewRow filaSeleccionada = dgvColaboradores.Rows[e.RowIndex];

                    txtNombreCompleto.Text = filaSeleccionada.Cells["NombreCompleto"].Value.ToString();
                    txtTelefono.Text = filaSeleccionada.Cells["Telefono"].Value.ToString();
                    txtEmail.Text = filaSeleccionada.Cells["Email"].Value.ToString();
                    txtDepartamento.Text = filaSeleccionada.Cells["Departamento"].Value.ToString();
                    txtObjetivo.Text = filaSeleccionada.Cells["Objetivo"].Value.ToString();

                    // Omitimos la lógica de la foto, ya que no es necesaria
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del colaborador: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que se dispara al hacer clic en una celda del DataGridView de formación académica.
        // Carga los datos de la formación seleccionada en los campos correspondientes del formulario:
        // Institución, Título, Año de inicio y Año de fin.
        private void dgvFormacionAcademica_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvFormacionAcademica.Rows[e.RowIndex].Cells["FormacionID"].Value != null)
                {
                    DataGridViewRow filaSeleccionada = dgvFormacionAcademica.Rows[e.RowIndex];

                    txtInstitucion.Text = filaSeleccionada.Cells["Institucion"].Value.ToString();
                    txtTitulo.Text = filaSeleccionada.Cells["Titulo"].Value.ToString();
                    numAñoInicio.Value = Convert.ToDecimal(filaSeleccionada.Cells["AñoInicio"].Value);
                    numAñoFin.Value = Convert.ToDecimal(filaSeleccionada.Cells["AñoFin"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de formación académica: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que se dispara al hacer clic en una celda del DataGridView de experiencia profesional.
        // Carga los datos de la experiencia seleccionada en los campos correspondientes del formulario:
        // Empresa, Puesto, Año de inicio y Año de fin.
        private void dgvExperienciaProfesional_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvExperienciaProfesional.Rows[e.RowIndex].Cells["ExperienciaID"].Value != null)
                {
                    DataGridViewRow filaSeleccionada = dgvExperienciaProfesional.Rows[e.RowIndex];

                    txtEmpresa.Text = filaSeleccionada.Cells["Empresa"].Value.ToString();
                    txtPuesto.Text = filaSeleccionada.Cells["Puesto"].Value.ToString();
                    numAñoInicioExperiencia.Value = Convert.ToDecimal(filaSeleccionada.Cells["AñoInicio"].Value);
                    numAñoFinExperiencia.Value = Convert.ToDecimal(filaSeleccionada.Cells["AñoFin"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de experiencia profesional: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que se dispara al hacer clic en una celda del DataGridView de habilidades.
        // Carga el nombre de la habilidad seleccionada en el campo correspondiente del formulario.
        private void dgvHabilidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvHabilidades.Rows[e.RowIndex].Cells["HabilidadID"].Value != null)
                {
                    DataGridViewRow filaSeleccionada = dgvHabilidades.Rows[e.RowIndex];

                    txtHabilidad.Text = filaSeleccionada.Cells["Habilidad"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de habilidades: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que se dispara al hacer clic en una celda del DataGridView de competencias.
        // Carga los datos de la competencia seleccionada en los campos correspondientes del formulario:
        // Competencia y Nivel de dominio.
        private void dgvCompetencias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvCompetencias.Rows[e.RowIndex].Cells["CompetenciaID"].Value != null)
                {
                    DataGridViewRow filaSeleccionada = dgvCompetencias.Rows[e.RowIndex];

                    txtCompetencia.Text = filaSeleccionada.Cells["Competencia"].Value.ToString();
                    cmbDominio.SelectedItem = filaSeleccionada.Cells["Dominio"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de competencias: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento que se dispara al hacer clic en una celda del DataGridView de referencias.
        // Carga los datos de la referencia seleccionada en los campos correspondientes del formulario:
        // Tipo de referencia, Nombre y Teléfono.
        private void dgvReferencias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvReferencias.Rows[e.RowIndex].Cells["ReferenciaID"].Value != null)
                {
                    DataGridViewRow filaSeleccionada = dgvReferencias.Rows[e.RowIndex];

                    cmbTipoReferencia.SelectedItem = filaSeleccionada.Cells["TipoReferencia"].Value.ToString();
                    txtNombreReferencia.Text = filaSeleccionada.Cells["Nombre"].Value.ToString();
                    txtTelefonoReferencia.Text = filaSeleccionada.Cells["Telefono"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de referencias: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnBuscarColaborador_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores ingresados en los campos
                string nombre = txtNombreCompleto.Text.Trim().ToLower();
                string telefono = txtTelefono.Text.Trim().ToLower();
                string email = txtEmail.Text.Trim().ToLower();
                string departamento = txtDepartamento.Text.Trim().ToLower();
                string objetivo = txtObjetivo.Text.Trim().ToLower();

                // Crear una lista de filas que coincidan con los criterios
                List<DataGridViewRow> resultados = dgvColaboradores.Rows.Cast<DataGridViewRow>()
                    .Where(fila =>
                        (string.IsNullOrEmpty(nombre) || fila.Cells["NombreCompleto"].Value.ToString().ToLower().Contains(nombre)) &&
                        (string.IsNullOrEmpty(telefono) || fila.Cells["Telefono"].Value.ToString().ToLower().Contains(telefono)) &&
                        (string.IsNullOrEmpty(email) || fila.Cells["Email"].Value.ToString().ToLower().Contains(email)) &&
                        (string.IsNullOrEmpty(departamento) || fila.Cells["Departamento"].Value.ToString().ToLower().Contains(departamento)) &&
                        (string.IsNullOrEmpty(objetivo) || fila.Cells["Objetivo"].Value.ToString().ToLower().Contains(objetivo))
                    ).ToList();

                // Verificar si hay resultados
                if (resultados.Count > 0)
                {
                    // Limpia la selección actual
                    dgvColaboradores.ClearSelection();

                    // Resaltar las filas encontradas
                    foreach (var fila in resultados)
                    {
                        fila.Selected = true;
                        dgvColaboradores.FirstDisplayedScrollingRowIndex = fila.Index;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron colaboradores que coincidan con los criterios de búsqueda.",
                                    "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error durante la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación de campos vacíos
                if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Por favor, complete los campos obligatorios (Nombre Completo y Email).",
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar si el colaborador ya existe
                Colaborador colaborador = new Colaborador();
                if (colaborador.ExisteColaborador(txtNombreCompleto.Text.Trim(), txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Ya existe un colaborador con este nombre o correo electrónico.",
                                    "Duplicado detectado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear el nuevo colaborador
                colaborador.NombreCompleto = txtNombreCompleto.Text.Trim();
                colaborador.Telefono = txtTelefono.Text.Trim();
                colaborador.Email = txtEmail.Text.Trim();
                colaborador.Departamento = txtDepartamento.Text.Trim();
                colaborador.Objetivo = txtObjetivo.Text.Trim();
                colaborador.EstadoActivo = true;

                // Convertir la imagen en el PictureBox a un arreglo de bytes
                if (pictureBoxFoto.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBoxFoto.Image.Save(ms, pictureBoxFoto.Image.RawFormat);
                        colaborador.Foto = ms.ToArray();
                    }
                }

                // Agregar el colaborador
                colaborador.AgregarColaborador();

                // Registrar la actividad en el historial
                ConexionBD conexion = new ConexionBD();
                string accion = $"Colaborador agregado: {colaborador.NombreCompleto}";
                conexion.RegistrarActividad(UsuarioSesion.UsuarioID, accion);

                // Notificar al usuario
                MessageBox.Show("Colaborador agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar la lista de colaboradores
                CargarColaboradores();

                // Limpiar los campos del formulario
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Método para limpiar los campos del formulario
        private void LimpiarCampos()
        {
            // Limpia los campos de entrada
            txtNombreCompleto.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDepartamento.Clear();
            txtObjetivo.Clear();

            // Limpia la imagen en el PictureBox
            pictureBoxFoto.Image = null;

            // Deselecciona cualquier fila en el DataGridView
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                dgvColaboradores.ClearSelection();
            }
        }



        // Botón Actualizar
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                try
                {
                    // Crear el objeto colaborador con los datos actualizados
                    Colaborador colaborador = new Colaborador
                    {
                        ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value),
                        NombreCompleto = txtNombreCompleto.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Departamento = txtDepartamento.Text.Trim(),
                        Objetivo = txtObjetivo.Text.Trim(),
                    };

                    // Convertir la imagen en el PictureBox a un arreglo de bytes
                    if (pictureBoxFoto.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pictureBoxFoto.Image.Save(ms, pictureBoxFoto.Image.RawFormat);
                            colaborador.Foto = ms.ToArray();
                        }
                    }

                    // Actualizar el colaborador
                    colaborador.ActualizarColaborador();

                    // Registrar la actividad en el historial
                    ConexionBD conexion = new ConexionBD();
                    string accion = $"Colaborador actualizado: {colaborador.NombreCompleto} (ID: {colaborador.ColaboradorID})";
                    conexion.RegistrarActividad(UsuarioSesion.UsuarioID, accion);

                    // Notificar al usuario
                    MessageBox.Show("Colaborador actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar la lista de colaboradores
                    CargarColaboradores();

                    // Limpiar los campos del formulario
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un colaborador para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                try
                {
                    // Confirmar la eliminación
                    DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este colaborador?",
                                                             "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        // Crear el objeto colaborador para eliminar
                        Colaborador colaborador = new Colaborador
                        {
                            ColaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value)
                        };

                        // Eliminar el colaborador
                        colaborador.EliminarColaborador();

                        // Registrar la actividad en el historial
                        ConexionBD conexion = new ConexionBD();
                        string accion = $"Colaborador eliminado: ID {colaborador.ColaboradorID}";
                        conexion.RegistrarActividad(UsuarioSesion.UsuarioID, accion);

                        // Notificar al usuario
                        MessageBox.Show("Colaborador marcado como inactivo.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Recargar la lista de colaboradores
                        CargarColaboradores();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un colaborador para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void btnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Seleccionar una foto";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Cargar la imagen seleccionada en el PictureBox
                        this.pictureBoxFoto.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                        this.pictureBoxFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        // Botón Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtNombreCompleto.Text.Trim();
            if (!string.IsNullOrEmpty(criterio))
            {
                using (SqlConnection conexion = new ConexionBD().AbrirConexion())
                {
                    string query = "SELECT ColaboradorID, NombreCompleto, Telefono, Email, Departamento, EstadoActivo FROM Colaboradores WHERE NombreCompleto LIKE @Criterio + '%'";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Criterio", criterio);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);
                    dgvColaboradores.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Ingrese un criterio de búsqueda.");
            }
        }

        // Método para cargar formación académica
        private void CargarFormaciones()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);

                using (SqlConnection conexion = new ConexionBD().AbrirConexion())
                {
                    string query = "SELECT FormacionID, Institucion, Titulo, AñoInicio, AñoFin " +
                                   "FROM FormacionAcademica WHERE ColaboradorID = @ColaboradorID";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@ColaboradorID", colaboradorID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvFormacionAcademica.DataSource = dt;
                }
            }
            else
            {
                dgvFormacionAcademica.DataSource = null;
            }
        }


        private bool ExisteFormacionAcademica(string institucion, string titulo, int anioInicio, int anioFin)
        {
            foreach (DataGridViewRow fila in dgvFormacionAcademica.Rows)
            {
                if (fila.Cells["Institucion"].Value.ToString() == institucion &&
                    fila.Cells["Titulo"].Value.ToString() == titulo &&
                    Convert.ToInt32(fila.Cells["AñoInicio"].Value) == anioInicio &&
                    Convert.ToInt32(fila.Cells["AñoFin"].Value) == anioFin)
                {
                    return true; // La formación académica ya existe
                }
            }
            return false; // No existe
        }

        // Código para agregar la formación académica
        private void btnAgregarFormacion_Click(object sender, EventArgs e)
        {
            string institucion = txtInstitucion.Text.Trim();
            string titulo = txtTitulo.Text.Trim();
            int anioInicio = (int)numAñoInicio.Value;
            int anioFin = (int)numAñoFin.Value;

            if (ExisteFormacionAcademica(institucion, titulo, anioInicio, anioFin))
            {
                MessageBox.Show("Ya existe una formación académica con los mismos datos.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

   
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO FormacionAcademica (Institucion, Titulo, AñoInicio, AñoFin, ColaboradorID) " +
                               "VALUES (@Institucion, @Titulo, @AñoInicio, @AñoFin, @ColaboradorID)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Institucion", institucion);
                comando.Parameters.AddWithValue("@Titulo", titulo);
                comando.Parameters.AddWithValue("@AñoInicio", anioInicio);
                comando.Parameters.AddWithValue("@AñoFin", anioFin);
                comando.Parameters.AddWithValue("@ColaboradorID", ObtenerColaboradorSeleccionadoID());
                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Formación académica agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarFormaciones();
        }

        private int ObtenerColaboradorSeleccionadoID()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);
            }
            throw new InvalidOperationException("No hay un colaborador seleccionado.");
        }


        private void btnActualizarFormacion_Click(object sender, EventArgs e)
        {
            if (dgvFormacionAcademica.SelectedRows.Count > 0)
            {
                FormacionAcademica formacion = new FormacionAcademica
                {
                    FormacionID = Convert.ToInt32(dgvFormacionAcademica.SelectedRows[0].Cells["FormacionID"].Value),
                    Institucion = txtInstitucion.Text.Trim(),
                    Titulo = txtTitulo.Text.Trim(),
                    AñoInicio = (int)numAñoInicio.Value,
                    AñoFin = (int)numAñoFin.Value
                };

                formacion.ActualizarFormacion();
                MessageBox.Show("Formación actualizada exitosamente.");
                CargarFormaciones();
            }
            else
            {
                MessageBox.Show("Seleccione una formación para actualizar.");
            }
        }

        private void btnEliminarFormacion_Click(object sender, EventArgs e)
        {
            if (dgvFormacionAcademica.SelectedRows.Count > 0)
            {
                FormacionAcademica formacion = new FormacionAcademica
                {
                    FormacionID = Convert.ToInt32(dgvFormacionAcademica.SelectedRows[0].Cells["FormacionID"].Value)
                };

                formacion.EliminarFormacion();
                MessageBox.Show("Formación eliminada exitosamente.");
                CargarFormaciones();
            }
            else
            {
                MessageBox.Show("Seleccione una formación para eliminar.");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de texto en la pestaña Datos Básicos
            txtNombreCompleto.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDepartamento.Clear();
            txtObjetivo.Clear();
            pictureBoxFoto.Image = null;

            // Limpiar los campos en Formación Académica
            txtInstitucion.Clear();
            txtTitulo.Clear();
            numAñoInicio.Value = numAñoInicio.Minimum;
            numAñoFin.Value = numAñoFin.Minimum;

            // Limpiar los campos en Experiencia Profesional
            txtEmpresa.Clear();
            txtPuesto.Clear();
            numAñoInicioExperiencia.Value = numAñoInicioExperiencia.Minimum;
            numAñoFinExperiencia.Value = numAñoFinExperiencia.Minimum;

            // Limpiar los campos en Habilidades
            txtHabilidad.Clear();

            // Limpiar los campos en Competencias
            txtCompetencia.Clear();
            cmbDominio.SelectedIndex = -1; // Desselecciona el ComboBox de Dominio

            // Limpiar los campos en Referencias
            cmbTipoReferencia.SelectedIndex = -1; // Desselecciona el ComboBox de TipoReferencia
            txtNombreReferencia.Clear();
            txtTelefonoReferencia.Clear();

            // Deseleccionar todas las filas en todos los DataGridView
            dgvColaboradores.ClearSelection();
            dgvFormacionAcademica.ClearSelection();
            dgvExperienciaProfesional.ClearSelection();
            dgvHabilidades.ClearSelection();
            dgvCompetencias.ClearSelection();
            dgvReferencias.ClearSelection();

            // Refrescar los DataGridView para garantizar que reflejan el estado inicial
            dgvCompetencias.DataSource = null;
            dgvReferencias.DataSource = null;
        }
        //Resetea el DataGridView cada que se cambia de colaborador
        private void dgvColaboradores_SelectionChanged_Reset(object sender, EventArgs e)
        {
            // Limpiar todos los campos y DataGridView relacionados al cambiar de colaborador
            LimpiarPestañas();
        }

        private void LimpiarPestañas()
        {
            // Limpiar los campos de la pestaña Formación Académica
            txtInstitucion.Clear();
            txtTitulo.Clear();
            numAñoInicio.Value = numAñoInicio.Minimum;
            numAñoFin.Value = numAñoFin.Minimum;
            dgvFormacionAcademica.ClearSelection();
            dgvFormacionAcademica.DataSource = null;

            // Limpiar los campos de la pestaña Experiencia Profesional
            txtEmpresa.Clear();
            txtPuesto.Clear();
            numAñoInicioExperiencia.Value = numAñoInicioExperiencia.Minimum;
            numAñoFinExperiencia.Value = numAñoFinExperiencia.Minimum;
            dgvExperienciaProfesional.ClearSelection();
            dgvExperienciaProfesional.DataSource = null;

            // Limpiar los campos de la pestaña Habilidades
            txtHabilidad.Clear();
            dgvHabilidades.ClearSelection();
            dgvHabilidades.DataSource = null;

            // Limpiar los campos de la pestaña Competencias
            txtCompetencia.Clear();
            cmbDominio.SelectedIndex = -1;
            dgvCompetencias.ClearSelection();
            dgvCompetencias.DataSource = null;

            // Limpiar los campos de la pestaña Referencias
            cmbTipoReferencia.SelectedIndex = -1;
            txtNombreReferencia.Clear();
            txtTelefonoReferencia.Clear();
            dgvReferencias.ClearSelection();
            dgvReferencias.DataSource = null;
        }


        // Método para cargar experiencia profesional
        private void CargarExperiencias()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);

                using (SqlConnection conexion = new ConexionBD().AbrirConexion())
                {
                    string query = "SELECT ExperienciaID, Empresa, Puesto, AñoInicio, AñoFin " +
                                   "FROM ExperienciaProfesional WHERE ColaboradorID = @ColaboradorID";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@ColaboradorID", colaboradorID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvExperienciaProfesional.DataSource = dt;
                }
            }
            else
            {
                dgvExperienciaProfesional.DataSource = null;
            }
        }


        private bool ExisteExperienciaProfesional(string empresa, string puesto, int anioInicio, int anioFin)
        {
            foreach (DataGridViewRow fila in dgvExperienciaProfesional.Rows)
            {
                if (fila.Cells["Empresa"].Value.ToString() == empresa &&
                    fila.Cells["Puesto"].Value.ToString() == puesto &&
                    Convert.ToInt32(fila.Cells["AñoInicio"].Value) == anioInicio &&
                    Convert.ToInt32(fila.Cells["AñoFin"].Value) == anioFin)
                {
                    return true; // La experiencia profesional ya existe
                }
            }
            return false; // No existe
        }

        private void btnAgregarExperiencia_Click(object sender, EventArgs e)
        {
            string empresa = txtEmpresa.Text.Trim();
            string puesto = txtPuesto.Text.Trim();
            int anioInicio = (int)numAñoInicioExperiencia.Value;
            int anioFin = (int)numAñoFinExperiencia.Value;

            if (ExisteExperienciaProfesional(empresa, puesto, anioInicio, anioFin))
            {
                MessageBox.Show("Ya existe una experiencia profesional con los mismos datos.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Código para agregar la experiencia profesional
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO ExperienciaProfesional (Empresa, Puesto, AñoInicio, AñoFin, ColaboradorID) " +
                               "VALUES (@Empresa, @Puesto, @AñoInicio, @AñoFin, @ColaboradorID)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Empresa", empresa);
                comando.Parameters.AddWithValue("@Puesto", puesto);
                comando.Parameters.AddWithValue("@AñoInicio", anioInicio);
                comando.Parameters.AddWithValue("@AñoFin", anioFin);
                comando.Parameters.AddWithValue("@ColaboradorID", ObtenerColaboradorSeleccionadoID());
                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Experiencia profesional agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarExperiencias();
        }


        private void btnActualizarExperiencia_Click(object sender, EventArgs e)
        {
            if (dgvExperienciaProfesional.SelectedRows.Count > 0)
            {
                ExperienciaProfesional experiencia = new ExperienciaProfesional
                {
                    ExperienciaID = Convert.ToInt32(dgvExperienciaProfesional.SelectedRows[0].Cells["ExperienciaID"].Value),
                    Empresa = txtEmpresa.Text.Trim(),
                    Puesto = txtPuesto.Text.Trim(),
                    AñoInicio = (int)numAñoInicioExperiencia.Value,
                    AñoFin = (int)numAñoFinExperiencia.Value
                };

                experiencia.ActualizarExperiencia();
                MessageBox.Show("Experiencia actualizada exitosamente.");
                CargarExperiencias();
            }
            else
            {
                MessageBox.Show("Seleccione una experiencia para actualizar.");
            }
        }

        private void btnEliminarExperiencia_Click(object sender, EventArgs e)
        {
            if (dgvExperienciaProfesional.SelectedRows.Count > 0)
            {
                ExperienciaProfesional experiencia = new ExperienciaProfesional
                {
                    ExperienciaID = Convert.ToInt32(dgvExperienciaProfesional.SelectedRows[0].Cells["ExperienciaID"].Value)
                };

                experiencia.EliminarExperiencia();
                MessageBox.Show("Experiencia eliminada exitosamente.");
                CargarExperiencias();
            }
            else
            {
                MessageBox.Show("Seleccione una experiencia para eliminar.");
            }
        }

        private void CargarHabilidades()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);

                dgvHabilidades.DataSource = Habilidad.ObtenerHabilidades(colaboradorID);
            }
            else
            {
                dgvHabilidades.DataSource = null;
            }
        }

        private bool ExisteHabilidad(string habilidad)
        {
            foreach (DataGridViewRow fila in dgvHabilidades.Rows)
            {
                if (fila.Cells["Habilidad"].Value.ToString() == habilidad)
                {
                    return true; // La habilidad ya existe
                }
            }
            return false; // No existe
        }

        private void btnAgregarHabilidad_Click(object sender, EventArgs e)
        {
            string habilidad = txtHabilidad.Text.Trim();

            if (ExisteHabilidad(habilidad))
            {
                MessageBox.Show("Ya existe una habilidad con los mismos datos.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Código para agregar la habilidad
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO Habilidades (Habilidad, ColaboradorID) VALUES (@Habilidad, @ColaboradorID)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Habilidad", habilidad);
                comando.Parameters.AddWithValue("@ColaboradorID", ObtenerColaboradorSeleccionadoID());
                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Habilidad agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarHabilidades();
        }


        private void btnActualizarHabilidad_Click(object sender, EventArgs e)
        {
            if (dgvHabilidades.SelectedRows.Count > 0)
            {
                try
                {
                    Habilidad habilidad = new Habilidad
                    {
                        HabilidadID = Convert.ToInt32(dgvHabilidades.SelectedRows[0].Cells["HabilidadID"].Value),
                        HabilidadNombre = txtHabilidad.Text.Trim()
                    };

                    habilidad.ActualizarHabilidad();
                    MessageBox.Show("Habilidad actualizada exitosamente.");
                    CargarHabilidades();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar habilidad: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una habilidad para actualizar.");
            }
        }

        private void btnEliminarHabilidad_Click(object sender, EventArgs e)
        {
            if (dgvHabilidades.SelectedRows.Count > 0)
            {
                try
                {
                    Habilidad habilidad = new Habilidad
                    {
                        HabilidadID = Convert.ToInt32(dgvHabilidades.SelectedRows[0].Cells["HabilidadID"].Value)
                    };

                    habilidad.EliminarHabilidad();
                    MessageBox.Show("Habilidad eliminada exitosamente.");
                    CargarHabilidades();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar habilidad: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una habilidad para eliminar.");
            }
        }

        private void CargarCompetencias()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);
                dgvCompetencias.DataSource = Competencia.ObtenerCompetencias(colaboradorID);
            }
        }

        private bool ExisteCompetencia(string competencia, string dominio)
        {
            foreach (DataGridViewRow fila in dgvCompetencias.Rows)
            {
                if (fila.Cells["Competencia"].Value.ToString() == competencia &&
                    fila.Cells["Dominio"].Value.ToString() == dominio)
                {
                    return true; // La competencia ya existe
                }
            }
            return false; // No existe
        }

        private void btnAgregarCompetencia_Click(object sender, EventArgs e)
        {
            string competencia = txtCompetencia.Text.Trim();
            string dominio = cmbDominio.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(competencia) || string.IsNullOrEmpty(dominio))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de agregar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ExisteCompetencia(competencia, dominio))
            {
                MessageBox.Show("Ya existe una competencia con los mismos datos.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Código para agregar la competencia
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO Competencias (Competencia, Dominio, ColaboradorID) " +
                               "VALUES (@Competencia, @Dominio, @ColaboradorID)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Competencia", competencia);
                comando.Parameters.AddWithValue("@Dominio", dominio);
                comando.Parameters.AddWithValue("@ColaboradorID", ObtenerColaboradorSeleccionadoID());
                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Competencia agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarCompetencias();
        }



        private void btnActualizarCompetencia_Click(object sender, EventArgs e)
        {
            if (dgvCompetencias.SelectedRows.Count > 0)
            {
                try
                {
                    Competencia competencia = new Competencia
                    {
                        CompetenciaID = Convert.ToInt32(dgvCompetencias.SelectedRows[0].Cells["CompetenciaID"].Value),
                        CompetenciaNombre = txtCompetencia.Text.Trim(),
                        Dominio = cmbDominio.SelectedItem.ToString()
                    };

                    competencia.ActualizarCompetencia();
                    MessageBox.Show("Competencia actualizada exitosamente.");
                    CargarCompetencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la competencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una competencia para actualizar.");
            }
        }

        private void btnEliminarCompetencia_Click(object sender, EventArgs e)
        {
            if (dgvCompetencias.SelectedRows.Count > 0)
            {
                try
                {
                    Competencia competencia = new Competencia
                    {
                        CompetenciaID = Convert.ToInt32(dgvCompetencias.SelectedRows[0].Cells["CompetenciaID"].Value)
                    };

                    competencia.EliminarCompetencia();
                    MessageBox.Show("Competencia eliminada exitosamente.");
                    CargarCompetencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la competencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione una competencia para eliminar.");
            }
        }

        private bool ExisteReferencia(string tipoReferencia, string nombre, string telefono)
        {
            foreach (DataGridViewRow fila in dgvReferencias.Rows)
            {
                if (fila.Cells["TipoReferencia"].Value.ToString() == tipoReferencia &&
                    fila.Cells["Nombre"].Value.ToString() == nombre &&
                    fila.Cells["Telefono"].Value.ToString() == telefono)
                {
                    return true; // La referencia ya existe
                }
            }
            return false; // No existe
        }

        private void btnAgregarReferencia_Click(object sender, EventArgs e)
        {
            string tipoReferencia = cmbTipoReferencia.SelectedItem?.ToString();
            string nombre = txtNombreReferencia.Text.Trim();
            string telefono = txtTelefonoReferencia.Text.Trim();

            if (string.IsNullOrEmpty(tipoReferencia) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(telefono))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de agregar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ExisteReferencia(tipoReferencia, nombre, telefono))
            {
                MessageBox.Show("Ya existe una referencia con los mismos datos.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Código para agregar la referencia
            using (SqlConnection conexion = new ConexionBD().AbrirConexion())
            {
                string query = "INSERT INTO Referencias (TipoReferencia, Nombre, Telefono, ColaboradorID) " +
                               "VALUES (@TipoReferencia, @Nombre, @Telefono, @ColaboradorID)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@TipoReferencia", tipoReferencia);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Telefono", telefono);
                comando.Parameters.AddWithValue("@ColaboradorID", ObtenerColaboradorSeleccionadoID());
                comando.ExecuteNonQuery();
            }

            MessageBox.Show("Referencia agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarReferencias();
        }


        private void btnActualizarReferencia_Click(object sender, EventArgs e)
        {
            if (dgvReferencias.SelectedRows.Count > 0)
            {
                try
                {
                    Referencia referencia = new Referencia
                    {
                        ReferenciaID = Convert.ToInt32(dgvReferencias.SelectedRows[0].Cells["ReferenciaID"].Value),
                        TipoReferencia = cmbTipoReferencia.SelectedItem.ToString(),
                        Nombre = txtNombreReferencia.Text.Trim(),
                        Telefono = txtTelefonoReferencia.Text.Trim()
                    };

                    referencia.ActualizarReferencia();
                    MessageBox.Show("Referencia actualizada exitosamente.");
                    CargarReferencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la referencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una referencia para actualizar.");
            }
        }

        private void btnEliminarReferencia_Click(object sender, EventArgs e)
        {
            if (dgvReferencias.SelectedRows.Count > 0)
            {
                try
                {
                    Referencia referencia = new Referencia
                    {
                        ReferenciaID = Convert.ToInt32(dgvReferencias.SelectedRows[0].Cells["ReferenciaID"].Value)
                    };

                    referencia.EliminarReferencia();
                    MessageBox.Show("Referencia eliminada exitosamente.");
                    CargarReferencias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la referencia: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una referencia para eliminar.");
            }
        }

        private void CargarReferencias()
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                int colaboradorID = Convert.ToInt32(dgvColaboradores.SelectedRows[0].Cells["ColaboradorID"].Value);
                dgvReferencias.DataSource = Referencia.ObtenerReferencias(colaboradorID);
            }
        }



        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario actual
        }


        // Evento para actualizar las formaciones cuando se selecciona un colaborador
        private void dgvColaboradores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvColaboradores.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvColaboradores.SelectedRows[0];
                int colaboradorID = Convert.ToInt32(row.Cells["ColaboradorID"].Value);

                using (SqlConnection conexion = new ConexionBD().AbrirConexion())
                {
                    string query = "SELECT Foto FROM Colaboradores WHERE ColaboradorID = @ColaboradorID";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@ColaboradorID", colaboradorID);

                    object fotoObj = comando.ExecuteScalar();
                    if (fotoObj != null && fotoObj != DBNull.Value)
                    {
                        byte[] fotoBytes = (byte[])fotoObj;
                        using (var ms = new MemoryStream(fotoBytes))
                        {
                            pictureBoxFoto.Image = Image.FromStream(ms);
                            pictureBoxFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                    else
                    {
                        pictureBoxFoto.Image = null; // Si no hay foto, limpiar el PictureBox
                    }
                }
            }
            CargarFormaciones();
            CargarExperiencias();
            CargarHabilidades();
            CargarCompetencias();
            CargarReferencias();
        }

        // Método para limpiar los campos
       
    }
}

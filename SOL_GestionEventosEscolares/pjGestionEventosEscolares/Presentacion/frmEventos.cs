using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pjGestionEventosEscolares.Presentacion
{
    public partial class frmEventos : Form
    {
        public frmEventos()
        {
            InitializeComponent();
        }

        // Variables
        int iCodigoEvento = 0;

        // Método para listar eventos sin procedimientos
        private DataTable Listar_Eventos(string cBusqueda)
        {
            DataTable tabla = new DataTable();
            string cadenaConexion = "Server=DESKTOP-VD5GB2H\\SQLEXPRESS;Database=bd_gestion_eventos;Integrated Security=true;";

            using (SqlConnection SqlCon = new SqlConnection(cadenaConexion))
            {
                try
                {
                    string query = @"SELECT E.IdEvento,
                                            E.Nombre AS Evento,
                                            E.Organizador,
                                            E.FechaInicio,
                                            E.Presupuesto,
                                            E.Participantes,
                                            L.NombreLugar AS Lugar
                                     FROM Eventos E
                                     INNER JOIN Lugares L ON E.IdLugar = L.IdLugar
                                     WHERE E.Nombre LIKE @cBusqueda OR L.NombreLugar LIKE @cBusqueda";

                    SqlCommand comando = new SqlCommand(query, SqlCon);
                    comando.Parameters.AddWithValue("@cBusqueda", "%" + cBusqueda + "%");

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(tabla);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al listar eventos: " + ex.Message);
                }
            }

            return tabla;
        }

        // Refrescar lista
        private void ListarEventos(string cBusqueda)
        {
            DataTable dt = Listar_Eventos(cBusqueda);
            dgvLista.Columns.Clear();
            dgvLista.AutoGenerateColumns = true;
            dgvLista.DataSource = dt;
            dgvLista.Refresh();
            dgvLista.Visible = true;
            dgvLista.BringToFront();
            FormatoListaEventos();
        }

        // Formato del DataGridView
        private void FormatoListaEventos()
        {
            dgvLista.Columns["IdEvento"].Visible = true;
            dgvLista.Columns["IdEvento"].Width = 60;

            dgvLista.Columns["Evento"].Width = 200;
            dgvLista.Columns["Organizador"].Width = 160;
            dgvLista.Columns["FechaInicio"].Width = 120;
            dgvLista.Columns["Presupuesto"].Width = 110;
            dgvLista.Columns["Participantes"].Width = 110;
            dgvLista.Columns["Lugar"].Width = 180;
        }

        // Activar/Desactivar controles
        private void ActivarTextos(bool bEstado)
        {
            txtNombre.Enabled = bEstado;
            txtParticipantes.Enabled = bEstado;
            txtLugar.Enabled = bEstado;
            dtpFecha.Enabled = bEstado;
            txtPresupuesto.Enabled = bEstado;
            txtOrg.Enabled = bEstado;
            txtParts.Enabled = bEstado;

            txtBuscar.Enabled = !bEstado;
        }

        private void ActivarBotones(bool bEstado)
        {
            btnNuevo.Enabled = bEstado;
            btnActualizar.Enabled = bEstado;
            btnEliminar.Enabled = bEstado;

            btnGuardar.Visible = !bEstado;
            btnCancelar.Visible = !bEstado;
        }

        // Botón Nuevo
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ActivarTextos(true);
            ActivarBotones(false);
            Limpiar();
            txtNombre.Select();
        }

        // Botón Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ActivarTextos(false);
            ActivarBotones(true);
            Limpiar();
        }

        // Seleccionar evento en la lista
        private void SeleccionarEvento()
        {
            if (dgvLista.CurrentRow == null) return;

            iCodigoEvento = Convert.ToInt32(dgvLista.CurrentRow.Cells["IdEvento"].Value);

            txtNombre.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Evento"].Value);
            txtOrg.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Organizador"].Value);
            dtpFecha.Value = Convert.ToDateTime(dgvLista.CurrentRow.Cells["FechaInicio"].Value);
            txtPresupuesto.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Presupuesto"].Value);
            txtParticipantes.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Participantes"].Value);
            txtLugar.Text = Convert.ToString(dgvLista.CurrentRow.Cells["Lugar"].Value);
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarEvento();
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Opcional: también seleccionar al hacer click en contenido
            SeleccionarEvento();
        }

        // Botón Actualizar (habilita edición del seleccionado)
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (iCodigoEvento == 0)
            {
                MessageBox.Show("Debe seleccionar un evento de la lista para poder actualizarlo.", "Gestor de Eventos Escolares", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ActivarTextos(true);
            ActivarBotones(false);
            txtNombre.Select();
        }

        // Limpiar campos
        private void Limpiar()
        {
            txtNombre.Clear();
            txtParticipantes.Clear();
            txtLugar.Clear();
            txtPresupuesto.Clear();
            txtBuscar.Clear();
            txtOrg.Clear();

            dtpFecha.Value = DateTime.Now;
            iCodigoEvento = 0;
        }

        // Botón Guardar (INSERT)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string cadenaConexion = "Server=DESKTOP-VD5GB2H\\SQLEXPRESS;Database=bd_gestion_eventos;Integrated Security=true;";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    int idLugar = ObtenerIdLugarPorNombre(txtLugar.Text);

                    if (idLugar == -1)
                    {
                        MessageBox.Show("El lugar ingresado no existe en la base de datos.", "Lugar no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string query = "INSERT INTO Eventos (Nombre, FechaInicio, IdLugar, Presupuesto, Organizador, Participantes) " +
                                   "VALUES (@Nombre, @FechaInicio, @IdLugar, @Presupuesto, @Organizador, @Participantes)";

                    SqlCommand comando = new SqlCommand(query, conexion);

                    comando.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    comando.Parameters.AddWithValue("@FechaInicio", dtpFecha.Value);
                    comando.Parameters.AddWithValue("@IdLugar", idLugar);
                    comando.Parameters.AddWithValue("@Presupuesto", decimal.Parse(txtPresupuesto.Text));
                    comando.Parameters.AddWithValue("@Organizador", txtOrg.Text);
                    comando.Parameters.AddWithValue("@Participantes", int.Parse(txtParts.Text));

                    conexion.Open();
                    int resultado = comando.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        MessageBox.Show("Evento guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarEventos("%");
                        Limpiar();
                        ActivarTextos(false);
                        ActivarBotones(true);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar el evento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Verifica que Presupuesto y Participantes tengan valores numéricos válidos.",
                                    "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private int ObtenerIdLugarPorNombre(string nombreLugar)
        {
            int idLugar = -1;
            string cadenaConexion = "Server=DESKTOP-VD5GB2H\\SQLEXPRESS;Database=bd_gestion_eventos;Integrated Security=true;";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT IdLugar FROM Lugares WHERE NombreLugar = @NombreLugar";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@NombreLugar", nombreLugar);

                conexion.Open();
                object resultado = comando.ExecuteScalar();

                if (resultado != null)
                    idLugar = Convert.ToInt32(resultado);
            }

            return idLugar;
        }



        // Botón Eliminar (DELETE)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvLista.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un evento para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEvento = Convert.ToInt32(dgvLista.CurrentRow.Cells["IdEvento"].Value);

            string cadenaConexion = "Server=DESKTOP-VD5GB2H\\SQLEXPRESS;Database=bd_gestion_eventos;Integrated Security=true;";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    string query = "DELETE FROM Eventos WHERE IdEvento = @IdEvento";
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@IdEvento", idEvento);

                    conexion.Open();
                    int resultado = comando.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        MessageBox.Show("Evento eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarEventos("%"); // refrescar lista
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el evento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Excepción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Botón Salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Carga del formulario: listar y preparar UI
        private void frmEventos_Load(object sender, EventArgs e)
        {
            ListarEventos("%");
            ActivarTextos(false);
            ActivarBotones(true);
        }

        // Búsqueda (botón y escritura en tiempo real)
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarEventos(txtBuscar.Text);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ListarEventos(txtBuscar.Text);
        }
    }
}

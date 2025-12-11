using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using pjGestionEventosEscolares.Datos;

namespace pjGestionEventosEscolares.Presentacion
{
    public partial class frmConectar : Form
    {
        public frmConectar()
        {
           // InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection SqlCon = Conexion.getInstancia().CrearConexion();

            try
            {
                SqlCon.Open();
                MessageBox.Show("Conexión exitosa a la base de datos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
            }
        }
    }
}

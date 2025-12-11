using System;
using System.Data.SqlClient;

namespace pjGestionEventosEscolares.Datos
{
    public class Conexion
    {
        private string cadenaConexion;
        private static Conexion instancia = null;

        private Conexion()
        {
            // AUTENTICACIÓN DE WINDOWS (sin usuario ni clave):
            cadenaConexion =
                "Server=DESKTOP-VD5GB2H\\SQLEXPRESS;" +
                "Database=bd_gestion_eventos;" +
                "Integrated Security=true;";
        }

        public static Conexion getInstancia()
        {
            if (instancia == null)
            {
                instancia = new Conexion();
            }
            return instancia;
        }

        public SqlConnection CrearConexion()
        {
            SqlConnection cn = new SqlConnection();

            try
            {
                cn.ConnectionString = cadenaConexion;
            }
            catch (Exception)
            {
                cn = null;
            }

            return cn;
        }
    }
}

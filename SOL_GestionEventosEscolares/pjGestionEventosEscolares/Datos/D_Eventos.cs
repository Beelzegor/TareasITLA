using pjGestionEventosEscolares.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjGestionEventosEscolares.Datos
{
    public class D_Eventos
    {
        public DataTable Listar_Eventos(string cBusqueda)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("sp_MostrarEventosCompleto", SqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@cBusqueda", SqlDbType.VarChar).Value = cBusqueda;
                SqlCon.Open();

                resultado = comando.ExecuteReader();
                tabla.Load(resultado);

                return tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
              
            }
        }

        public string Guardar_Eventos(E_Evento Evento)
        {
            string respuesta = "";
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                // Crear la conexión
                SqlCon = Conexion.getInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("sp_guardar_eventos2", SqlCon);
                comando.CommandType = CommandType.StoredProcedure;

                // Parámetros del procedimiento
                comando.Parameters.Add("@cNombreEvento", SqlDbType.VarChar).Value = Evento.Nombre;
                comando.Parameters.Add("@dFechaInicio", SqlDbType.Date).Value = Evento.FechaInicio;
                comando.Parameters.Add("@cNombreLugar", SqlDbType.VarChar).Value = Evento.NombreLugar;
                comando.Parameters.Add("@cDireccionLugar", SqlDbType.VarChar).Value = Evento.Direccion;
                comando.Parameters.Add("@nPresupuesto", SqlDbType.Decimal).Value = Evento.Presupuesto;

                // Abrir la conexión y ejecutar
                SqlCon.Open();
                respuesta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo guardar el evento";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }

            return respuesta;
        }

    }
}
using InvestWiseProyecto.Model;
using System.Data.SqlClient;
using System.Data;
using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Service;
using InvestWiseProyecto.Repository;

namespace InvestWiseProyecto.DataConnection
{
    public class RolConnection:IRolRepository
    {
        //private string cadena = CadenaConexion.RetornaCadenaConexion();
        private string cadena = CadenaConexion.Instancia.ObtenerCadenaConexion();

        public Respuesta ObtenerRol()
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerRoles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión
                    connection.Open();

                    // Llenar DataTable con los datos obtenidos
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;

                    // Asignar el DataTable al atributo selectResultado de respuesta
                    //respuesta.selectResultado = dataTable;

                    // Convertir DataTable a lista de diccionarios y asignarlo a selectResultado
                    respuesta.selectResultado = DataTableHelper.ConvertDataTableToList(dataTable);
                }
            }

            return respuesta;
        }

        
    }
}

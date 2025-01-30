using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model;
using System.Data.SqlClient;
using System.Data;

namespace InvestWiseProyecto.DataConnection
{
    public class ProductoConnection
    {
        private string cadena = CadenaConexion.RetornaCadenaConexion();

        // INSERTAR PRODUCTO
        public Respuesta InsertarProducto(Producto producto)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_InsertarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idCategoria", SqlDbType.Int)).Value = producto.idCategoria;
                    command.Parameters.Add(new SqlParameter("@nombreProducto", SqlDbType.VarChar, 50)).Value = producto.nombreProducto;
                    command.Parameters.Add(new SqlParameter("@descripcionProducto", SqlDbType.VarChar, 50)).Value = producto.descripcionProducto;
                    command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Float)).Value = producto.precio;

                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión y ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;
                }
            }

            return respuesta;
        }

        //OBTENER PRODUCTOS
        public Respuesta ObtenerProductos()
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerProductos", connection))
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
                    respuesta.selectResultado = ConvertDataTableToList(dataTable);
                }
            }

            return respuesta;
        }
        //OBTENER POR ID
        // OBTENER PRODUCTO POR ID
        public Respuesta ObtenerProductoPorId(int idProducto)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerProductoPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int)).Value = idProducto;

                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión y ejecutar el procedimiento
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

                    // Convertir DataTable a lista de diccionarios y asignarlo a selectResultado
                    respuesta.selectResultado = ConvertDataTableToList(dataTable);
                }
            }

            return respuesta;
        }


        //ACTUALIZAR PRODUCTO
        public Respuesta ActualizarProducto(ProductoModificado productoModi)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ActualizarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int)).Value = productoModi.idProducto;
                    command.Parameters.Add(new SqlParameter("@idCategoria", SqlDbType.Int)).Value = productoModi.idCategoria;
                    command.Parameters.Add(new SqlParameter("@nombreProducto", SqlDbType.VarChar, 50)).Value = productoModi.nombreProducto;
                    command.Parameters.Add(new SqlParameter("@descripcionProducto", SqlDbType.VarChar, 50)).Value = productoModi.descripcionProducto;
                    command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Float)).Value = productoModi.precio;

                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión y ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;
                }
            }

            return respuesta;
        }

        //ELIMINAR PRODUCTO
        public Respuesta EliminarProducto(int idProducto)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_EliminarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int)).Value = idProducto;

                    // Agregar parámetro de salida
                    SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParameter);

                    // Abrir conexión y ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    resultado = (int)outputParameter.Value;
                    respuesta.codigo = resultado;
                }
            }

            return respuesta;
        }

        //Convertimos la tabla
        private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                Dictionary<string, object> rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    rowDict[column.ColumnName] = row[column] != DBNull.Value ? row[column] : null;
                }
                list.Add(rowDict);
            }

            return list;
        }
    }
}
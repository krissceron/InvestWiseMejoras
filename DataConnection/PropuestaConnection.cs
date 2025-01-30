using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model;
using System.Data.SqlClient;
using System.Data;
using InvestWiseProyecto.Service;
using InvestWiseProyecto.Repository;

namespace InvestWiseProyecto.DataConnection
{
    public class PropuestaConnection:IPropuestaRepository
    {
        //private string cadena = CadenaConexion.RetornaCadenaConexion();
        private readonly string cadena = CadenaConexion.RetornaCadenaConexion();//KC

        //INSERTAR USUARIO
        public Respuesta InsertarPropuesta(Propuesta propuesta)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();



            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_InsertarPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int)).Value = propuesta.idProducto;
                    command.Parameters.Add(new SqlParameter("@numInversionistasPropuesta", SqlDbType.Int)).Value = propuesta.numInversionistasPropuesta;
                    command.Parameters.Add(new SqlParameter("@presupuestoGastoPropuesta", SqlDbType.Float,5)).Value = propuesta.presupuestoGastoPropuesta;
                    //command.Parameters.Add(new SqlParameter("@valorTotalPropuesta", SqlDbType.Float,5)).Value = propuesta.valorTotalPropuesta;
                    command.Parameters.Add(new SqlParameter("@precioVentaPropuesta", SqlDbType.Float,5)).Value = propuesta.precioVentaPropuesta;
                    command.Parameters.Add(new SqlParameter("@fechaInicioPropuesta", SqlDbType.VarChar,8)).Value = propuesta.fechaInicioPropuesta;
                    //command.Parameters.Add(new SqlParameter("@gananciaPropuesta", SqlDbType.Float,5)).Value = propuesta.gananciaPropuesta;
                   
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

        //OBTENER USUARIOS
        public Respuesta ObtenerPropuestas()
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerPropuestas", connection))
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

        //OBTENER POR ID
        public Respuesta ObtenerPropuestaPorId(int idPropuesta)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerPropuestaPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idPropuesta", SqlDbType.Int)).Value = idPropuesta;

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
                    respuesta.selectResultado = DataTableHelper.ConvertDataTableToList(dataTable);
                }
            }

            return respuesta;
        }

        public Respuesta ObtenerUsuariosPorPropuesta(int idPropuesta)
        {
            Respuesta respuesta = new Respuesta();
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerUsuariosPorPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idPropuesta", idPropuesta));

                    // Parámetro de salida
                    SqlParameter outputResultado = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputResultado);

                    // Ejecutar el procedimiento
                    connection.Open();
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Obtener el valor del parámetro de salida
                    int resultado = (int)outputResultado.Value;
                    respuesta.codigo = resultado;

                    if (resultado == 1)
                    {
                        respuesta.selectResultado = DataTableHelper.ConvertDataTableToList(dataTable);
                    }
                    else
                    {
                        respuesta.mensaje = "Error al obtener usuarios por propuesta.";
                    }
                }
            }
            return respuesta;
        }

        public Respuesta AceptarPropuesta(AceptarPropuesta usuarioPropuesta)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_AceptarPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idUsuario", usuarioPropuesta.IdUsuario));
                    command.Parameters.Add(new SqlParameter("@idPropuesta", usuarioPropuesta.IdPropuesta));

                    // Parámetro de salida
                    SqlParameter outputResultado = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputResultado);

                    // Ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    int resultado = (int)outputResultado.Value;
                    respuesta.codigo = resultado;

                    if (resultado == 1)
                    {
                        respuesta.mensaje = "Propuesta aceptada correctamente.";
                    }
                    else if (resultado == 2)
                    {
                        respuesta.mensaje = "Este usuario ya acepto la propuesta.";
                    }
                    else if (resultado == -1)
                    {
                        respuesta.mensaje = "Límite de inversionistas alcanzado.";
                    }

                    else
                    {
                        respuesta.mensaje = "Error al aceptar propuesta.";
                    }
                }
            }

            return respuesta;
        }




        // Método para actualizar usuario
        public Respuesta ActualizarPropuesta(PropuestaModificada propuestaModi)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ActualizarPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandType = CommandType.StoredProcedure;
                    DateTime fechaInicioPropuestaDateTime = DateTime.ParseExact(
                        propuestaModi.fechaInicioPropuesta,
                        "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture
                    );

                    // Agregar parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idPropuesta", SqlDbType.Int)).Value = propuestaModi.idPropuesta;
                    command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int)).Value = propuestaModi.idProducto;
                    //command.Parameters.Add(new SqlParameter("@idEstadoPropuesta", SqlDbType.Int)).Value = propuestaModi.idEstadoPropuesta;
                    command.Parameters.Add(new SqlParameter("@numInversionistasPropuesta", SqlDbType.Int)).Value = propuestaModi.numInversionistasPropuesta;
                    command.Parameters.Add(new SqlParameter("@presupuestoGastoPropuesta", SqlDbType.Float)).Value = propuestaModi.presupuestoGastoPropuesta;
                    //command.Parameters.Add(new SqlParameter("@valorTotalPropuesta", SqlDbType.Float, 5)).Value = propuestaModi.valorTotalPropuesta;
                    command.Parameters.Add(new SqlParameter("@precioVentaPropuesta", SqlDbType.Float, 5)).Value = propuestaModi.precioVentaPropuesta;
                    command.Parameters.Add(new SqlParameter("@fechaInicioPropuesta", SqlDbType.VarChar, 8)).Value = fechaInicioPropuestaDateTime;
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

        //eliminar usuario
        // Método para eliminar usuario
        public Respuesta EliminarPropuesta(int idPropuesta)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_EliminarPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idPropuesta", SqlDbType.Int)).Value = idPropuesta;

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

        public Respuesta SalirPropuesta(AceptarPropuesta usuarioPropuesta)
        {
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_SalirPropuesta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idUsuario", usuarioPropuesta.IdUsuario));
                    command.Parameters.Add(new SqlParameter("@idPropuesta", usuarioPropuesta.IdPropuesta));

                    // Parámetro de salida
                    SqlParameter outputResultado = new SqlParameter("@resultado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputResultado);

                    // Ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    int resultado = (int)outputResultado.Value;
                    respuesta.codigo = resultado;

                    if (resultado == 1)
                    {
                        respuesta.mensaje = "Usuario eliminado de la propuesta correctamente.";
                    }
                    else if (resultado == 0)
                    {
                        respuesta.mensaje = "Usuario no está asociado a esta propuesta.";
                    }
                    else
                    {
                        respuesta.mensaje = "Error al intentar eliminar al usuario de la propuesta.";
                    }
                }
            }

            return respuesta;
        }



        //Convertimos la tabla

        //private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        //{
        //    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

        //    foreach (DataRow row in dataTable.Rows)
        //    {
        //        Dictionary<string, object> rowDict = new Dictionary<string, object>();
        //        foreach (DataColumn column in dataTable.Columns)
        //        {
        //            rowDict[column.ColumnName] = row[column] != DBNull.Value ? row[column] : null;
        //        }
        //        list.Add(rowDict);
        //    }

        //    return list;
        //}



    }
}

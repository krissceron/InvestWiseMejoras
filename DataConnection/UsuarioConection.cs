using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model;
using InvestWiseProyecto.Repository;
using InvestWiseProyecto.Service;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace InvestWiseProyecto.Data
{
    public class UsuarioConection : IUsuarioRepository
    {
        //private string cadena = CadenaConexion.RetornaCadenaConexion();
        private readonly string cadena = CadenaConexion.RetornaCadenaConexion();//KC

        //INSERTAR USUARIO
        public Respuesta InsertarUsuario(Usuario usuario)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            // Validar formato de correo electrónico
            //if (!EsCorreoValido(usuario.correoUsuario))
            //{
            //    respuesta.codigo = -1;
            //    respuesta.mensaje = "El correo electrónico tiene un formato inválido.";
            //    return respuesta;
            //}
            if (!ValidationHelper.EsCorreoValido(usuario.correoUsuario))
            {
                respuesta.codigo = -1;
                respuesta.mensaje = "El correo electrónico tiene un formato inválido.";
                return respuesta;
            }//kc

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_InsertarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    DateTime fechaNacimientoDateTime = DateTime.ParseExact(
                        usuario.fechaNacimientoUsuario,
                        "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture
                    );

                    // Agregar parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idRol", SqlDbType.Int)).Value = usuario.idRol;
                    command.Parameters.Add(new SqlParameter("@nombreApellido", SqlDbType.VarChar, 50)).Value = usuario.nombreApellido;
                    command.Parameters.Add(new SqlParameter("@nombreUsuario", SqlDbType.VarChar, 50)).Value = usuario.nombreUsuario;
                    command.Parameters.Add(new SqlParameter("@contraseniaUsuario", SqlDbType.VarChar, 50)).Value = usuario.contraseniaUsuario;
                    command.Parameters.Add(new SqlParameter("@cedulaUsuario", SqlDbType.VarChar, 50)).Value = usuario.cedulaUsuario;
                    command.Parameters.Add(new SqlParameter("@telefonoUsuario", SqlDbType.VarChar, 50)).Value = usuario.telefonoUsuario;
                    command.Parameters.Add(new SqlParameter("@correoUsuario", SqlDbType.VarChar, 100)).Value = usuario.correoUsuario;
                    command.Parameters.Add(new SqlParameter("@generoUsuario", SqlDbType.VarChar, 30)).Value = usuario.generoUsuario;
                    command.Parameters.Add(new SqlParameter("@fechaNacimientoUsuario", SqlDbType.DateTime)).Value = fechaNacimientoDateTime;
                    command.Parameters.Add(new SqlParameter("@objPorcPropUsuario", SqlDbType.Float,5)).Value = usuario.objPorcPropUsuario;
                    command.Parameters.Add(new SqlParameter("@objGanMesUsuario", SqlDbType.Float,5)).Value = usuario.objGanMesUsuario;

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
        public Respuesta ObtenerUsuario()
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerUsuarios", connection))
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
                    //respuesta.selectResultado = ConvertDataTableToList(dataTable);
                    respuesta.selectResultado = DataTableHelper.ConvertDataTableToList(dataTable);//kc
                }
            }

            return respuesta;
        }

        //obtener por id

        public Respuesta ObtenerUsuarioPorId(int idUsuario)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ObtenerUsuarioPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int)).Value = idUsuario;

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

                    // Convertir DataTable a lista de diccionarios y asignarlo a selectResultado
                    respuesta.selectResultado = DataTableHelper.ConvertDataTableToList(dataTable);
                }
            }

            return respuesta;
        }


        // Método para actualizar usuario
        public Respuesta ActualizarUsuario(UsuarioModificado usuarioModi)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_ActualizarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandType = CommandType.StoredProcedure;
                    DateTime fechaNacimientoDateTime = DateTime.ParseExact(
                        usuarioModi.fechaNacimientoUsuario,
                        "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture
                    );
                    // Agregar parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int)).Value = usuarioModi.idUsuario;
                    command.Parameters.Add(new SqlParameter("@idRol", SqlDbType.Int)).Value = usuarioModi.idRol;
                    command.Parameters.Add(new SqlParameter("@nombreApellido", SqlDbType.VarChar, 50)).Value = usuarioModi.nombreApellido;
                    command.Parameters.Add(new SqlParameter("@nombreUsuario", SqlDbType.VarChar, 50)).Value = usuarioModi.nombreUsuario;
                    command.Parameters.Add(new SqlParameter("@contraseniaUsuario", SqlDbType.VarChar, 50)).Value = usuarioModi.contraseniaUsuario;
                    command.Parameters.Add(new SqlParameter("@cedulaUsuario", SqlDbType.VarChar, 50)).Value = usuarioModi.cedulaUsuario;
                    command.Parameters.Add(new SqlParameter("@telefonoUsuario", SqlDbType.VarChar, 50)).Value = usuarioModi.telefonoUsuario;
                    command.Parameters.Add(new SqlParameter("@correoUsuario", SqlDbType.VarChar, 100)).Value = usuarioModi.correoUsuario;
                    command.Parameters.Add(new SqlParameter("@generoUsuario", SqlDbType.VarChar, 30)).Value = usuarioModi.generoUsuario;
                    command.Parameters.Add(new SqlParameter("@fechaNacimientoUsuario", SqlDbType.DateTime)).Value = fechaNacimientoDateTime;
                    command.Parameters.Add(new SqlParameter("@objPorcPropUsuario", SqlDbType.Float)).Value = usuarioModi.objPorcPropUsuario;
                    command.Parameters.Add(new SqlParameter("@objGanMesUsuario", SqlDbType.Float)).Value = usuarioModi.objGanMesUsuario;

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
        public Respuesta EliminarUsuario(int idUsuario)
        {
            int resultado;
            Respuesta respuesta = new Respuesta();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_EliminarUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetro de entrada
                    command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int)).Value = idUsuario;

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

        //LOGIN USUARIO
        // Método para el login de usuario
        public Respuesta LoginUsuario(LoginUsuario loginUsuario)
        {
            Respuesta respuesta = new Respuesta();
            using (SqlConnection connection = new SqlConnection(cadena))
            {
                using (SqlCommand command = new SqlCommand("sp_LoginUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    command.Parameters.Add(new SqlParameter("@nombreUsuario", SqlDbType.VarChar, 50)).Value = loginUsuario.nombreUsuario;
                    command.Parameters.Add(new SqlParameter("@contraseniaUsuario", SqlDbType.VarChar, 50)).Value = loginUsuario.contraseniaUsuario;

                    // Parámetros de salida
                    SqlParameter outputResultado = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputRolUsuario = new SqlParameter("@rolUsuario", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
                    SqlParameter outputIdUsuario = new SqlParameter("@idUsuario", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputResultado);
                    command.Parameters.Add(outputRolUsuario);
                    command.Parameters.Add(outputIdUsuario);

                    // Ejecutar el procedimiento
                    connection.Open();
                    command.ExecuteNonQuery();

                    // Configurar la respuesta
                    respuesta.codigo = (int)outputResultado.Value;
                    if (respuesta.codigo == 1)
                    {
                        respuesta.mensaje = "Inicio de sesión exitoso";
                        respuesta.selectResultado = new
                        {
                            rolUsuario = outputRolUsuario.Value?.ToString(),
                            idUsuario = (int)outputIdUsuario.Value
                        };
                    }
                    else if (respuesta.codigo == 0)
                    {
                        respuesta.mensaje = "Credenciales incorrectas";
                    }
                    else
                    {
                        respuesta.mensaje = "Error en el servidor";
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
        //}esta en datatablehelper

        //private bool EsCorreoValido(string correo)
        //{
        //    // Expresión regular para validar correos
        //    string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        //    return Regex.IsMatch(correo, patronCorreo);
        //}//esta en validationhelper



    }
}

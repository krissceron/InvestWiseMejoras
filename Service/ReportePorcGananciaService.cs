using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model;
using InvestWiseProyecto.Model.Reportes;
using System.Data.SqlClient;

namespace InvestWiseProyecto.Service
{
    public class ReportePorcGananciaService
    {
        private string _cadenaConexion;

        public ReportePorcGananciaService()
        {
            _cadenaConexion = CadenaConexion.RetornaCadenaConexion();
        }

        public List<object> GenerarReporte()
        {
            // Cargar datos desde la base de datos
            var usuarios = ObtenerUsuarios();
            var productos = ObtenerProductos();
            var propuestas = ObtenerPropuestas();
            var usuariosPropuestas = ObtenerUsuariosPropuestas();

            // Lista para almacenar los resultados del reporte
            var reporte = new List<object>();

            foreach (var propuesta in propuestas)
            {
                // Excluir propuestas aprobadas
                if (propuesta.EstaAprobado) continue;

                // Filtrar usuarios relacionados a la propuesta actual
                var usuariosEnPropuesta = usuariosPropuestas
                    .Where(up => up.IdPropuesta == propuesta.IdPropuesta).ToList();

                if (!usuariosEnPropuesta.Any()) continue;

                // Calcular ingreso por usuario para la propuesta
                float ingresoPorUsuario = (propuesta.PrecioVentaPropuesta - propuesta.ValorTotalPropuesta) /
                                           usuariosEnPropuesta.Count;

                foreach (var usuarioPropuesta in usuariosEnPropuesta)
                {
                    // Obtener datos del usuario relacionado
                    var usuario = usuarios.FirstOrDefault(u => u.IdUsuario == usuarioPropuesta.IdUsuario);
                    if (usuario == null) continue;

                    // Obtener el producto relacionado
                    var producto = productos.FirstOrDefault(p => p.IdProducto == propuesta.IdProducto);
                    if (producto == null) continue;

                    // Calcular porcentaje de ganancia final
                    float porcentajeGananciaFinal = (ingresoPorUsuario / usuarioPropuesta.MontoInversion) * 100;

                    // Generar mensaje basado en el objetivo del usuario
                    string mensajeResultado;
                    if (porcentajeGananciaFinal > usuario.ObjPorcPropUsuario)
                    {
                        mensajeResultado = "Superó su objetivo. Felicidades";
                    }
                    else if (porcentajeGananciaFinal < usuario.ObjPorcPropUsuario)
                    {
                        mensajeResultado = "No alcanzó su objetivo";
                    }
                    else
                    {
                        mensajeResultado = "Alcanzó su objetivo. Felicidades";
                    }

                    // Crear el objeto del reporte
                    reporte.Add(new
                    {
                        NombreUsuario = usuario.NombreApellido,
                        Genero = usuario.GeneroUsuario,
                        Edad = CalcularEdadDesdeFormatoYYYYMMDD(usuario.FechaNacimientoUsuario),
                        Producto = producto.NombreProducto,
                        InversionTotalPropuesta = propuesta.ValorTotalPropuesta,
                        PrecioDeVenta = propuesta.PrecioVentaPropuesta,
                        Participantes = propuesta.NumInversionistasPropuesta,
                        InversionIndividual = usuarioPropuesta.MontoInversion,
                        IngresoPropuestaPorUsuario = ingresoPorUsuario,
                        PorcentajeGananciaFinal = porcentajeGananciaFinal,
                        ObjPorcGananciaIndiv = usuario.ObjPorcPropUsuario,
                        MensajeResultado = mensajeResultado
                    });
                }
            }

            // Ordenar el reporte por NombreUsuario antes de retornarlo
            var reporteOrdenado = reporte.OrderBy(r => r.GetType().GetProperty("NombreUsuario").GetValue(r)).ToList();

            return reporteOrdenado;
        }


        private List<UsuarioRPorcGanan> ObtenerUsuarios()
        {
            var usuarios = new List<UsuarioRPorcGanan>();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                var query = "SELECT * FROM Usuario";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new UsuarioRPorcGanan
                            {
                                IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                NombreApellido = reader["nombreApellido"].ToString(),
                                CorreoUsuario = reader["correoUsuario"].ToString(),
                                GeneroUsuario = reader["generoUsuario"].ToString(),
                                FechaNacimientoUsuario = Convert.ToDateTime(reader["fechaNacimientoUsuario"])
                                                  .ToString("yyyyMMdd"),
                                ObjPorcPropUsuario = Convert.ToSingle(reader["objPorcPropUsuario"])
                            });
                        }
                    }
                }
            }
            return usuarios;
        }

        private List<ProductoRPorcGanan> ObtenerProductos()
        {
            var productos = new List<ProductoRPorcGanan>();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                var query = "SELECT * FROM Producto";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(new ProductoRPorcGanan
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                NombreProducto = reader["nombreProducto"].ToString()
                            });
                        }
                    }
                }
            }
            return productos;
        }

        private List<PropuestaRPorcGanan> ObtenerPropuestas()
        {
            var propuestas = new List<PropuestaRPorcGanan>();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                var query = "SELECT * FROM Propuesta";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            propuestas.Add(new PropuestaRPorcGanan
                            {
                                IdPropuesta = Convert.ToInt32(reader["idPropuesta"]),
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                ValorTotalPropuesta = Convert.ToSingle(reader["valorTotalPropuesta"]),
                                PrecioVentaPropuesta = Convert.ToSingle(reader["precioVentaPropuesta"]),
                                EstaAprobado = Convert.ToBoolean(reader["estaAprobado"]),
                                NumInversionistasPropuesta = Convert.ToInt32(reader["numInversionistasPropuesta"])
                            });
                        }
                    }
                }
            }
            return propuestas;
        }

        private List<UsuPropRPorcGanan> ObtenerUsuariosPropuestas()
        {
            var usuariosPropuestas = new List<UsuPropRPorcGanan>();
            using (var connection = new SqlConnection(_cadenaConexion))
            {
                var query = "SELECT * FROM UsuarioPropuesta";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuariosPropuestas.Add(new UsuPropRPorcGanan
                            {
                                IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                IdPropuesta = Convert.ToInt32(reader["idPropuesta"]),
                                MontoInversion = Convert.ToSingle(reader["montoInversion"])
                            });
                        }
                    }
                }
            }
            return usuariosPropuestas;
        }
        private int CalcularEdadDesdeFormatoYYYYMMDD(string fechaNacimiento)
        {
            if (DateTime.TryParseExact(fechaNacimiento, "yyyyMMdd",
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None,
                                       out DateTime fechaNacimientoDate))
            {
                var today = DateTime.Today;
                int edad = today.Year - fechaNacimientoDate.Year;

                // Ajustar la edad si el cumpleaños aún no ha ocurrido este año
                if (fechaNacimientoDate > today.AddYears(-edad))
                {
                    edad--;
                }

                return edad;
            }
            else
            {
                throw new FormatException("La fecha de nacimiento no tiene el formato esperado (yyyyMMdd).");
            }
        }

    }
}
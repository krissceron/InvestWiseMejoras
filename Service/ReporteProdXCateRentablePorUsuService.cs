using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model;
using InvestWiseProyecto.Model.Reportes;
using System.Data.SqlClient;

public class ReporteProdXCateRentablePorUsuService
{
    private string _cadenaConexion;

    public ReporteProdXCateRentablePorUsuService()
    {
        _cadenaConexion = CadenaConexion.RetornaCadenaConexion();
    }
    public List<object> GenerarReportePorUsuarioPorCategoria(int idUsuario, int? idCategoria = null)
    {
        // Cargar datos desde la base de datos
        var usuarios = ObtenerUsuarios();
        var productos = ObtenerProductos();
        var propuestas = ObtenerPropuestas();
        var usuariosPropuestas = ObtenerUsuariosPropuestas();
        var categorias = ObtenerCategorias();

        // Filtrar el usuario por ID
        var usuarioFiltrado = usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
        if (usuarioFiltrado == null)
        {
            return new List<object>(); // Retorna vacío si no hay usuario
        }

        var reporte = new List<object>();

        // Recorrer propuestas
        foreach (var propuesta in propuestas)
        {
            // Ignorar propuestas aprobadas
            if (propuesta.EstaAprobado) continue;

            // Filtrar propuestas específicas del usuario
            var propuestasUsuario = usuariosPropuestas
                .Where(up => up.IdUsuario == idUsuario && up.IdPropuesta == propuesta.IdPropuesta)
                .ToList();

            if (!propuestasUsuario.Any()) continue;

            // Buscar producto relacionado
            var productoRelacionado = productos.FirstOrDefault(p => p.IdProducto == propuesta.IdProducto);
            if (productoRelacionado == null) continue;

            // Filtro por categoría si se proporciona
            if (idCategoria.HasValue && productoRelacionado.IdCategoria != idCategoria.Value)
            {
                continue;
            }

            // Obtener categoría del producto
            var categoria = categorias.FirstOrDefault(c => c.IdCategoria == productoRelacionado.IdCategoria);

            // Recorrer las propuestas específicas del usuario
            foreach (var up in propuestasUsuario)
            {
                // Calcular ingreso por usuario
                int numParticipantes = usuariosPropuestas.Count(x => x.IdPropuesta == propuesta.IdPropuesta);
                float ingresoPorUsuario = (propuesta.PrecioVentaPropuesta - propuesta.ValorTotalPropuesta) / numParticipantes;

                // Calcular porcentaje de ganancia final
                float porcentajeGananciaFinal = (ingresoPorUsuario / up.MontoInversion) * 100;

                // Generar mensaje de cumplimiento de objetivo
                string mensajeResultado = porcentajeGananciaFinal > usuarioFiltrado.ObjPorcPropUsuario
                    ? "Superó su objetivo. Felicidades"
                    : porcentajeGananciaFinal < usuarioFiltrado.ObjPorcPropUsuario
                        ? "No alcanzó su objetivo"
                        : "Alcanzó su objetivo. Felicidades";

                // Convertir fechas
                var fechaInicio = ConvertirFechaYYYYMMDD(propuesta.FechaInicioPropuesta);
                var fechaAceptacion = ConvertirFechaYYYYMMDD(up.FechaAceptacion);

                // Calcular rotación de días
                int? rotacionDias = (fechaInicio.HasValue && fechaAceptacion.HasValue)
                    ? (int?)(fechaAceptacion.Value - fechaInicio.Value).TotalDays
                    : null;

                // Calcular rentabilidad por día
                float rentabilidadPorDia = rotacionDias.HasValue && rotacionDias > 0
                    ? porcentajeGananciaFinal / rotacionDias.Value
                    : 0;

                // Analizar rentabilidad
                string analisisRentabilidad = rentabilidadPorDia != 0
                    ? (rentabilidadPorDia > 5
                        ? "Alta rentabilidad, buena inversión"
                        : porcentajeGananciaFinal == 100
                            ? "Recuperaste lo invertido. Considera nuevas oportunidades."
                            : "Baja rentabilidad, reconsiderar inversión")
                    : "Datos insuficientes para análisis";

                // Agregar al reporte
                reporte.Add(new
                {
                    Categoria = categoria?.NombreCategoria ?? "Sin Categoría",
                    Producto = productoRelacionado.NombreProducto,
                    InversionTotalPropuesta = propuesta.ValorTotalPropuesta,
                    PrecioDeVenta = propuesta.PrecioVentaPropuesta,
                    Participantes = numParticipantes,
                    InversionIndividual = up.MontoInversion,
                    IngresoPropuestaPorUsuario = ingresoPorUsuario,
                    PorcentajeGananciaFinal = porcentajeGananciaFinal,
                    ObjPorcGananciaIndiv = usuarioFiltrado.ObjPorcPropUsuario,
                    MensajeResultado = mensajeResultado,
                    RotacionDias = rotacionDias,
                    RentabilidadPorDia = rentabilidadPorDia,
                    AnalisisRentabilidad = analisisRentabilidad
                });
            }
        }

        // Ordenar reporte por rentabilidad diaria o porcentaje de ganancia final
        return reporte
            .OrderByDescending(r => ((dynamic)r).RentabilidadPorDia)
            .ThenByDescending(r => ((dynamic)r).PorcentajeGananciaFinal)
            .ToList();
    }
    private List<CategoriaRProdCatRentable> ObtenerCategorias()
    {
        var categorias = new List<CategoriaRProdCatRentable>();
        using (var connection = new SqlConnection(_cadenaConexion))
        {
            var query = "SELECT * FROM CategoriaProducto";
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new CategoriaRProdCatRentable
                        {
                            IdCategoria = Convert.ToInt32(reader["idCategoria"]),
                            NombreCategoria = reader["nombreCategoria"].ToString()
                        });
                    }
                }
            }
        }
        return categorias;
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
                            NombreProducto = reader["nombreProducto"].ToString(),
                            IdCategoria= Convert.ToInt32(reader["idCategoria"]),
                            //NombreCategoria = reader["nombreCategoria"].ToString(),
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
                            NumInversionistasPropuesta = Convert.ToInt32(reader["numInversionistasPropuesta"]),
                            FechaInicioPropuesta = Convert.ToDateTime(reader["fechaInicioPropuesta"])
                                              .ToString("yyyyMMdd"),
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
                            MontoInversion = Convert.ToSingle(reader["montoInversion"]),
                            FechaAceptacion = Convert.ToDateTime(reader["fechaAceptacion"])
                                              .ToString("yyyyMMdd"),
                        });
                    }
                }
            }
        }
        return usuariosPropuestas;
    }

    private DateTime? ConvertirFechaYYYYMMDD(string fecha)
    {
        if (string.IsNullOrWhiteSpace(fecha))
        {
            return null; // Retorna null si la cadena es nula o vacía
        }

        if (DateTime.TryParseExact(fecha, "yyyyMMdd",
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None,
                                   out DateTime fechaConvertida))
        {
            return fechaConvertida;
        }
        else
        {
            return null; // Retorna null si no es posible convertir la fecha
        }
    }




}

using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model;
using System.Data.SqlClient;
using System.Data;

namespace InvestWiseProyecto.DataConnection
{
    public class ReportePorcGananciaConnection
    {
        private string cadena = CadenaConexion.RetornaCadenaConexion();

        public List<ReporteGananciaUsuario> ObtenerReporteGananciaUsuarios()
        {
            List<ReporteGananciaUsuario> reportes = new List<ReporteGananciaUsuario>();

            using (SqlConnection connection = new SqlConnection(cadena))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Conexión exitosa.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al conectar: {ex.Message}");
                }

                // Cargar los datos de las tablas necesarias en DataTables
                DataTable dtUsuarios = new DataTable();
                DataTable dtPropuestas = new DataTable();
                DataTable dtProductos = new DataTable();
                DataTable dtUsuarioPropuestas = new DataTable();



                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Usuario", connection))
                {
                    adapter.Fill(dtUsuarios);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Propuesta", connection))
                {
                    adapter.Fill(dtPropuestas);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Producto", connection))
                {
                    adapter.Fill(dtProductos);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM UsuarioPropuesta", connection))
                {
                    adapter.Fill(dtUsuarioPropuestas);
                }

                // Convertir DataTables a listas para LINQ
                var usuarios = dtUsuarios.AsEnumerable().Select(row => new
                {
                    IdUsuario = row.Field<int>("idUsuario"),
                    NombreApellido = row.Field<string>("nombreApellido"),
                    CorreoUsuario = row.Field<string>("correoUsuario"),
                    GeneroUsuario = row.Field<string>("generoUsuario"),
                    FechaNacimiento = row.Field<DateTime>("fechaNacimientoUsuario"),
                    ObjPorcPropUsuario = row.Field<double>("objPorcPropUsuario")
                }).ToList();

                var propuestas = dtPropuestas.AsEnumerable().Select(row => new
                {
                    IdPropuesta = row.Field<int>("idPropuesta"),
                    ValorTotalPropuesta = row.Field<double>("valorTotalPropuesta"),
                    PrecioVentaPropuesta = row.Field<double>("precioVentaPropuesta"),
                    NumInversionistas = row.Field<int>("numInversionistasPropuesta")
                }).ToList();

                var productos = dtProductos.AsEnumerable().Select(row => new
                {
                    IdProducto = row.Field<int>("idProducto"),
                    NombreProducto = row.Field<string>("nombreProducto")
                }).ToList();

                var usuarioPropuestas = dtUsuarioPropuestas.AsEnumerable().Select(row => new
                {
                    IdUsuario = row.Field<int>("idUsuario"),
                    IdPropuesta = row.Field<int>("idPropuesta"),
                    MontoInversion = row.Field<double>("montoInversion")
                }).ToList();

                // Realizar la lógica con LINQ
                reportes = (from usuario in usuarios
                            join usuarioPropuesta in usuarioPropuestas on usuario.IdUsuario equals usuarioPropuesta.IdUsuario
                            join propuesta in propuestas on usuarioPropuesta.IdPropuesta equals propuesta.IdPropuesta
                            join producto in productos on propuesta.IdPropuesta equals producto.IdProducto
                            let numUsuarios = usuarioPropuestas.Count(up => up.IdPropuesta == propuesta.IdPropuesta)
                            let ingresoPropuestaPorUsuario = (propuesta.PrecioVentaPropuesta - propuesta.ValorTotalPropuesta) / numUsuarios
                            let porcentajeGananciaFinal = (ingresoPropuestaPorUsuario / usuarioPropuesta.MontoInversion) * 100
                            select new ReporteGananciaUsuario
                            {
                                nombreUsuario = usuario.NombreApellido,
                                correo = usuario.CorreoUsuario,
                                genero = usuario.GeneroUsuario,
                                edad = DateTime.Now.Year - usuario.FechaNacimiento.Year,
                                producto = producto.NombreProducto,
                                inversionTotalPropuesta = propuesta.ValorTotalPropuesta,
                                precioDeVenta = propuesta.PrecioVentaPropuesta,
                                participantes = numUsuarios,
                                inversionIndividual = usuarioPropuesta.MontoInversion,
                                ingresoPropuestaPorUsuario = ingresoPropuestaPorUsuario,
                                porcentajeGananciaFinal = porcentajeGananciaFinal,
                                objPorcGananciaIndiv = usuario.ObjPorcPropUsuario,
                                mensajeResultado = porcentajeGananciaFinal > usuario.ObjPorcPropUsuario
                                    ? "Superó su objetivo. Felicidades"
                                    : porcentajeGananciaFinal < usuario.ObjPorcPropUsuario
                                        ? "No alcanzó su objetivo"
                                        : "Alcanzó su objetivo. Felicidades"
                            }).ToList();
            }

            return reportes;
        }
    }
}

using InvestWiseProyecto.DataConnection;
using InvestWiseProyecto.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestWiseProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpPost]
        [Route("Crear")]
        public Respuesta CrearProducto([FromBody] Producto producto)
        {

            ProductoConnection dbConexion = new ProductoConnection();
            Respuesta res = dbConexion.InsertarProducto(producto);
            return res;
        }

        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerProductos()
        {
            ProductoConnection dbConexion = new ProductoConnection();
            Respuesta res = dbConexion.ObtenerProductos();
            return res;
        }

        [HttpGet]
        [Route("ObtenerPorId/{idProducto}")]
        public Respuesta ObtenerProductoPorId(int idProducto)
        {
            ProductoConnection dbConexion = new ProductoConnection();
            Respuesta res = dbConexion.ObtenerProductoPorId(idProducto);
            return res;
        }


        [HttpPut]
        [Route("Editar")]
        public Respuesta ActualizarProducto([FromBody] ProductoModificado productoModi)
        {
            ProductoConnection dbConexion = new ProductoConnection();
            Respuesta res = dbConexion.ActualizarProducto(productoModi);
            return res;
        }

        [HttpDelete]
        [Route("Eliminar/{idProducto}")]
        public Respuesta EliminarProducto(int idProducto)
        {
            ProductoConnection dbConexion = new ProductoConnection();
            Respuesta res = dbConexion.EliminarProducto(idProducto);
            return res;
        }




    }
}


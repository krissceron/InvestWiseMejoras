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
        private readonly ProductoConnection _dbConexion;

        public ProductoController(ProductoConnection dbConexion)
        {
            _dbConexion = dbConexion;
        }


        [HttpPost]
        [Route("Crear")]
        public Respuesta CrearProducto([FromBody] Producto producto)
        {
            return _dbConexion.InsertarProducto(producto.idCategoria, producto.nombreProducto, producto.descripcionProducto, producto.precio);
        }

     
        [HttpGet]
        [Route("ObtenerTodo")]
        public Respuesta ObtenerProductos()
        {
            return _dbConexion.ObtenerProductos();
        }

     
        [HttpGet]
        [Route("ObtenerPorId/{idProducto}")]
        public Respuesta ObtenerProductoPorId(int idProducto)
        {
            return _dbConexion.ObtenerProductoPorId(idProducto);
        }

        [HttpPut]
        [Route("Editar")]
        public Respuesta ActualizarProducto([FromBody] ProductoModificado productoModi)
        {
            return _dbConexion.ActualizarProducto(productoModi.idProducto, productoModi.idCategoria, productoModi.nombreProducto, productoModi.descripcionProducto, productoModi.precio);
        }


        [HttpDelete]
        [Route("Eliminar/{idProducto}")]
        public Respuesta EliminarProducto(int idProducto)
        {
            return _dbConexion.EliminarProducto(idProducto);
        }
    }
}



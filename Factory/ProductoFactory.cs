using InvestWiseProyecto.Interface;
using InvestWiseProyecto.Model;

namespace InvestWiseProyecto.Factory
{
    public class ProductoFactory : IProductoFactory
    {
        public IProducto CrearProducto(string tipo, int idCategoria, string nombre, string descripcion, float precio, int idProducto = 0)
        {
            return tipo.ToLower() switch
            {
                "nuevo" => new Producto
                {
                    idCategoria = idCategoria,
                    nombreProducto = nombre,
                    descripcionProducto = descripcion,
                    precio = precio
                },
                "modificado" => new ProductoModificado
                {
                    idProducto = idProducto,
                    idCategoria = idCategoria,
                    nombreProducto = nombre,
                    descripcionProducto = descripcion,
                    precio = precio
                },
                _ => throw new ArgumentException("Tipo de producto desconocido")
            };
        }
    }
}
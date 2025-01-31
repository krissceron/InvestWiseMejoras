namespace InvestWiseProyecto.Interface
{
    public interface IProductoFactory
    {
        IProducto CrearProducto(string tipo, int idCategoria, string nombre, string descripcion, float precio, int idProducto = 0);
    }
}

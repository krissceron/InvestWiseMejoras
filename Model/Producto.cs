using InvestWiseProyecto.Interface;

namespace InvestWiseProyecto.Model
{
    public class Producto : IProducto
    {
        public int idCategoria { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionProducto { get; set; }
        public float precio { get; set; }
    }
}
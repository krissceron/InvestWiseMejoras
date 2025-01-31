using InvestWiseProyecto.Interface;

namespace InvestWiseProyecto.Model
{
    public class ProductoModificado : IProducto
    {
        public int idProducto { get; set; }
        public int idCategoria { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionProducto { get; set; }
        public float precio { get; set; }
    }
}


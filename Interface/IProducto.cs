namespace InvestWiseProyecto.Interface
{
    public interface IProducto
    {
        int idCategoria { get; set; }
        string nombreProducto { get; set; }
        string descripcionProducto { get; set; }
        float precio { get; set; }
    }
}

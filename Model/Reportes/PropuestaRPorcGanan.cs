namespace InvestWiseProyecto.Model.Reportes
{
    public class PropuestaRPorcGanan
    {
        public int IdPropuesta { get; set; }
        public int IdProducto { get; set; }
        public float ValorTotalPropuesta { get; set; }
        public float PrecioVentaPropuesta { get; set; }
        public bool EstaAprobado { get; set; }
        public int NumInversionistasPropuesta { get; set; }
        public string FechaInicioPropuesta { get; set; }
    }
}

namespace InvestWiseProyecto.Model
{
    public class Propuesta
    {
        public int idProducto { get; set; }
        //public int idEstadoPropuesta { get; set; }
        public int numInversionistasPropuesta { get; set; }
        private float _presupuestoGastoPropuesta;
        public float presupuestoGastoPropuesta
        {
            get => (float)Math.Round(_presupuestoGastoPropuesta, 2); // Limitar a 2 decimales
            set => _presupuestoGastoPropuesta = (float)Math.Round(value, 2);
        }
        public float valorTotalPropuesta { get; set; }
        public float precioVentaPropuesta { get; set; }
        public string fechaInicioPropuesta { get; set; }
        public float gananciaPropuesta { get; set; }
    }
}

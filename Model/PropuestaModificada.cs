using System.Diagnostics.Contracts;

namespace InvestWiseProyecto.Model
{
    public class PropuestaModificada
    {
        public int idPropuesta {  get; set; }
        public int idProducto { get; set; }
        //public int idEstadoPropuesta { get; set; }
        public int numInversionistasPropuesta { get; set; }
        public float presupuestoGastoPropuesta { get; set; }
        public float precioVentaPropuesta { get; set; }
        public string fechaInicioPropuesta { get; set; }
        //public int estaAprobado { get; set; }
    }
}

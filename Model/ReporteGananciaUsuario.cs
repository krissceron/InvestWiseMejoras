namespace InvestWiseProyecto.Model
{
    public class ReporteGananciaUsuario
    {
        public string nombreUsuario { get; set; }
        public string correo { get; set; }
        public string genero { get; set; }
        public int edad { get; set; }
        public string producto { get; set; }
        public double inversionTotalPropuesta { get; set; }
        public double precioDeVenta { get; set; }
        public int participantes { get; set; }
        public double inversionIndividual { get; set; }
        public double ingresoPropuestaPorUsuario { get; set; }
        public double porcentajeGananciaFinal { get; set; }
        public double objPorcGananciaIndiv { get; set; }
        public string mensajeResultado { get; set; }
    }
}

namespace InvestWiseProyecto.Model
{
    public class UsuarioPropuesta
    {
        public int IdUsuarioPropuesta { get; set; } // ID único de la relación
        public int IdUsuario { get; set; }          // ID del usuario que aceptó la propuesta
        public int IdPropuesta { get; set; }        // ID de la propuesta aceptada
        public float PorcentPartiUsuProp { get; set; } // Porcentaje de participación del usuario
        public float UtilidadPorUsuario { get; set; }  // Utilidad inicializada en 0
        public float MontoInversion { get; set; }      // Monto de inversión calculado
        public string fechaAceptacion { get; set; }
    }
}

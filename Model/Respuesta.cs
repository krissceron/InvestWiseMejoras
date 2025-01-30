namespace InvestWiseProyecto.Model
{
    public class Respuesta
    {
        public int codigo { get; set; } // Código de respuesta (1 = Éxito, 0 = Error, -1 = Excepción)
        public string mensaje { get; set; } // Mensaje para detalles del resultado
        public object selectResultado { get; set; } // Objeto genérico para devolver datos dinámicos
    }
}

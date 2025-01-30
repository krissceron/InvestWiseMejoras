using System.Text.RegularExpressions;

namespace InvestWiseProyecto.Service
{
    public static class ValidationHelper
    {
        public static bool EsCorreoValido(string correo)
        {
            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patronCorreo);
        }
    }
}

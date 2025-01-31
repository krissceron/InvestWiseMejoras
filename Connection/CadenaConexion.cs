namespace InvestWiseProyecto.Connection
{
    
    public sealed class CadenaConexion
    {
        private static readonly Lazy<CadenaConexion> instancia =
            new Lazy<CadenaConexion>(() => new CadenaConexion());

        private string cadenaConexion;

        private CadenaConexion()
        {
            string servidor = @"investwiseserver.database.windows.net";
            string base_tip = "InvestWise";
            string usuario = "userWise12";
            string password = "1q@2w@3e@";

            cadenaConexion = $"Data Source={servidor};Initial Catalog={base_tip};User ID={usuario};Password={password}";
        }

        public static CadenaConexion Instancia => instancia.Value;

        public string ObtenerCadenaConexion()
        {
            return cadenaConexion;
        }
    }
}

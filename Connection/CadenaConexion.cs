namespace InvestWiseProyecto.Connection
{
    public class CadenaConexion
    {
        //private static string servidor = @"camilacabrera.database.windows.net";
        private static string servidor = @"investwiseserver.database.windows.net";
        private static string base_tip = "InvestWise";
        private static string usuario = "userWise12";
        private static string password = "1q@2w@3e@";
        //private static string usuario = "sa";
        //private static string password = "sasa";
        public static string RetornaCadenaConexion()
        {

            return "Data Source=" + servidor + ";Initial Catalog=" + base_tip + ";User ID=" + usuario + ";Password=" + password;
        }
    }
}

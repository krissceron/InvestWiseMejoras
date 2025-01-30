using System.Data;

namespace InvestWiseProyecto.Service
{
    public static class DataTableHelper
    {
        public static List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow row in dataTable.Rows)
            {
                Dictionary<string, object> rowDict = new Dictionary<string, object>();
                foreach (DataColumn column in dataTable.Columns)
                {
                    rowDict[column.ColumnName] = row[column] != DBNull.Value ? row[column] : null;
                }
                list.Add(rowDict);
            }

            return list;
        }
    }
}

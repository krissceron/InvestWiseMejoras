﻿using InvestWiseProyecto.Connection;
using InvestWiseProyecto.Model;

using System.Data;
//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace InvestWiseProyecto.DataConnection
{
    public class CategoriaConnection {
        private string cadena = CadenaConexion.Instancia.ObtenerCadenaConexion();

        //private string cadena = CadenaConexion.RetornaCadenaConexion();

        public Respuesta ObtenerCategoria()
    {
        int resultado;
        Respuesta respuesta = new Respuesta();

        using (SqlConnection connection = new SqlConnection(cadena))
        {
            using (SqlCommand command = new SqlCommand("sp_ObtenerCategorias", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Agregar parámetro de salida
                SqlParameter outputParameter = new SqlParameter("@resultado", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputParameter);

                // Abrir conexión
                connection.Open();

                // Llenar DataTable con los datos obtenidos
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                // Obtener el valor del parámetro de salida
                resultado = (int)outputParameter.Value;
                respuesta.codigo = resultado;

                // Asignar el DataTable al atributo selectResultado de respuesta
                //respuesta.selectResultado = dataTable;

                // Convertir DataTable a lista de diccionarios y asignarlo a selectResultado
                respuesta.selectResultado = ConvertDataTableToList(dataTable);
            }
        }

        return respuesta;
    }

    private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
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


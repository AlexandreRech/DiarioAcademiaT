using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Infra.Shared
{
    public delegate T ConverterDelegate<T>(IDataReader reader);

    public static class Db
    {
        private static string providerName = "";

        private static string connectionString = "";

        private static DbProviderFactory factory = null;

        static Db()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DiarioAcademiaDb"].ConnectionString;
            
            providerName = ConfigurationManager.ConnectionStrings["DiarioAcademiaDb"].ProviderName;
            
            factory = DbProviderFactories.GetFactory(providerName);
        }

        public static int Insert(string sql, object[] parms = null)
        {
            sql = string.Format(sql, GetParameterPrefix());

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (DbCommand command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.SetParameters(parms);                     // Extension method
                    command.CommandText = sql.AppendIdentitySelect(); // Extension method

                    connection.Open();

                    int id = Convert.ToInt32(command.ExecuteScalar());

                    return id;
                }
            }
        }

        public static void Update(string sql, object[] parms = null)
        {
            sql = string.Format(sql, GetParameterPrefix());

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.SetParameters(parms);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string sql, object[] parms = null)
        {
            Update(sql, parms);
        }

        public static List<T> GetAll<T>(string sql, ConverterDelegate<T> convert, object[] parms = null)
        {
            sql = string.Format(sql, GetParameterPrefix());

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.SetParameters(parms);

                    connection.Open();

                    var list = new List<T>();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var obj = convert(reader);
                        list.Add(obj);
                    }

                    return list;
                }
            }
        }

        public static T Get<T>(string sql, ConverterDelegate<T> convert, object[] parms = null)
        {
            sql = string.Format(sql, GetParameterPrefix());

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sql;
                    command.SetParameters(parms);  // Extension method

                    connection.Open();

                    T t = default(T);

                    var reader = command.ExecuteReader();

                    if (reader.Read())
                        t = convert(reader);

                    return t;
                }
            }
        }

        #region Private methods

        private static void SetParameters(this DbCommand command, object[] parms)
        {
            if (parms != null && parms.Length > 0)
            {
                for (int i = 0; i < parms.Length; i += 2)
                {
                    string name = GetParameterPrefix() + parms[i].ToString();

                    if (parms[i + 1] is string && (string)parms[i + 1] == "")
                        parms[i + 1] = null;

                    object value = parms[i + 1] ?? DBNull.Value;

                    var dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = name;
                    dbParameter.Value = value;

                    command.Parameters.Add(dbParameter);
                }
            }
        }

        private static string AppendIdentitySelect(this string sql)
        {
            switch (providerName)
            {
                // Microsoft Access não tem suporte a esse tipo de comando
                case "System.Data.OleDb": return sql;
                case "System.Data.SqlClient": return sql + ";SELECT SCOPE_IDENTITY()";
                case "System.Data.OracleClient": return sql + ";SELECT MySequence.NEXTVAL FROM DUAL";
                case "Firebird.Data.FbClient": return sql + ";GENERATOR(SEILAÁ)";
                default: return sql + ";SELECT @@IDENTITY";
            }
        }

        private static string GetParameterPrefix()
        {
            switch (providerName)
            {
                // Microsoft Access não tem suporte a esse tipo de comando
                case "System.Data.OleDb": return "@";
                case "System.Data.SqlClient": return "@";
                case "System.Data.OracleClient": return ":";
                case "MySql.Data.MySqlClient": return "?";

                default:
                    return "@";
            }

        }

        #endregion Private methods
    }
}
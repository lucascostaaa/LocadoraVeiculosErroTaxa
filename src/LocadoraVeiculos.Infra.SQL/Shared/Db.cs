using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;

namespace LocadoraVeiculos.Infra.SQL
{
    public static class Db
    {
        private static readonly string connectionString;
        private static readonly string nomeProvider;
        private static readonly DbProviderFactory fabricaProvedor;

        static Db()
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                var config = builder.Build();

                var bancoDeDados = config.GetSection("appSettings:bancoDeDados").Value;

                if (string.IsNullOrEmpty(bancoDeDados))
                    Log.Error("A configuração do banco de dados esta inválida: {bancoDeDados}", bancoDeDados);

                connectionString = config.GetConnectionString(bancoDeDados);
                if (string.IsNullOrEmpty(connectionString))
                    Log.Error("A configuração do endereço do banco de dados esta inválida: {connectionString}", connectionString);

                nomeProvider = config.GetSection("Providers")[bancoDeDados];
                if (string.IsNullOrEmpty(nomeProvider))
                    Log.Error("A configuração do provider do banco de dados no appSettings esta inválida: {nomeProvider}", connectionString);
            }
            catch (Exception ex)
            {
                var pathAppSettings = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

                Log.Error(ex, "Erro ao tentar carregar as configurações do arquivo de configuração: {pathAppSettings}", pathAppSettings);
            }

            try
            {
                DbProviderFactories.RegisterFactory(nomeProvider, SqlClientFactory.Instance);

                fabricaProvedor = DbProviderFactories.GetFactory(nomeProvider);
            }
            catch (Exception daex)
            {
                Log.Error(daex, "Erro ao tentar registrar o provider do banco de dados: {nomeProvider}", nomeProvider);
                throw;
            }

        }

        public static int Insert(string sql, Dictionary<string, object> parameters)
        {
            using (IDbConnection connection = fabricaProvedor.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (IDbCommand command = fabricaProvedor.CreateCommand())
                {
                    command.CommandText = sql.AppendSelectIdentity();
                    command.Connection = connection;
                    command.SetParameters(parameters);

                    connection.Open();

                    var id = Convert.ToInt32(command.ExecuteScalar());

                    return id;
                }
            }
        }

        public static void Update(string sql, Dictionary<string, object> parameters = null)
        {
            using (IDbConnection connection = fabricaProvedor.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (IDbCommand command = fabricaProvedor.CreateCommand())
                {
                    command.CommandText = sql;

                    command.Connection = connection;

                    command.SetParameters(parameters);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(string sql, Dictionary<string, object> parameters)
        {
            Update(sql, parameters);
        }
        public static List<T> GetAll<T>(string sql, Func<IDataReader, T> convert, Dictionary<string, object> parameters = null)
        {
            using (IDbConnection connection = fabricaProvedor.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (IDbCommand command = fabricaProvedor.CreateCommand())
                {
                    command.CommandText = sql;

                    command.Connection = connection;

                    command.SetParameters(parameters);

                    connection.Open();

                    var list = new List<T>();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var obj = convert(reader);
                            list.Add(obj);
                        }

                        return list;
                    }
                }
            }
        }

        public static T Get<T>(string sql, Func<IDataReader, T> convert, Dictionary<string, object> parameters)
        {
            using (IDbConnection connection = fabricaProvedor.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (IDbCommand command = fabricaProvedor.CreateCommand())
                {
                    command.CommandText = sql;

                    command.Connection = connection;

                    command.SetParameters(parameters);

                    connection.Open();

                    T t = default;

                    using (IDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                            t = convert(reader);

                        return t;
                    }
                }
            }
        }

        public static bool Exists(string sql, Dictionary<string, object> parameters)
        {
            using (IDbConnection connection = fabricaProvedor.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (IDbCommand command = fabricaProvedor.CreateCommand())
                {
                    command.CommandText = sql;

                    command.Connection = connection;

                    command.SetParameters(parameters);

                    connection.Open();

                    int numberRows = Convert.ToInt32(command.ExecuteScalar());

                    return numberRows > 0;
                }
            }
        }
        private static void SetParameters(this IDbCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return;

            foreach (var parameter in parameters)
            {
                string name = parameter.Key;

                object value = parameter.Value.IsNullOrEmpty() ? DBNull.Value : parameter.Value;

                IDataParameter dbParameter = command.CreateParameter();

                dbParameter.ParameterName = name;
                dbParameter.Value = value;

                command.Parameters.Add(dbParameter);
            }
        }
        private static string AppendSelectIdentity(this string sql)
        {
            switch (nomeProvider)
            {
                case "Microsoft.Data.SqlClient": return sql + ";SELECT SCOPE_IDENTITY()";

                case "System.Data.SQLite": return sql + ";SELECT LAST_INSERT_ROWID()";

                default: return sql;
            }
        }

        public static bool IsNullOrEmpty(this object value)
        {
            return (value is string && string.IsNullOrEmpty((string)value)) ||
                    value == null;
        }
    }
}

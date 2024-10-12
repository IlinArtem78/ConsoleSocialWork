using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Data;

namespace ConsoleSocialWork.DAL.Repositories
{
    public class BaseRepository
    {
        private string connectionString = "Host=localhost;Port=5432;Database=social_network_bd;Username=postgres;Password=!QAZ@WSX;";
      
            protected T QueryFirstOrDefault<T>(string sql, object parameters = null)
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return connection.QueryFirstOrDefault<T>(sql, parameters);
                }
            }

            protected List<T> Query<T>(string sql, object parameters = null)
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return connection.Query<T>(sql, parameters).ToList();
                }
            }

            protected int Execute(string sql, object parameters = null)
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    return connection.Execute(sql, parameters);
                }
            }

            private IDbConnection CreateConnection()
            {
                return new NpgsqlConnection(connectionString);
              //  SQLiteConnection("Data Source = DAL/DB/social_network.db; Version = 3");
            }
        }



    
}

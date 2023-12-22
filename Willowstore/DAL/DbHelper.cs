using Dapper;
using Npgsql;

namespace Willowstore.DAL
{
    public class DbHelper
    {
        public static string ConnString = "";

        public static async Task ExecuteAsync(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
        }

        public static async Task<T> QueryScalarAsync<T>(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<T>(sql, model);
            }
        }
    }
}

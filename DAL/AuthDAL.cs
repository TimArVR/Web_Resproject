using Dapper;
using Npgsql;
using Web_siteResume.DAL.Models;

namespace Web_siteResume.DAL
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<UserModel> GetUser(string email)
        {
            using (var connection = new NpgsqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                        select UserId, Email, Password, Salt, Status 
                        from AppUser 
                        where Email =@email", new { email = email }) ?? new UserModel();
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new NpgsqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<UserModel>(@"
                        select UserId, Email, Password, Salt, Status 
                        from AppUser 
                        where UserId =@id", new { id = id }) ?? new UserModel();
            }

        }

        public async Task<int> CreateUser(UserModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();
                string sql = @"INSERT INTO AppUser(Email, Password, Salt, Status)
                            VALUES(@Email, @Password, @Salt, @Status) returning UserId";
                return await connection.QuerySingleAsync<int>(sql, model);
            }
        }
    }
}

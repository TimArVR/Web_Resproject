﻿using Web_siteResume.DAL.Models;
using Dapper;
using Npgsql;

namespace Web_siteResume.DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        public async Task<int> Create(SessionModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();
                string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                      values (@DbSessionID, @SessionContent, @Created, @LastAccessed, @UserId)";

                return await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            using (var connection = new NpgsqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();
                string sql = @"select DbSessionID, SessionData, Created, LastAccessed, UserId 
                            from DbSession 
                            where DbSessionID = @sessionId";
                var sessions = await connection.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
                return sessions.FirstOrDefault();
            }
        }

        public async Task Lock(Guid sessionId)
        {
            using (var connection = new NpgsqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();
                string sql = @"select DbSessionID from DbSession where DbSessionID = @sessionId for update";
                await connection.QueryAsync<SessionModel>(sql, new { sessionId = sessionId });
            }
        }

        public async Task<int> Update(SessionModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.connString))
            {
                await connection.OpenAsync();
                string sql = @"update DbSession
                            set SessionData=@SessionData, LastAccessed=@LastAccessed, UserId=@UserId
                            where DbSessionId=@DbSessionId";
                return await connection.ExecuteAsync(sql, model);
            }
        }
    }
}

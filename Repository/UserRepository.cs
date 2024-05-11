using ControllerApiTutorial.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MinimalApiTutorial.Repository
{
    public class UserRepository
    {
        private readonly string _conString;
        public UserRepository(string conString)
        {
            _conString = conString;
        }

        public async Task<UserEntity> Login(string userName, string pw)
        {
            using IDbConnection con = new SqlConnection(_conString);
            string cmd = """
                SELECT * FROM "USER" WHERE user_name=@userName and pwd=@pw;
                """;
            return await con.QuerySingleAsync<UserEntity>(cmd, new { userName, pw });
        }


        public async Task<int> Register(UserEntity user)
        {
            using IDbConnection con = new SqlConnection(_conString);
            user.Created = DateTime.Now;
            string cmd = """
                INSERT INTO "USER" (
                    user_name, 
                    pwd, 
                    phone, 
                    email, 
                    gender, 
                    couple, 
                    is_delete, 
                    nick_name, 
                    create_date) 
                VALUES (
                    @User_Name, 
                    @Pwd, 
                    @Phone, 
                    @Email, 
                    @Gender, 
                    @CoupleId, 
                    0, 
                    @Nick_Name, 
                    @Created);
                """;
            return await con.ExecuteAsync(cmd, user);

        }

        public async Task<bool> Update(UserEntity user)
        {
            using IDbConnection con = new SqlConnection(_conString);
            string cmd = """
                UPDATE "USER" 
                SET user_name = @User_Name,
                    pwd = @Pwd,
                    phone = @Phone,
                    email = @Email,
                    gender = @Gender,
                    couple = @CoupleId,
                    nick_name = @Nick_Name
                WHERE
                    id = @Id
                """;
            int rows = await con.ExecuteAsync(cmd, user);
            return rows > 0;
        }
    }
}

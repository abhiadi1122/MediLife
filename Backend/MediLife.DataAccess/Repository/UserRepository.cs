using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediLife.DataAccess.Configurations;
using MediLife.DataAccess.IRepository;
using MediLife.DataObjects;

namespace MediLife.DataAccess.Repository
{
    public class UserRepository : BaseRepository,  IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(StoreProc.SP_SaveUserDetails, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@UserName", user.UserName);                    
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    command.Parameters.AddWithValue("@RoleId", 1);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public User GetUserByEmail(string email)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(StoreProc.SP_GetUserDetails, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = GetIntegerValue(reader["UserId"]),
                                FullName = reader["FullName"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                MobileNumber = reader["MobileNumber"].ToString(),
                                RoleName = reader["RoleName"].ToString(),
                                CreatedDate = GetDateTimeValue(reader["CreatedDate"])
                            };
                        }
                    }
                }
            }

            return user;
        }

    }
}

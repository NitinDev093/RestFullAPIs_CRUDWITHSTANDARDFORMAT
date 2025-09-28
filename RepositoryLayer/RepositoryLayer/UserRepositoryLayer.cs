using CommonLayer.RequestModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.IRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryLayer
{
    public class UserRepositoryLayer : IUserRepositoyLayer
    {
        private readonly string _connectionString;
        public UserRepositoryLayer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int CreateUser(UserRequestModel user)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_CreateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", (object?)user.LastName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Gender", (object?)user.Gender ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DOB", (object?)user.DOB ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Phone", (object?)user.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                con.Open();
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result); // returns the new UserID
            }
        }

        public DataTable GetUsers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return dt;
        }

        
    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UserCL.Services;
using UserRL.Interface;
using System.Data.SqlClient;
using System.Data;

namespace UserRL.Services
{
    public class UserRepositoryLayer : InterfaceUserRepositoryLayer
    {
        // Step 3: Configration for database : Install package : using Microsoft.Extensions.Configration
        private readonly IConfiguration configuration;
        // Step 4: Install package System.Data.SqlClient
        private SqlConnection conn = null;
        string constr = null;
        public UserRepositoryLayer(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // Step 5: Connection
        private void Connection()
        {
            try
            {
                // Step 6: Call the connection string
                constr = configuration.GetSection("ConnectionStrings").GetSection("UserContext").Value;
                conn = new SqlConnection(constr);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Return_Data method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add_Data(User data)
        {
            try
            {
                // Step 7: Connect to stored procedure and add in column
                Connection();
                SqlCommand command = new SqlCommand("spUserDetails", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Address", data.Address);
                command.Parameters.AddWithValue("@EmailId", data.EmailId);
                command.Parameters.AddWithValue("@FirstName", data.FirstName);
                command.Parameters.AddWithValue("@LastName", data.LastName);
                command.Parameters.AddWithValue("@UserName", data.UserName);
                command.Parameters.AddWithValue("@UserId", data.UserId);
                command.Parameters.AddWithValue("@Passward", data.Passward);
                // Open Connection UserDatails Table
                conn.Open();
                // Returns 1 for successful run and 0 For unsuccesful run
                int response = command.ExecuteNonQuery();
                conn.Close();
                if (response <= 1)
                {
                    return "Add Successful";

                }
                else
                {
                    return "Add Fail";
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }            
        }     
    }
}

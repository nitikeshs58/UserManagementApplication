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
        public bool Add_Data(User data)
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
                if (response != 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }            
        }

        /// <summary>
        /// UserName and Passwars to check it is correct or not.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UserLogin(User data)
        {
            try
            {
                // Step 7: Connect to stored procedure and add in column
                Connection();
                SqlCommand command = new SqlCommand("spUserLogin", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", data.UserName);
                command.Parameters.AddWithValue("@Passward", data.Passward);
                // Open Connection UserDatails Table
                conn.Open();
                // Execute command
                int response = command.ExecuteNonQuery();
                conn.Close();
                if (response!=1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public dynamic UserList()
        {
            Connection();
            // Create insatance of Stored procedure
            SqlCommand command = new SqlCommand("spUserList", conn);
            command.CommandType = CommandType.StoredProcedure;
            // Calling Get data method
            return GetData(command);
        }

        public dynamic GetData(SqlCommand command)
        {
            // command.CommandType = CommandType.StoredProcedure;
            List<User> list = new List<User>();
            //open connection EmployeeTable
            conn.Open();
            //data from databse return in reder
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var User = new User
                {
                    //get oridinal is return the name of column on the basis of case insensative
                    //getstring retun the data in sting or particular type 
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                    EmailId = reader.GetString(reader.GetOrdinal("EmailId")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),                    
                    Address = reader.GetString(reader.GetOrdinal("Address"))
                };
                list.Add(User);
            }
            conn.Close();
            return list;
        }

        /// <summary>
        /// Sending Old Emaild and New Passward
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ForgotPassward(User data)
        {
            try
            {
                Connection();
                SqlCommand command = new SqlCommand("spForgotPassward", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailId", data.EmailId);
                command.Parameters.AddWithValue("@Passward", data.Passward);
                // Open Connection UserDatails Table
                conn.Open();
                // Returns 1 for successful run and 0 For unsuccesful run
                int response = command.ExecuteNonQuery();
                conn.Close();
                if (response != 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }
    }
}

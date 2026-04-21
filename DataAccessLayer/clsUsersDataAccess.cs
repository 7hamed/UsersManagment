using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsUsersDataAccess
    {

        public static bool GetUserInfoByID(int id, ref string userName, ref string email, ref string password, ref string imagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from Users where ID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    userName = (string)reader["UserName"];
                    email = (string)reader["Email"];
                    password = (string)reader["Password"];

                    if (reader["ImagePath"] == DBNull.Value)
                        imagePath = "";
                    else
                        imagePath = (string)reader["ImagePath"];
                }
                else
                {
                    isFound = false;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewUser(string userName, string email, string password, string imagePath)
        {
            int userID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert into Users(UserName, Email, Password, ImagePath) values (@userName, @email, @password, @imagePath);
                            select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@userName", userName);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);

            if (imagePath != "")
                command.Parameters.AddWithValue("@imagePath", imagePath);
            else
                command.Parameters.AddWithValue("@imagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    userID = insertedID;
                }
                else
                {
                    userID = -1;
                }
            }
            catch (Exception ex)
            {
                userID = -1;
            }
            finally
            {
                connection.Close();
            }

            return userID;
        }

        public static bool DeleteContactByID(int id)
        {
            int affectedRow = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "delete from Users where ID = @id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                connection.Open();

                affectedRow = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                affectedRow = 0;
            }
            finally
            {
                connection.Close();
            }

            return (affectedRow > 0);
        }

        public static bool UpdateUserByID(int id, string userName, string email, string password, string imagePath)
        {
            int affectedRow = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users
                            SET UserName = @userName,
                                Email = @email,
                                Password = @password,
                                ImagePath = @imagePath
                            WHERE ID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@userName", userName);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);

            if (imagePath != "")
                command.Parameters.AddWithValue("@imagePath", imagePath);
            else
                command.Parameters.AddWithValue("@imagePath", System.DBNull.Value);

            try
            {
                connection.Open();

                affectedRow = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                affectedRow = 0;
            }
            finally
            {
                connection.Close();
            }

            return (affectedRow > 0);
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Users";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                else
                {
                    dt = null;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                dt = null;
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

    }
}

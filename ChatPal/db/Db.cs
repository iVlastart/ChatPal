using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatPal.db
{
    internal static class Db
    {
        internal static void addUser(string username, string password) 
        { 
            SqlConnection con = new(Enviro.CONNECT()); 
            try 
            { 
                openCon(con); 
                string query = "INSERT INTO Users (ID, Username, Password) VALUES (@ID, @username, @password)"; 
                using (SqlCommand cmd = new(query, con)) 
                { 
                    cmd.Parameters.AddWithValue("@ID", makeID(username)); 
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); 
                    cmd.ExecuteNonQuery(); 
                } 
                MessageBox.Show("Added"); 
            } 
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "addUser ERROR", MessageBoxButton.OK); 
            } 
            finally 
            { 
                con.Close(); 
            } 
        }

        internal static void logUser(string username, string password)
        {
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                openCon(con);
                if (!checkIfUserExists(username))
                {
                    string query = "SELECT * FROM Users WHERE Username=@username AND Password=@password";
                    using (SqlCommand cmd = new(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "logUser ERROR", MessageBoxButton.OK);
            }
            finally
            {
                con.Close();
            }
        }

        private static string makeID(string username)
        {
            Random random = new();
            var usernameBytes = System.Text.Encoding.UTF8.GetBytes(username);
            var id1stPart = System.Convert.ToBase64String(usernameBytes);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var id2ndPart = Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray();
            return $"{id1stPart}.{new string(id2ndPart)}";
        }

        private static string checkErrors(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return "Please provide a username";
            }
            else if (string.IsNullOrEmpty(password))
            {
                return "Please provide a password";
            }
            else if(false)
            {
                return $"{username} was not found";
            }
            else
            {
                return "";
            }
        }

        private static bool checkIfUserExists(string username)
        {
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                openCon(con);
                string query = "SELECT Username FROM Users WHERE Username=@username";
                using (SqlCommand cmd = new(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows) return true;
                        else return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "findUserByUsername ERROR", MessageBoxButton.OK);
                return false;
            }
            finally { con.Close(); }

        }

        private static void openCon(SqlConnection con)
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }
    }
}

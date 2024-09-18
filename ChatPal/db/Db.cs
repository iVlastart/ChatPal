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
        internal static void addUser(string ID, string username, string password)
        {
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                openCon(con);
                string query = "INSERT INTO Users (ID, Username, Password) VALUES (@ID, @username, @password)";
                using(SqlCommand cmd = new(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", makeID(username));
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
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

        //change this to private later after checking if it works
        internal static string makeID(string username)
        {
            Random random = new();
            var usernameBytes = System.Text.Encoding.UTF8.GetBytes(username);
            var id1stPart =  System.Convert.ToBase64String(usernameBytes);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var id2ndPart = Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray();
            return $"{id1stPart}.{id2ndPart}";
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
            //also check if the user does not exist
            return "";
        }

        private static string findUserByUsername(string username)
        {
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                openCon(con);
                string query = "SELECT Username FROM Users WHERE Username=@username";
                using(SqlCommand cmd = new(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                }
                return "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "findUserByUsername ERROR", MessageBoxButton.OK);
                return "";
            }
            finally { con.Close(); }
            
        }

        private static void openCon(SqlConnection con)
        {
            if(con.State == System.Data.ConnectionState.Closed) con.Open();
        }
    }
}

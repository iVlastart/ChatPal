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
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                using (SqlCommand checkCmd = new(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int userExists = (int)checkCmd.ExecuteScalar();

                    if (userExists > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose another.");
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO Users (ID, Username, Password) VALUES (@ID, @username, @password)";
                        using (SqlCommand cmd = new(insertQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@ID", makeID(username));
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@password", password);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("User added successfully.");
                    }
                }
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

        internal static bool logUser(string username, string password)
        {
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                openCon(con);
                string query = "SELECT * FROM Users WHERE Username=@username AND Password=@password";
                using (SqlCommand cmd = new(query, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "logUser ERROR", MessageBoxButton.OK);
                return false;
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

        internal static string getID(string username)
        {
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                openCon(con);
                string query = "SELECT ID FROM Users WHERE Username=@Username";
                using (SqlCommand cmd = new(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    return cmd.ExecuteScalar().ToString() ?? string.Empty;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "getID error", MessageBoxButton.OK);
                return "";
            }
            finally
            {
                con.Close();
            }
        }

        internal static string getUsername(string userID)
        {
            string username = string.Empty;
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                openCon(con);
                string query = "SELECT Username FROM Users WHERE ID=@ID";
                using(SqlCommand cmd = new(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", userID);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            username = (string)reader["Username"];
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Username ERROR", MessageBoxButton.OK);
            }
            finally
            {
                con.Close();
            }
            return username;
        }

        internal static string checkErrors(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return "Please provide a username";
            }
            else if (string.IsNullOrEmpty(password))
            {
                return "Please provide a password";
            }
            else
            {
                return "";
            }
        }

        internal static bool checkIfUserExists(string username)
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

        internal static void addMsg(string userID, string msg)
        {
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                if (!string.IsNullOrEmpty(userID) && !string.IsNullOrEmpty(msg))
                {
                    openCon(con);
                    string query = "INSERT INTO Msgs (UserID, Msg) VALUES (@UserID, @Msg)";
                    using (SqlCommand cmd = new(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@Msg", msg); //good luck vlasta :D from Mr.J
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Message ERROR", MessageBoxButton.OK);
            }
            finally
            {
                con.Close();
            }
        }

        internal static string[][] getMsgs()
        {
            List<string[]> data = new();
            SqlConnection con = new(Enviro.CONNECT());
            try
            {
                openCon(con);
                string query = "SELECT userID, Msg FROM Msgs";
                using(SqlCommand cmd = new(query, con))
                {
                    cmd.ExecuteNonQuery();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            string userId = reader["userID"].ToString();
                            string msg = reader["Msg"].ToString();
                            data.Add(new string[] { userId, msg });
                        }
                    }
                }
                return data.ToArray();
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message, "Get Message ERROR", MessageBoxButton.OK);
                return Array.Empty<string[]>();
            }
            finally
            {
                con.Close();
            }
        }

        internal static void delMsg(string username, string content)
        {
            SqlConnection con = new(Enviro.CONNECT());
            string ID = getID(username);
            try
            {
                openCon(con);
                string query = "DELETE FROM Msgs WHERE Msg=@Msg AND UserID=@UserID";
                using(SqlCommand cmd = new(query, con))
                {
                    cmd.Parameters.AddWithValue("@Msg", content);
                    cmd.Parameters.AddWithValue("@UserID", ID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message, "Delete Message ERROR", MessageBoxButton.OK);
            }
            finally
            {
                con.Close();
            }
        }

        private static void openCon(SqlConnection con)
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }
    }
}

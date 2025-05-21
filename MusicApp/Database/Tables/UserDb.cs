using MusicApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MusicApp.Database.Tables
{
    internal class UserDb
    {

        public void add(User user)
        {
            string query = @"
                INSERT INTO User (username, password)
                VALUES (@User, @Password)";
            using (var connection = new MySqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@User", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        
                        command.ExecuteNonQuery();

                        Console.WriteLine("User: " + user.Username + " guardado con exito");
                    }
                    catch (MySqlException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }

        }

        public User GetUserByUsername(string username)
        {
            string query = "SELECT Id, username, password FROM User WHERE username = @Username";
            using (var connection = new MySqlConnection(DbConfig.ConnectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(reader.GetString("username"), reader.GetString("Password"))
                            {
                                Id = reader.GetInt32("Id"),
                                Username = reader.GetString("Username"),
                                Password = reader.GetString("Password")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Proyecto_1_TV_Track.Models;

namespace Proyecto_1_TV_Track.Data
{
    public class UserRepository
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lista_100_usuarios.csv"); // Ensure correct file path

        /// <summary>
        /// Retrieves all users from the CSV file (without IDs).
        /// </summary>
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            try
            {
                // Ensure the file exists, or create it with a header
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("⚠ CSV file does not exist! Creating a new one...");
                    File.WriteAllText(filePath, "Username,Role\n");
                    return users;
                }

                var lines = File.ReadAllLines(filePath).Skip(1); // Skip header row

                foreach (var line in lines)
                {
                    var data = line.Split(',');

                    if (data.Length >= 2)
                    {
                        string username = data[0].Trim();
                        string role = data[1].Trim();
                        users.Add(new User(username, role));

                        // Debugging: Print loaded users
                        Console.WriteLine($"✅ Loaded User: Username={username}, Role={role}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error reading users: {ex.Message}");
            }

            return users;
        }

        /// <summary>
        /// Adds a new user to the CSV file (without ID).
        /// </summary>
        public void AddUser(string username, string role)
        {
            try
            {
                // Ensure the file exists with a header
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "Username,Role\n"); // Ensure header exists
                }

                string newUserEntry = $"{username},{role}";

                // Append the new user
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(newUserEntry);
                }

                Console.WriteLine($"✅ User {username} added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error saving user: {ex.Message}");
            }
        }
    }
}

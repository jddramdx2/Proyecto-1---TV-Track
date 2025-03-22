using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Proyecto_1_TV_Track.Models;

namespace Proyecto_1_TV_Track.Data
{
    /// <summary>
    /// Acceso al archivo de usuarios
    /// </summary>
    public class UserRepository
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lista_100_usuarios.csv"); // ruta de csv usuarios
        
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            try
            {
                // se asegura que el archivo exista
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No hay archivo csv");
                    File.WriteAllText(filePath, "Username,Role\n");
                    return users;
                }

                var lines = File.ReadAllLines(filePath).Skip(1); //  Ignorar encabezados

                foreach (var line in lines)
                {
                    var data = line.Split(',');

                    if (data.Length >= 2)
                    {
                        string username = data[0].Trim();
                        string role = data[1].Trim();
                        users.Add(new User(username, role));

                        // identificacion de usuario
                        Console.WriteLine($"Loaded User: Username={username}, Role={role}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo identificar el usuario: {ex.Message}");
            }

            return users;
        }

        /// <summary>
        /// Agrega nuevos usuarios a el archivo CSV
        /// </summary>
        public void AddUser(string username, string role)
        {
            try
            {
              
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "Username,Role\n"); 
                }

                string newUserEntry = $"{username},{role}";

                // Agrega nuevo contenido sin borrar existente
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(newUserEntry);
                }

                Console.WriteLine($"✅ Usuario {username} agregado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error salvando usuario: {ex.Message}");
            }
        }
    }
}

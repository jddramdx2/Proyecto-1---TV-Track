﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Proyecto_1_TV_Track.Models;

namespace Proyecto_1_TV_Track.Data

{

    /// <summary>

    /// acceso al CSV de usuarios

    /// </summary>

    public class UserRepository

    {

        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lista_100_usuarios.csv"); // ruta de csv usuarios

        public List<User> GetUsers()

        {

            List<User> users = new List<User>();

            try

            {

                // Verifica si el archivo existe

                if (!File.Exists(filePath))

                {

                    Console.WriteLine("No hay archivo csv");

                    File.WriteAllText(filePath, "Username,Role\n");

                    return users;

                }

                var lines = File.ReadAllLines(filePath).Skip(1); //  Ignora encabezados

                foreach (var line in lines)

                {

                    if (string.IsNullOrWhiteSpace(line)) continue;

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

        /// Registro de nuevos usuarios en el archivo CSV

        /// </summary>

        public void AddUser(string username, string role)

        {

            try

            {

                if (!File.Exists(filePath))

                {

                    File.WriteAllText(filePath, "Username,Role\n");

                }

                // Revisa si ya existe el usuario antes de agregarlo

                var existingUsers = GetUsers();

                bool userExists = existingUsers.Any(u => u.Name.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (userExists)

                {

                    Console.WriteLine($"⚠ El usuario '{username}' ya existe.");

                    return;

                }

                string newUserEntry = $"{username},{role}";

                // Agrega nuevo contenido sin eliminar el que ya existe

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

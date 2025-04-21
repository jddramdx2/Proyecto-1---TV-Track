using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using TVTrackWeb.Models;

namespace TVTrackWeb.Services
{
    public class Neo4jService
    {
        private readonly IDriver _driver;

        public Neo4jService()
        {
            // conexión con Neo4j - usuario y clave por defecto
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "1234"));
        }

        // este método mete un usuario nuevo a la base
        public async Task AgregarUsuario(string name, string role)
        {
            var session = _driver.AsyncSession();
            try
            {
                var query = "CREATE (u:User {name: $name, role: $role})";
                var parameters = new { name, role };
                await session.RunAsync(query, parameters);
                Console.WriteLine($"✅ Usuario creado: {name} - {role}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al agregar usuario: {ex.Message}");
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        // este saca todos los usuarios que hay guardados
        public async Task<List<User>> ObtenerUsuarios()
        {
            var lista = new List<User>();
            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync("MATCH (u:User) RETURN u.name AS name, u.role AS role");

                await result.ForEachAsync(record =>
                {
                    lista.Add(new User(
                        record["name"].As<string>(),
                        record["role"].As<string>()
                    ));
                });

                Console.WriteLine($"📋 Usuarios cargados: {lista.Count}");
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener usuarios: {ex.Message}");
                return lista;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        // este sirve para revisar si un usuario existe (para login)
        public async Task<bool> ValidarCredenciales(string name, string role)
        {
            var session = _driver.AsyncSession();
            try
            {
                var query = "MATCH (u:User {name: $name, role: $role}) RETURN u";
                var result = await session.RunAsync(query, new { name, role });

                bool encontrado = await result.FetchAsync();
                Console.WriteLine($"🔎 Validando {name} - {role}: {(encontrado ? "✅ Válido" : "❌ No encontrado")}");
                return encontrado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Error al validar credenciales: {ex.Message}");
                return false;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }
}

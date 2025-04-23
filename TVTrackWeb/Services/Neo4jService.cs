using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Neo4j.Driver;
using Proyecto_1_TV_Track.Models;
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
                var query = "CREATE (u:User {name: $name, role: $role, fechaCreacion: datetime()})";
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
        public async Task ImportarPeliculasDesdeCSV(string rutaCsv)
        {
            var session = _driver.AsyncSession();

            try
            {
                var lines = File.ReadAllLines(rutaCsv);
                for (int i = 1; i < lines.Length; i++)
                {
                    var data = lines[i].Split(',');
                    if (data.Length < 3) continue;

                    string title = data[0].Trim();
                    string genre = data[1].Trim();
                    string platform = data[2].Trim();

                    var query = @"MERGE (m:Peliculas {title: $title}) 
                          SET m.genre = $genre, m.platform = $platform";
                    await session.RunAsync(query, new { title, genre, platform });
                }

                Console.WriteLine("✅ Películas importadas desde CSV.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<Movie>> ObtenerPeliculas()
        {
            var lista = new List<Movie>();
            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync("MATCH (m:Peliculas) RETURN m.Peliculas AS title , m.Genero AS genre, m.Plataforma AS platform");

                await result.ForEachAsync(record =>
                {
                    lista.Add(new Movie(
                        record["title"].As<string>(),
                        record["genre"].As<string>(),
                        record["platform"].As<string>()
                        //Peliculas, Genero, Plataforma///
                    ));
                });

                return lista;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async Task CalificarPelicula(string titulo, double rating, string viewStatus)
        {
            var session = _driver.AsyncSession();
            try
            {
                var recomendado = (rating >= 4.0 && viewStatus != "No Vista");

                var query = @"MATCH (m:Movie {title: $titulo})
                      SET m.rating = $rating,
                          m.viewStatus = $viewStatus,
                          m.isRecommended = $recomendado";
                await session.RunAsync(query, new { titulo, rating, viewStatus, recomendado });
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task AgregarComentario(string username, string movieTitle, string texto)
        {
            var session = _driver.AsyncSession();
            try
            {
                var query = @"
            MATCH (u:User {name: $username}), (m:Movie {title: $movieTitle})
            CREATE (c:Comment {text: $texto, fecha: datetime()})
            CREATE (u)-[:COMMENTED]->(c)-[:ABOUT]->(m)";
                await session.RunAsync(query, new { username, movieTitle, texto });
                Console.WriteLine("✅ Comentario guardado.");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async Task<List<Comment>> ObtenerComentarios(string movieTitle)
        {
            var lista = new List<Comment>();
            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(@"
            MATCH (c:Comment)-[:ABOUT]->(:Movie {title: $movieTitle})
            RETURN c.text AS text, c.fecha AS fecha
            ORDER BY c.fecha DESC", new { movieTitle });

                await result.ForEachAsync(record =>
                {
                    lista.Add(new Comment(record["text"].As<string>())
                    {
                        Fecha = record["fecha"].As<ZonedDateTime>().ToDateTimeOffset().DateTime
                    });
                });

                return lista;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async Task ExportarPeliculasRecomendadasCSV(string rutaArchivo)
        {
            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(@"
            MATCH (m:Movie)
            WHERE m.isRecommended = true
            RETURN m.title AS title, m.genre AS genre, m.platform AS platform, m.rating AS rating, m.viewStatus AS status");

                using (var writer = new StreamWriter(rutaArchivo))
                {
                    await writer.WriteLineAsync("Título,Género,Plataforma,Rating,Estado");

                    await result.ForEachAsync(record =>
                    {
                        var linea = $"{record["title"].As<string>()},{record["genre"].As<string>()},{record["platform"].As<string>()},{record["rating"]?.As<double>() ?? 0},{record["status"]?.As<string>() ?? "No vista"}";
                        writer.WriteLine(linea);
                    });
                }

                Console.WriteLine("📁 CSV de recomendaciones exportado.");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async Task<int> ContarUsuariosUltimoMes()
        {
            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(@"
            MATCH (u:User)
            WHERE u.fechaCreacion >= datetime() - duration({days: 30})
            RETURN count(u) AS total");

                var record = await result.SingleAsync();
                return record["total"].As<int>();
            }
            catch
            {
                return 0;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async Task<List<Movie>> ObtenerPeliculasTop()
        {
            var lista = new List<Movie>();
            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(@"
            MATCH (m:Movie)
            WHERE exists(m.rating)
            RETURN m.title AS title, m.genre AS genre, m.platform AS platform, m.rating AS rating
            ORDER BY m.rating DESC
            LIMIT 5");

                await result.ForEachAsync(record =>
                {
                    lista.Add(new Movie(
                        record["title"].As<string>(),
                        record["genre"].As<string>(),
                        record["platform"].As<string>(),
                        record["rating"].As<double>()
                    ));
                });

                return lista;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async Task<Dictionary<string, int>> ObtenerGeneroMasVisto()
        {
            var resultado = new Dictionary<string, int>();
            var session = _driver.AsyncSession();

            try
            {
                var result = await session.RunAsync(@"
            MATCH (m:Movie)
            WHERE m.viewStatus = 'Vista'
            RETURN m.genre AS genre, count(*) AS cantidad
            ORDER BY cantidad DESC
            LIMIT 5");

                await result.ForEachAsync(record =>
                {
                    resultado[record["genre"].As<string>()] = record["cantidad"].As<int>();
                });

                return resultado;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }
}

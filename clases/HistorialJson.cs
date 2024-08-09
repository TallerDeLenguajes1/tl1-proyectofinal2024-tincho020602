using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EspacioPersonajes;

namespace EspacioHistorial
{
    public class Ganador
    {
        public Personaje personajeGanador { get; set; }
        public string fechaVictoria { get; set; } // Almacenar la fecha como cadena

        public Ganador() { }

        public Ganador(Personaje ganador, string victoria)
        {
            personajeGanador = ganador;
            fechaVictoria = victoria;
        }
    }

    public class HistorialJson
    {
        public void GuardarGanador(Personaje ganador, DateTime fecha, string nombreArchivo)
        {
            try
            {
                List<Ganador> ganadores = Existe(nombreArchivo) ? LeerGanadores(nombreArchivo) : new List<Ganador>();

                // Convertir la fecha a una cadena en el formato "yyyy-MM-dd"
                string fechaFormateada = fecha.ToString("yyyy-MM-dd");

                ganadores.Add(new Ganador(ganador, fechaFormateada));

                var opciones = new JsonSerializerOptions { WriteIndented = true };
                using (var archivo = new FileStream(nombreArchivo, FileMode.Create))
                {
                    using (var strWriter = new StreamWriter(archivo))
                    {
                        string json = JsonSerializer.Serialize(ganadores, opciones);
                        strWriter.WriteLine(json);
                    }
                }
                Console.WriteLine($"Datos guardados en '{nombreArchivo}'.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al guardar el archivo '{nombreArchivo}': {e.Message}");
            }
        }

        public List<Ganador> LeerGanadores(string nombreArchivo)
        {
            List<Ganador> ganadores = new List<Ganador>();
            try
            {
                using (var archivoOpen = new FileStream(nombreArchivo, FileMode.Open))
                {
                    using (var strReader = new StreamReader(archivoOpen))
                    {
                        string json = strReader.ReadToEnd();
                        ganadores = JsonSerializer.Deserialize<List<Ganador>>(json);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Archivo '{nombreArchivo}' no encontrado. No hay ganadores registrados.");
                Console.WriteLine("Debe jugar al menos una partida para guardar un ganador.");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error al deserializar el archivo '{nombreArchivo}': {jsonEx.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al leer el archivo '{nombreArchivo}': {e.Message}");
            }
            return ganadores;
        }

        public bool Existe(string nombreArchivo)
        {
            try
            {
                return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al verificar el archivo '{nombreArchivo}': {e.Message}");
                return false;
            }
        }
    }
}

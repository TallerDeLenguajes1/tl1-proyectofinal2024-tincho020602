// Program.cs
using EspacioFabricaPj;
using EspacioPersonajes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LaBatallaDeSpringfield
{
    // Clase PersonajesJson
    public class PersonajesJson
    {
        public static void GuardarPersonajes(List<Personaje> personajes, string archivo)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(personajes, opciones);
            File.WriteAllText(archivo, json);
        }

        public static List<Personaje> LeerPersonajes(string archivo)
        {
            if (File.Exists(archivo))
            {
                string json = File.ReadAllText(archivo);
                return JsonSerializer.Deserialize<List<Personaje>>(json);
            }
            return new List<Personaje>();
        }

        public static bool Existe(string archivo)
        {
            return File.Exists(archivo) && new FileInfo(archivo).Length > 0;
        }
    }

    // Clase HistorialJson
    public class HistorialJson
    {
        public static void GuardarGanador(Personaje ganador, string archivo)
        {
            List<Personaje> ganadores = LeerGanadores(archivo);
            ganadores.Add(ganador);
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(ganadores, opciones);
            File.WriteAllText(archivo, json);
        }

        public static List<Personaje> LeerGanadores(string archivo)
        {
            if (File.Exists(archivo))
            {
                string json = File.ReadAllText(archivo);
                return JsonSerializer.Deserialize<List<Personaje>>(json);
            }
            return new List<Personaje>();
        }

        public static bool Existe(string archivo)
        {
            return File.Exists(archivo) && new FileInfo(archivo).Length > 0;
        }
    }

    // Clase Combate
    public class Combate
    {
        public Personaje Personaje1 { get; set; }
        public Personaje Personaje2 { get; set; }

        public Combate(Personaje personaje1, Personaje personaje2)
        {
            Personaje1 = personaje1;
            Personaje2 = personaje2;
        }

        public void IniciarCombate()
        {
            while (Personaje1.Salud > 0 && Personaje2.Salud > 0)
            {
                Personaje1.Atacar(Personaje2);
                if (Personaje2.Salud > 0)
                {
                    Personaje2.Atacar(Personaje1);
                }
            }

            if (Personaje1.Salud > 0)
            {
                Console.WriteLine($"{Personaje1.Nombre} ha ganado el combate!");
                MejorarPersonaje(Personaje1);
                HistorialJson.GuardarGanador(Personaje1, "ganadores.json");
            }
            else
            {
                Console.WriteLine($"{Personaje2.Nombre} ha ganado el combate!");
                MejorarPersonaje(Personaje2);
                HistorialJson.GuardarGanador(Personaje2, "ganadores.json");
            }
        }

        private void MejorarPersonaje(Personaje ganador)
        {
            ganador.Mejorar();
        }
    }

    // Clase EventoAleatorio
    public class EventoAleatorio
    {
        public static void GenerarEvento(Personaje personaje)
        {
            Random random = new Random();
            int evento = random.Next(1, 4);

            switch (evento)
            {
                case 1:
                    Console.WriteLine("Maggie aparece y restaura 10 puntos de salud a su familia.");
                    personaje.ModificarSalud(10);
                    break;
                case 2:
                    Console.WriteLine("El Sr. Burns libera a los perros, causando 5 puntos de daño.");
                    personaje.ModificarSalud(-5);
                    break;
                case 3:
                    Console.WriteLine("Apu regala un Squishy, restaurando 5 puntos de salud.");
                    personaje.ModificarSalud(5);
                    break;
            }
        }
    }

    // Clase principal Program
    class Program
    {
        static void Main(string[] args)
        {
            List<Personaje> personajes;

            if (PersonajesJson.Existe("personajes.json"))
            {
                personajes = PersonajesJson.LeerPersonajes("personajes.json");
            }
            else
            {
                personajes = new List<Personaje>();
                for (int i = 0; i < 10; i++)
                {
                    personajes.Add(FabricaDePersonajes.CrearPersonajeAleatorio());
                }
                PersonajesJson.GuardarPersonajes(personajes, "personajes.json");
            }

            foreach (var personaje in personajes)
            {
                Console.WriteLine($"Nombre: {personaje.Nombre}, Salud: {personaje.Salud}");
            }

            Combate combate = new Combate(personajes[0], personajes[1]);
            combate.IniciarCombate();
        }
    }
}
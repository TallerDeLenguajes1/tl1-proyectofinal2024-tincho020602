using System;
using EspacioPersonajes;
 // Clase FabricaDePersonajes
namespace EspacioFabricaPj
{
    
    public class FabricaDePersonajes
    {
        private static Random random = new Random();

        public static Simpsons CrearPersonajeAleatorio()
        {
            string[] nombres = { "Homer", "Bart", "Lisa", "Marge", "Maggie", "Ned", "Mr. Burns", "Apu", "Krusty", "Milhouse" };
            string nombre = nombres[random.Next(nombres.Length)];
            string apodo = nombre + " the Great";
            string tipo = "Humano";
            DateTime fechaDeNacimiento = new DateTime(random.Next(1950, 2010), random.Next(1, 13), random.Next(1, 29));
            int edad = DateTime.Now.Year - fechaDeNacimiento.Year;
            int velocidad = random.Next(1, 11);
            int destreza = random.Next(1, 6);
            int fuerza = random.Next(1, 11);
            int nivel = random.Next(1, 11);
            int armadura = random.Next(1, 11);
            int salud = 100;

            return new Simpsons(nombre, apodo, tipo, velocidad, destreza, fuerza, nivel, armadura, salud);
        }
    }

}
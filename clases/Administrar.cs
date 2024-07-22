using EspacioPersonajes;
using EventoAleatorio;
using System.Text.Json;
using System.IO;

namespace EspacioJuego
{
    class Juego
    {
        //Atributos
        private List<Personaje> ListaDePersonajes { get; set; }
        private Personaje Jugador1 { get; set; }

        private Personaje Jugador2 { get; set; }

        private int Turno { get; set; }


        //Constructor
        public Juego()
        {
            ListaDePersonajes = CargarPersonajes();
        }
        //Metodos
        public void MostrarMenu(string[] opciones)
        {
            foreach (var opcion in opciones)
            {
                Console.WriteLine(opcion);
            }
        }
        private List<Personaje> CargarPersonajes()
        {
            //Leo el archivo y lo guardo como texto
            string personajes = File.ReadAllText("./json/personajes.json");
            //trasnformo el texto a una lista objeto
            List<Personaje> listita = JsonSerializer.Deserialize<List<Personaje>>(personajes);
            return listita;
        }


        private int RetornarTurno()
        {
            return new Random().Next(1, 3);
        }
        public void Duelo1vs1()
        {
            Jugador1=ListaDePersonajes[1];
            Jugador2=ListaDePersonajes[0];
            int turno = RetornarTurno();
            while (Jugador1.EstaVivo() && Jugador2.EstaVivo())
            {
                switch (turno)
                {
                    case 1:
                        //Ataca jugador1
                        Console.WriteLine("Ataca jugador 1");
                        turno = 2;
                        Jugador2.Defender(50);
                        break;
                    case 2:
                        //Ataca jugador2
                        Console.WriteLine("Ataca jugador 2");
                        turno = 1;
                        Jugador1.Defender(50);
                        break;
                    default:
                        break;
                }
            }
            if(Jugador1.EstaVivo()){
                Console.WriteLine("Gano Jugador 1");
            }else{
                Console.WriteLine("Gano Jugador 2");
            }
        }



    }

}

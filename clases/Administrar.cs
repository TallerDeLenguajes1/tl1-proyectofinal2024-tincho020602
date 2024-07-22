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

        private List<Evento> ListaDeEventos { get; set;}


        //Constructor
        public Juego()
        {
            ListaDePersonajes = CargarPersonajes();
            ListaDeEventos = CargarEventos();
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



         private List<Evento> CargarEventos()

        {
            //Leo el archivo y lo guardo como texto
            string eventos = File.ReadAllText("./json/eventos.json");
            //trasnformo el texto a una lista objeto
            List<Evento> listiDeEvento = JsonSerializer.Deserialize<List<Evento>>(eventos);
            return listiDeEvento;
        }


        private void TurnoJugador(Personaje atacante,Personaje defensor){
            Evento eventoRandom = GenerarEvento();
            int danio=atacante.Atacar(eventoRandom);
            defensor.Defender(danio);
        }

        private Evento GenerarEvento(){
            return ListaDeEventos[new Random().Next(0,ListaDeEventos.Count()-1)];
        }
        public void Duelo()
        {
            int turno = RetornarTurno();
            while (Jugador1.EstaVivo() && Jugador2.EstaVivo())
            {
                switch (turno)
                {
                    case 1:
                        //Ataca jugador1
                        Console.WriteLine("Ataca jugador 1");
                        TurnoJugador(Jugador1,Jugador2);
                        turno = 2;
                        break;
                    case 2:
                        //Ataca jugador2
                        Console.WriteLine("Ataca jugador 2");
                        TurnoJugador(Jugador2,Jugador1);
                        turno = 1;
                        break;
                    default:
                        break;
                }
            }
            //mostrar Ganador-------------------


        }



    }

}

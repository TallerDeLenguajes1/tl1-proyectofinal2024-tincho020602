using System.IO;
using System.Text.Json;
using EspacioPersonajes;
using EventoAleatorio;
using datosApi;

namespace EspacioJuego
{
    class Juego
    {
        //Atributos
        private List<Personaje> ListaDePersonajes { get; set; }
        private Personaje Jugador1 { get; set; }

        private Personaje Jugador2 { get; set; }

        private int Turno { get; set; }

        private List<Evento> ListaDeEventos { get; set; }

        //Constructor
        public Juego()
        {
            ListaDePersonajes = CargarPersonajes();
            ListaDeEventos = CargarEventos();
        }

        //Metodos
        public void MostrarMenu(string[] opciones)
        {
            Console.Clear();
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

        /*
            AQUI REALIZO UN CAMBIO
            en partida1vs1 creo una lista temporal p/q no se eliminen definitivamente los personajes
            el problema ahora es reajustar sus vidas je--Ya lo corregi
        */
        public Personaje SeleccionarPersonaje(List<Personaje> listaTemporal)
        {
            int opcion = 0;
            string opcionIngresada;
            bool opcionValida;
            Personaje elegido = new Personaje();

            do
            {
                int indice = 0;
                foreach (var personaje in listaTemporal)
                {
                    personaje.RestaurarVida();
                    Console.WriteLine(
                        $"{indice}) - {personaje.Nombre} - Apodo: {personaje.Apodo} - Edad: {personaje.Edad}"
                    );
                    Console.WriteLine(
                        $"     Salud: {personaje.Salud} - Fuerza: {personaje.Fuerza} - Velocidad De Ataque: {personaje.VelocidadDeAtaque}"
                    );
                    Console.WriteLine("------------------------------");
                    indice++;
                }
                Console.WriteLine("Opcion: ");
                opcionIngresada = Console.ReadLine();

                opcionValida =
                    int.TryParse(opcionIngresada, out opcion)
                    && (opcion >= 0 && opcion < listaTemporal.Count);

                if (opcionValida)
                {
                    elegido = listaTemporal[opcion];
                    listaTemporal.Remove(elegido);
                }
            } while (!opcionValida);

            Console.Clear();
            return elegido;
        }

        public void FinDelDuelo()
        {
            var apiInsultos= new ConsumirApi();
            string insulto=apiInsultos.ObtenerInsulto().GetAwaiter().GetResult();
            Console.Clear();
            if (Jugador1.EstaVivo())
            {
                Console.WriteLine("Gano Jugador1");
                Console.WriteLine($"\nEl Jugador2 dice: {insulto}");
                //Jugador1.RegenerarVida();
            }
            else
            {
                Console.WriteLine("Gano Jugador2");
                Console.WriteLine($"\nEl Jugador1 dice: {insulto}");
                //Jugador2.RegenerarVida();
            }
            Console.ReadKey();
        }

        public void IniciarJuego()
        {
            int opcion = 0;
            string opcionIngresada;
            do
            {
                do
                {
                    MostrarMenu(["1- 1vsCpu", "2- 1vs1", "3- historial", "4- Salir"]);
                    Console.WriteLine("Opcion: ");
                    opcionIngresada = Console.ReadLine();
                    Console.Clear();
                } while (
                    !(int.TryParse(opcionIngresada, out opcion)) && !(opcion >= 1 && opcion <= 4)
                );
                switch (opcion)
                {
                    case 1:
                        Partida1vsCPU();
                        break;
                    case 2:
                        Partida1vs1();
                        break;
                    case 3:
                        Console.WriteLine("Mostrar Ranking...");
                        break;
                    case 4:
                        Console.WriteLine("Salir...");
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            } while (opcion != 4);
        }

        private void Partida1vs1()
        {
            // Hacer una copia temporal de la lista de personajes
            List<Personaje> listaTemporalDePersonajes = new List<Personaje>(ListaDePersonajes);

            Console.WriteLine($"Elegir personaje para el Jugador1: ");
            Jugador1 = SeleccionarPersonaje(listaTemporalDePersonajes);
            Console.WriteLine($"Elegir personaje para el Jugador2: ");
            Jugador2 = SeleccionarPersonaje(listaTemporalDePersonajes);
            Duelo();
        }

        private void Partida1vsCPU()
        {
            List<Personaje> listaPersonajeDuelo = new List<Personaje>(ListaDePersonajes);
            Jugador1 = SeleccionarPersonaje(listaPersonajeDuelo);
            // Crear una instancia de Random
            Random random = new Random();
            // Obtener un índice aleatorio
            while (listaPersonajeDuelo.Count>0 && Jugador1.EstaVivo())
            {                
            Jugador1.RestaurarVida();
            int indiceAleatorio = random.Next(listaPersonajeDuelo.Count);
            Jugador2 = listaPersonajeDuelo[indiceAleatorio];
            listaPersonajeDuelo.Remove(Jugador2);
            Duelo();
            }
            FinDelDuelo();
        }

        private void TurnoJugador(Personaje atacante, Personaje defensor)
        {
            Evento eventoRandom = GenerarEvento();
            Console.WriteLine($"Ocurrio un evento: {eventoRandom.Nombre}\n");
            Console.WriteLine($"`{eventoRandom.Descripcion}`\n");
            if (eventoRandom.Efecto > 0)
            {
                Console.WriteLine($"El ataque se incrementa: {eventoRandom.Efecto}\n");
            }
            else
            {
                Console.WriteLine($"El ataque se reduce: {eventoRandom.Efecto}\n");
            }
            int danio = atacante.Atacar(eventoRandom);
                Console.WriteLine($"La Fuerza de ataque total es: {danio}\n");
            defensor.Defender(danio);
        }

        private Evento GenerarEvento()
        {
            return ListaDeEventos[new Random().Next(0, ListaDeEventos.Count() - 1)];
        }

        private void MostrarSalud()
        {
            Console.WriteLine(
                $"\nSalud: {Jugador1.Nombre}: {Jugador1.Salud} - {Jugador2.Nombre}: {Jugador2.Salud}\n"
            );
        }

        public void Duelo()
        {
            int turno = RetornarTurno();
            Console.Clear();
            Console.WriteLine($"Comienza el Jugador {turno}");
            Console.WriteLine("Oprima cualquier tecla para continuar...");
            Console.ReadKey();
            while (Jugador1.EstaVivo() && Jugador2.EstaVivo())
            {
                Console.Clear();
                if (turno == 1)
                {
                    MostrarSalud();
                    Console.WriteLine(
                        $">>Ataca {Jugador1.Nombre} - Fuerza de ataque: {Jugador1.Fuerza} - Velocidad de ataque: {Jugador1.VelocidadDeAtaque}"
                    );
                    TurnoJugador(Jugador1, Jugador2);
                    turno = 2;
                    Console.ReadKey();
                }
                else
                {
                    MostrarSalud();
                    Console.WriteLine(
                        $">>Ataca {Jugador2.Nombre} - Fuerza de ataque: {Jugador2.Fuerza} - Velocidad de ataque: {Jugador2.VelocidadDeAtaque}"
                    );
                    TurnoJugador(Jugador2, Jugador1);
                    turno = 1;
                    Console.ReadKey();
                }
            }
            // Mostrar Ganador
            FinDelDuelo();
        }
    }
}
/*
-Guardar datos
-Mostrar Ranking
-Api
-Estetica
-Agregar Funcionalidades*/
//Genera insultos: https://evilinsult.com/generate_insult.php?lang=en&type=json
//Cosas al azar
//Generador de insultos malvados
//En "Análisis de texto" estan los traductores
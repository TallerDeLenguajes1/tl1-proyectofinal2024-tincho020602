using System.IO;
using System.Text.Json;
using datosApi;
using EspacioHistorial;
using EspacioPersonajes;
using EventoAleatorio;

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
            //retorna 1 o 2
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

        public void MostrarHistorial()
        {
            HistorialJson historial = new HistorialJson();
            //leo la lista de ganadores que se encuentra en Historial.json
            List<Ganador> listaDeGanadores = historial.LeerGanadores("Historial.json");
            if (listaDeGanadores.Count > 0)
            {
                Console.WriteLine("\nHistorial de ganadores: \n");
                foreach (var ganador in listaDeGanadores)
                {
                    Console.WriteLine(
                        ganador.personajeGanador.Nombre + "-" + ganador.fechaVictoria
                    );
                }
            }
            Console.ReadKey();
        }

        public Personaje SeleccionarPersonaje(List<Personaje> listaTemporal)
        {
            int opcion = 0;
            string opcionIngresada;
            bool opcionValida;
            Personaje elegido = new Personaje();

            do
            {
                int indice = 0;
                // Itera a través de la lista temporal de personajes
                foreach (var personaje in listaTemporal)
                {
                    //Restauro la vida a cada personaje antes de cada duelo
                    personaje.RestaurarVida();
                    Console.WriteLine(
                        $"{indice}) - {personaje.Nombre} - Apodo: {personaje.Apodo} - Edad: {personaje.Edad}"
                    );
                    Console.WriteLine(
                        $"     Salud: {personaje.Salud} - Fuerza: {personaje.Fuerza} - Velocidad De Ataque: {personaje.VelocidadDeAtaque}"
                    );
                    Console.WriteLine("------------------------------------------------------");
                    indice++;
                }
                Console.WriteLine("Opcion: ");
                opcionIngresada = Console.ReadLine();

                opcionValida =
                    int.TryParse(opcionIngresada, out opcion)
                    && (opcion >= 0 && opcion < listaTemporal.Count);
                //Si ingreso bien la opcion del personaje, lo saco de la lista temporal
                if (opcionValida)
                {
                    elegido = listaTemporal[opcion];
                    listaTemporal.Remove(elegido);
                }
            } while (!opcionValida);

            Console.Clear();
            return elegido;
        }

        public Personaje RetornarGanador()
        {
            //Luego del duelo, verifico quien sobrevivio
            if (Jugador1.EstaVivo())
            {
                return Jugador1;
            }
            else
            {
                return Jugador2;
            }
        }

        public void FinDelDuelo()
        {
            //Guardo en ganador al jugador que sobrevivio al duelo
            Personaje ganador = RetornarGanador();
            HistorialJson historial = new HistorialJson();
            var apiInsultos = new ConsumirApi();
            //Llamo al método ObtenerInsulto de manera asíncrona y espera el resultado (un insulto) de forma sincrónica
            string insulto = apiInsultos.ObtenerInsulto().GetAwaiter().GetResult();
            Console.Clear();
            Console.WriteLine($"Gano {ganador.Nombre}");
            Console.WriteLine($"\nEl perdedor dice: {insulto}");
            //Guardo el personaje ganador junto con la fecha del duelo en el json
            historial.GuardarGanador(ganador, DateTime.Now.Date, "Historial.json");
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
                        MostrarHistorial();
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

            Console.WriteLine($"Elegir personaje para el Jugador 1: ");
            Jugador1 = SeleccionarPersonaje(listaTemporalDePersonajes);
            Console.WriteLine($"Elegir personaje para el Jugador 2: ");
            Jugador2 = SeleccionarPersonaje(listaTemporalDePersonajes);
            Duelo();
            FinDelDuelo();
        }

        private void Partida1vsCPU()
        {
            List<Personaje> listaPersonajeDuelo = new List<Personaje>(ListaDePersonajes);
            Jugador1 = SeleccionarPersonaje(listaPersonajeDuelo);
            // Crear una instancia de Random
            Random random = new Random();
            // Obtener un índice aleatorio
            while (listaPersonajeDuelo.Count > 0 && Jugador1.EstaVivo())
            {
                Jugador1.RestaurarVida(); //Restauro la vida antes de c/duelo
                int indiceAleatorio = random.Next(listaPersonajeDuelo.Count);
                Jugador2 = listaPersonajeDuelo[indiceAleatorio];
                listaPersonajeDuelo.Remove(Jugador2);
                Duelo();
            }
            FinDelDuelo();
        }

        private void TurnoJugador(Personaje atacante, Personaje defensor)
        {
            Evento eventoRandom = null;
            //Genero un numero random entre 1 y 3, si el numero=1, ocurrira un evento aleatorio durante el duelo
            if (new Random().Next(1, 4) == 1)
            {
                eventoRandom = GenerarEvento();
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
            }
            int danio = atacante.Atacar(eventoRandom);
            Console.WriteLine($"La Fuerza de ataque total es: {danio}\n");
            //El personaje defensor intenta defenderse, reduciendo su salud según el daño calculado
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
                        $">>Ataca {Jugador1.Nombre} - Fuerza de ataque: {Jugador1.Fuerza} + Velocidad de ataque: {Jugador1.VelocidadDeAtaque}"
                    );
                    TurnoJugador(Jugador1, Jugador2);
                    turno = 2;
                    Console.ReadKey();
                }
                else
                {
                    MostrarSalud();
                    Console.WriteLine(
                        $">>Ataca {Jugador2.Nombre} - Fuerza de ataque: {Jugador2.Fuerza} + Velocidad de ataque: {Jugador2.VelocidadDeAtaque}"
                    );
                    TurnoJugador(Jugador2, Jugador1);
                    turno = 1;
                    Console.ReadKey();
                }
            }
            Personaje ganador = RetornarGanador();
            Console.WriteLine($"Ganó {ganador.Nombre}.");
            Console.ReadKey();
        }
    }
}

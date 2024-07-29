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


    private void ElegirPersonaje(int cantidadJugadores)
    {
    Jugador1 = SeleccionarPersonaje(1);
    Console.WriteLine($"Usted selecciono el personaje: {Jugador1.Apodo}");
    Console.WriteLine("-------------------------------------");
    if (cantidadJugadores == 2)
    {
        Jugador2 = SeleccionarPersonaje(2);
        Console.WriteLine($"Usted selecciono el personaje: {Jugador2.Apodo}");
        Console.WriteLine("-------------------------------------");
    }
    }

    private Personaje SeleccionarPersonaje(int numeroJugador)
    {
    int opcion = 0;
    string opcionIngresada;
    do
    {
        Console.WriteLine($"Elegir personaje para el jugador {numeroJugador}: ");
        int indice = 0;
        foreach (var personaje in ListaDePersonajes)
        {
            Console.WriteLine($"{indice}) - {personaje.Nombre} - Apodo: {personaje.Apodo} - Edad: {personaje.Edad}");
            Console.WriteLine($"     Salud: {personaje.Salud} - Fuerza: {personaje.Fuerza}");
            Console.WriteLine("------------------------------");
            indice++;
        }
        Console.WriteLine("Opcion: ");
        opcionIngresada = Console.ReadLine();
    } while (!(int.TryParse(opcionIngresada, out opcion)) && !(opcion >= 0 && opcion < ListaDePersonajes.Count));
    
    return ListaDePersonajes[opcion];
    }


    public void FinDelDuelo()
    {
            if(Jugador1.EstaVivo()){
                Console.WriteLine("Gano Jugador1");
                Jugador1.RegenerarVida();
            }else{
                Console.WriteLine("Gano Jugador2");
                Jugador2.RegenerarVida();
            }
    }

        public void IniciarJuego(){
            int opcion=0;
            string opcionIngresada;
            do{ 
            do{
                //Console.Clear();
                MostrarMenu(["1)- 1vs1","2)- 1vsCpu","3)- historial","4)- Salir"]);
                 Console.WriteLine("Opcion: ");
                 opcionIngresada = Console.ReadLine();
             } while (!(int.TryParse(opcionIngresada, out opcion)) && !(opcion >= 1 && opcion <= 4));
             switch(opcion){
                case 1:
                   Partida1vs1();
                break;
                case 2:
                 Partida1vsCPU();
                 break;
                 case 3:
                 Console.WriteLine("Opcion 3 ");
                 break;
                 case 4:
                 Console.WriteLine("Opcion 4 ");
                 break;
                 default:
                 break;
             }
            }while(opcion!=4);
        }

        private void Partida1vs1(){
            ElegirPersonaje(2);
            Duelo();
        }


        private void Partida1vsCPU(){
            ElegirPersonaje(1);
            List<Personaje> listaPersonajeDuelo=ListaDePersonajes;
            while(listaPersonajeDuelo.Count()>0 && Jugador1.EstaVivo()){
                Jugador2=listaPersonajeDuelo[new Random().Next(0,listaPersonajeDuelo.Count()-1)];
                Duelo();
                listaPersonajeDuelo.Remove(Jugador2);
            }
            
        }

        private void TurnoJugador(Personaje atacante, Personaje defensor)
        {
            Evento eventoRandom = GenerarEvento();
            Console.WriteLine($"{eventoRandom.Descripcion}");
            int danio = atacante.Atacar(eventoRandom);
            defensor.Defender(danio);
        }

        private Evento GenerarEvento()
        {
            return ListaDeEventos[new Random().Next(0, ListaDeEventos.Count() - 1)];
        }

        private void BebidaEnergetica(Personaje Jugador){
            Jugador.RegenerarVida();
        }

public void Duelo()
{
    int turno = RetornarTurno();
    int cantBebida = 1;
    int opcion=0;
    string opcionIngresada;
    Console.WriteLine($"Comienza el Jugador {turno}");
    Console.ReadKey(); // Espera a que el usuario presione cualquier tecla.
    Console.WriteLine("Presiona cualquier tecla para continuar...");

    while (Jugador1.EstaVivo() && Jugador2.EstaVivo())
    {
        switch (turno)
        {
            case 1:
                // Turno del Jugador 1
                Console.WriteLine("\n---------------------------------------");
                    Console.WriteLine($"\nSalud de {Jugador1.Nombre} es: {Jugador1.Salud}");
                    Console.WriteLine($"\nSalud de {Jugador2.Nombre} es: {Jugador2.Salud}");
                Console.WriteLine($"\nAtaca {Jugador1.Nombre}");

                if (cantBebida > 0)
                {
                    do
                    {
                        Console.WriteLine("Desea Atacar (1) o tomar una bebida energetica para recuperar su salud (2)?");
                        Console.WriteLine("Opcion: ");
                        opcionIngresada = Console.ReadLine();
                    } while (!int.TryParse(opcionIngresada, out opcion) || (opcion != 1 && opcion != 2));

                    if (opcion == 2 && cantBebida > 0)
                    {
                        Console.WriteLine($"\nBebida Energetica Disponible: {cantBebida}");
                        BebidaEnergetica(Jugador1);
                        cantBebida--;
                        Console.WriteLine($"\nSalud de {Jugador1.Nombre} es: {Jugador1.Salud}");
                    }
                }
                
                
                if (opcion == 1 || cantBebida <= 0)
                {
                    Console.WriteLine($"\nAtaca {Jugador1.Nombre}");
                    Console.WriteLine($"\nDefiende {Jugador2.Nombre}");
                    TurnoJugador(Jugador1, Jugador2);
                }
                
                turno = 2;
                Console.ReadKey(); // Espera a que el usuario presione cualquier tecla.
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                break;
            case 2:
                // Turno del Jugador 2
              Console.WriteLine("\n---------------------------------------");
                    Console.WriteLine($"\nSalud de {Jugador2.Nombre} es: {Jugador2.Salud}");
                    Console.WriteLine($"\nSalud de {Jugador1.Nombre} es: {Jugador1.Salud}");
                Console.WriteLine($"\nAtaca {Jugador2.Nombre}");

                if (cantBebida > 0)
                {
                    do
                    {
                        Console.WriteLine("Desea Atacar (1) o tomar una bebida energetica para recuperar su salud (2)?");
                        Console.WriteLine("Opcion: ");
                        opcionIngresada = Console.ReadLine();
                    } while (!int.TryParse(opcionIngresada, out opcion) || (opcion != 1 && opcion != 2));

                    if (opcion == 2 && cantBebida > 0)
                    {
                        Console.WriteLine($"\nBebida Energetica Disponible: {cantBebida}");
                        BebidaEnergetica(Jugador2);
                        cantBebida--;
                        Console.WriteLine($"\nSalud de {Jugador2.Nombre} es: {Jugador2.Salud}");
                    }
                }
                
                if (opcion == 1 || cantBebida <= 0)
                {
                    Console.WriteLine($"\nAtaca {Jugador2.Nombre}");
                    Console.WriteLine($"\nDefiende {Jugador1.Nombre}");
                    TurnoJugador(Jugador2, Jugador1);
                }
                
                Console.ReadKey(); // Espera a que el usuario presione cualquier tecla.
                Console.WriteLine("Presiona cualquier tecla para continuar...");
                turno = 1;
                break;
            default:
                break;
        }
    
            //mostrar Ganador-------------------
            FinDelDuelo();


        }



    }

    }
}
//Camnbiar el 0 que sale del programa
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

    private void EsperarTecla()
    {
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey(); // Espera a que el usuario presione cualquier tecla.
    }


   private void ElegirPersonaje(int cantidadJugadores)
{
    Jugador1 = SeleccionarPersonaje(1, null);
    Console.WriteLine($"Usted selecciono el personaje: {Jugador1.Apodo}");
    Console.WriteLine("-------------------------------------");
    EsperarTecla();

    if (cantidadJugadores == 2)
    {
        Jugador2 = SeleccionarPersonaje(2, Jugador1);
        Console.WriteLine($"Usted selecciono el personaje: {Jugador2.Apodo}");
        Console.WriteLine("-------------------------------------");
        EsperarTecla();
    }
}

private Personaje SeleccionarPersonaje(int numeroJugador, Personaje personajeNoPermitido)
{
    int opcion = 0;
    string opcionIngresada;
    bool opcionValida;
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

        opcionValida = int.TryParse(opcionIngresada, out opcion) && (opcion >= 0 && opcion < ListaDePersonajes.Count);

        if (opcionValida && personajeNoPermitido != null && ListaDePersonajes[opcion] == personajeNoPermitido)
        {
            Console.WriteLine("El personaje ya fue seleccionado por el Jugador 1. Elija otro personaje.");
            opcionValida = false;
        }
    } while (!opcionValida);

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
                MostrarMenu(["1)- 1vsCpu","2)- 1vs1","3)- historial","4)- Salir"]);
                 Console.WriteLine("Opcion: ");
                 opcionIngresada = Console.ReadLine();
             } while (!(int.TryParse(opcionIngresada, out opcion)) && !(opcion >= 1 && opcion <= 4));
             switch(opcion){
                case 1:
                   Partida1vsCPU(opcion);
                break;
                case 2:
                   Partida1vs1(opcion);
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

        private void Partida1vs1(int cantJugadores){
            ElegirPersonaje(2);
            Duelo(cantJugadores);
        }


        private void Partida1vsCPU(int cantJugadores){
            ElegirPersonaje(1);
            List<Personaje> listaPersonajeDuelo=ListaDePersonajes;
            while(listaPersonajeDuelo.Count()>0 && Jugador1.EstaVivo()){
                Jugador2=listaPersonajeDuelo[new Random().Next(0,listaPersonajeDuelo.Count()-1)];
                Duelo(cantJugadores);
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

        private void Estadisticas (Personaje Jugador){
            Console.WriteLine($"\nSalud de {Jugador.Nombre} es: {Jugador.Salud}");
        }

    public void Duelo(int cantJugadores)
    {
        int turno = RetornarTurno();
        int cantBebida = 1;
        int opcion = 0;
        string opcionIngresada;
    
        Console.WriteLine($"Comienza el Jugador {turno}");
    
        while (Jugador1.EstaVivo() && Jugador2.EstaVivo())
        {
            if (turno == 1)
            {
                ProcesarTurno(Jugador1, Jugador2, cantBebida, turno);
            }
            else
            {
                ProcesarTurno(Jugador2, Jugador1, cantBebida, turno, cantJugadores);
            }
        }
    
        // Mostrar Ganador
        FinDelDuelo();
    }
    
    private void ProcesarTurno(Personaje atacante, Personaje defensor, int cantBebida, int turno, int cantJugadores = 2)
    {
        Console.WriteLine("\n---------------------------------------");
        Estadisticas(atacante);
        Estadisticas(defensor);
        Console.WriteLine($"\nAtaca {atacante.Nombre}");
    
        if (cantJugadores == 2 || atacante == Jugador1)
        {
            RealizarAccionJugador(atacante, cantBebida);
        }
        else
        {
            RealizarAccionAutomatica(atacante, cantBebida);
        }
    
        Console.WriteLine($"\nAtaca {atacante.Nombre}");
        Console.WriteLine($"\nDefiende {defensor.Nombre}");
        TurnoJugador(atacante, defensor);
    
        turno = (turno == 1) ? 2 : 1;
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey(); // Espera a que el usuario presione cualquier tecla.
    }
    
    private void RealizarAccionJugador(Personaje jugador, int cantBebida)
    {
        int opcion;
        string opcionIngresada;
    
        if (cantBebida > 0)
        {
            do
            {
                Console.WriteLine("Desea Atacar (1) o tomar una bebida energetica para recuperar su salud (2)?");
                Console.WriteLine("Opcion: ");
                opcionIngresada = Console.ReadLine();
            } while (!int.TryParse(opcionIngresada, out opcion) || (opcion != 1 && opcion != 2));
    
            if (opcion == 2)
            {
                Console.WriteLine($"\nBebida Energetica Disponible: {cantBebida}");
                BebidaEnergetica(jugador);
                cantBebida--;
                Estadisticas(jugador);
            }
        }
}

private void RealizarAccionAutomatica(Personaje jugador,  int cantBebida)
{
    Random random = new Random();
    int valorAleatorio = random.Next(1, 3); // El límite superior es exclusivo, por lo que se usa 3 para obtener valores entre 1 y 2

    if (valorAleatorio == 2 && cantBebida > 0)
    {
        Console.WriteLine($"\nBebida Energetica Disponible: {cantBebida}");
        Console.WriteLine($"\nEl jugador {jugador.Apodo} toma una bebida Energetica.");
        BebidaEnergetica(jugador);
        cantBebida--;
        Estadisticas(jugador);
    }
}

    }
}
//Camnbiar el 0 que sale del programa
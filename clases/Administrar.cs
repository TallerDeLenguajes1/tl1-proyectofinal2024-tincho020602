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


private Personaje SeleccionarPersonaje()
{
    int opcion = 0;
    string opcionIngresada;
    bool opcionValida;
    Personaje elegido=new Personaje();
    do
    {
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

        if (opcionValida)
        {
          elegido = ListaDePersonajes[opcion];
          ListaDePersonajes.Remove(elegido);

        }
    } while (!opcionValida);
    Console.Clear();
    return elegido;
}


    public void FinDelDuelo()
    {
            Console.Clear();
            if(Jugador1.EstaVivo()){
                Console.WriteLine("Gano Jugador1");
                //Jugador1.RegenerarVida();
            }else{
                Console.WriteLine("Gano Jugador2");
                //Jugador2.RegenerarVida();
            }
            Console.ReadKey();
    }

        public void IniciarJuego(){
            int opcion=0;
            string opcionIngresada;
            do{ 
            do{
                MostrarMenu(["1- 1vsCpu","2- 1vs1","3- historial","4- Salir"]);
                 Console.WriteLine("Opcion: ");
                 opcionIngresada = Console.ReadLine();
                 Console.Clear();
             } while (!(int.TryParse(opcionIngresada, out opcion)) && !(opcion >= 1 && opcion <= 4));             
             switch(opcion){
                case 1:
                   //Partida1vsCPU(opcion);
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
            }while(opcion!=4);
        }

        private void Partida1vs1(){
            Console.WriteLine($"Elegir personaje para el Jugador1: ");
            Jugador1=SeleccionarPersonaje();
            Console.WriteLine($"Elegir personaje para el Jugador2: ");
            Jugador2=SeleccionarPersonaje();
            Duelo();
        }


        private void Partida1vsCPU(){
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
            Console.WriteLine($"Ocurrio un evento: {eventoRandom.Nombre}\n");
            Console.WriteLine($"`{eventoRandom.Descripcion}`\n");
            if (eventoRandom.Efecto>0)
            {
                Console.WriteLine($"El ataque se incrementa: {eventoRandom.Efecto}\n");
            }else
            {
                Console.WriteLine($"El ataque se reduce: {eventoRandom.Efecto}\n");
                
            }
            int danio = atacante.Atacar(eventoRandom);
            defensor.Defender(danio);
        }

        private Evento GenerarEvento()
        {
            return ListaDeEventos[new Random().Next(0, ListaDeEventos.Count() - 1)];
        }


        private void MostrarSalud(){
            Console.WriteLine($"\nSalud: {Jugador1.Nombre}: {Jugador1.Salud} - {Jugador2.Nombre}: {Jugador2.Salud}\n");
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
                Console.WriteLine($">>Ataca {Jugador1.Nombre} - Fuerza de ataque: {Jugador1.Fuerza}");
                TurnoJugador(Jugador1,Jugador2);
                turno=2;
                Console.ReadKey();
            }
            else
            {
                MostrarSalud();
                Console.WriteLine($">>Ataca {Jugador2.Nombre} - Fuerza de ataque: {Jugador2.Fuerza}");
                TurnoJugador(Jugador2,Jugador1);
                turno=1;
                Console.ReadKey();
            }
        }
        // Mostrar Ganador
        FinDelDuelo();
    }

    
    
    }
}
//Detalles del daño, se incrementa
//Cambiar json de eventos por eventos que solo influyan en ataque y no en salud
//Cuando se elige de nuevo en el 1v1, quedan eliminado los jugadores que ya se seleccionaron
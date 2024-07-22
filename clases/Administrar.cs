using EspacioPersonajes;
using EventoAleatorio;

namespace EspacioJuego
{ 
class Juego
{
    //Atributos
    private List<Personaje> ListaDePersonajes {get;set;}
    private Personaje Jugador1 {get;set;}

    private Personaje Jugador2 {get;set;}

    private int Turno {get;set;}
    
    public void MostrarMenu(string[] opciones)
    {
        foreach(var opcion in opciones)
        {
            Console.WriteLine(opcion);
        }
    }
    public 

}
    
}

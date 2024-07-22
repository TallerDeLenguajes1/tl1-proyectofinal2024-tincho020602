using EventoAleatorio;

namespace EspacioPersonajes
{

     public class Personaje
    {
        public string Nombre { get; set; }
        public string Apodo { get; set; }
        public int Edad { get; set; }
        public int Salud { get; set; }
        public int Fuerza { get; set; }

        // Constructor
        public Personaje(string nombre, string apodo, int edad, int salud, int fuerza)
        {
            Nombre = nombre;
            Apodo = apodo;
            Edad = edad;
            Salud = salud;
            Fuerza = fuerza;

        }

       public void RegenerarVida(){
        Salud=100;
        }
        public void Defender(int danio)
        {
            Salud-=danio;
            if(Salud<0)
            {
                Salud=0;
            }
        }

        public int Atacar(Evento eventoAleatorio)
        {
            return (Fuerza + eventoAleatorio.Efecto);
        }

        //-----------
         public void aumentarNivel(int masFuerza, int masSalud){
        Fuerza+=masFuerza;
        Salud+=masSalud;
    }

    public bool EstaVivo(){
        if(Salud>0){
            return true;
        }else{
            return false;
        }
    }


    }
}
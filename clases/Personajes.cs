using EventoAleatorio;

namespace EspacioPersonajes
{

     class Personaje
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


    }
}
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
        public int VelocidadDeAtaque { get; set; }

        //public bool EsCPU { get; set; }
        //public int CantBebidas { get; set; }

        // Constructor
        public Personaje() { }

        public Personaje(string nombre, string apodo, int edad, int salud, int fuerza, int velocidad)
        {
            Nombre = nombre;
            Apodo = apodo;
            Edad = edad;
            Salud = salud;
            Fuerza = fuerza;
            VelocidadDeAtaque = velocidad;
            //EsCPU = cpu;
            //CantBebidas= 1;
        }

        public void RestaurarVida()
        {
            Salud = 100;
        }

        public void Defender(int danio)
        {
            Salud -= danio;
            if (Salud < 0)
            {
                Salud = 0;
            }
        }

        public int Atacar(Evento eventoAleatorio)
        {
            int danio = (Fuerza + eventoAleatorio.Efecto + VelocidadDeAtaque);
            if (danio < 0)
            {
                danio = 0;
            }
            return danio;
        }

        public bool EstaVivo()
        {
            if (Salud > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

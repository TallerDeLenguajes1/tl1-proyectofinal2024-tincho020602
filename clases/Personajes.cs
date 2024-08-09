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

        // Constructor
        public Personaje() { }

        public Personaje(
            string nombre,
            string apodo,
            int edad,
            int salud,
            int fuerza,
            int velocidad
        )
        {
            Nombre = nombre;
            Apodo = apodo;
            Edad = edad;
            Salud = salud;
            Fuerza = fuerza;
            VelocidadDeAtaque = velocidad;
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
                //Esto hago p/que la salud no quede con un valor negativo
                Salud = 0;
            }
        }

        public int Atacar(Evento eventoAleatorio)
        {
             int efecto = (eventoAleatorio != null) ? eventoAleatorio.Efecto : 0;
            int danio = (Fuerza + efecto + VelocidadDeAtaque);
            if (danio < 0)
            {
                //Esto hago p/que el daño no quede con un valor negativo
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

namespace EspacioPersonajes
{

    public class Personaje
    {
        public string Nombre { get; set; }
        public string Apodo { get; set; }
        public int Edad { get; set; }
        public int Salud { get; set; }
        public double Fuerza { get; set; }

        // Constructor
        public Personaje(string nombre, string apodo, int edad, int salud, double fuerza)
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

        public void Atacar(Personaje defensor)
        {
         
        }


    }
}
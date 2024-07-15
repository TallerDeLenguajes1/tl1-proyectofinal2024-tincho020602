using System;
using System.Text.Json.Serialization;

namespace EspacioPersonajes
{
    // Clase Personaje
    public class Simpsons
    {
        private Caracteristicas caracteristicas;
        private Datos datos;

        public Simpsons(int velocidad, int destreza, int fuerza, int armadura, int nivel, string nombre, int salud, string apodo, int edad)
        {
            this.Caracteristicas1 = new Caracteristicas(velocidad, destreza, fuerza, nivel, armadura, salud);
            this.Datos1 = new Datos(nombre, apodo, edad);
        }

        public Simpsons()
        {
            //Para deserializar
        }


        public Caracteristicas Caracteristicas1 { get => caracteristicas; set => caracteristicas = value; }
        public Datos Datos1 { get => datos; set => datos = value; }

        public class Caracteristicas
        {

            private int velocidad;
            private int destreza;
            private int fuerza;
            private int armadura;
            private int salud;

            [JsonPropertyName("Velocidad")]
            public int Velocidad { get => velocidad; }

            [JsonPropertyName("Destreza")]
            public int Destreza { get => destreza; }

            [JsonPropertyName("Fuerza")]
            public int Fuerza { get => fuerza; set => fuerza = value; }

            [JsonPropertyName("Armadura")]
            public int Armadura { get => armadura; }

            [JsonPropertyName("Salud")]
            public int Salud { get => salud; set => salud = value; }

            public Caracteristicas(int Velocidad, int Destreza, int Fuerza, int Armadura)
            {
                Random numeroRandom = new Random();

                this.velocidad = Velocidad;
                this.destreza = Destreza;
                this.fuerza = Fuerza;
                this.armadura = Armadura;
                this.salud = 100;
            }
            //___________________________
            public class Datos
            {
                private string nombre;

                private string apodo;

                private int edad;

                [JsonPropertyName("Nombre")]
                public string nom { get => nombre; }

                [JsonPropertyName("Apodo")]
                public string Apodo { get => apodo; }

                [JsonPropertyName("Edad")]
                public int Edad { get => edad; }



                public Datos(string Nombre, string Apodo, int Edad)
                {
                    this.nombre = Nombre;
                    this.apodo = Apodo;
                    this.edad = Edad;
                }
            }

        }
    }
}


    //_________________________________________
/*
public class Simpsons
{
    public string Nombre { get; private set; }
    public string Apodo { get; private set; }
    public string Tipo { get; private set; }
    public int Velocidad { get; private set; }
    public int Destreza { get; private set; }
    public int Fuerza { get; private set; }
    public int Nivel { get; private set; }
    public int Armadura { get; private set; }
    public int Salud { get; private set; }

    // Constructor
    public Simpsons(string nombre, string apodo, string tipo, int velocidad, int destreza, int fuerza, int nivel, int armadura, int salud)
    {
        Nombre = nombre;
        Apodo = apodo;
        Tipo = tipo;
        Velocidad = velocidad;
        Destreza = destreza;
        Fuerza = fuerza;
        Nivel = nivel;
        Armadura = armadura;
        Salud = salud;
    }

    private void ReducirSalud(int danioProvocado)
    {
        if (danioProvocado > 0)
        {
            Salud -= danioProvocado;
            if (Salud < 0) Salud = 0; // Asegurarse de que la salud no sea negativa
            Console.WriteLine($"{Nombre} recibe {danioProvocado} de daño. Salud actual: {Salud}");
        }
        else
        {
            Console.WriteLine($"{Nombre} no recibe daño.");
        }
    }

    public void Atacar(Simpsons defensor)
    {
        int ataque = Destreza * Fuerza * Nivel;
        int efectividad = new Random().Next(1, 101);
        int defensa = defensor.Armadura * defensor.Velocidad;
        int constanteDeAjuste = 500;
        int danioProvocado = ((ataque * efectividad) / constanteDeAjuste) - defensa;
        if (danioProvocado < 0) danioProvocado = 0; // Asegurarse de que el daño no sea negativo
        defensor.ReducirSalud(danioProvocado);
    }
     public void ModificarSalud(int cantidad)
    {
        Salud += cantidad;
        if (Salud < 0) Salud = 0; // Asegurarse de que la salud no sea negativa
    }

     public void Mejorar()
    {
        Salud += 10;
        Armadura += 5;
        Console.WriteLine($"{Nombre} ha mejorado! Salud: {Salud}, Armadura: {Armadura}");
    }

    public void UsarHabilidadEspecial()
    {
        Console.WriteLine($"{Nombre} usa su habilidad especial.");
    }
}
}
*/
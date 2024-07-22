namespace EventoAleatorio
{
    public class Evento
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int Efecto { get; set; } //Valor + o -


        //Constructor
        public Evento(string nombre, string descripcion, int efecto)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Efecto = efecto;
        }
    }

}
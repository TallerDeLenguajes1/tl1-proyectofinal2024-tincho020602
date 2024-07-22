using EspacioJuego;

Juego miJuego = new Juego();
miJuego.Duelo1vs1();

/*
int opcion = 0;
do
{


    Console.WriteLine("Elija una opcion del Menu: ");
    miJuego.MostrarMenu(["1) Jugar 1 VS 1.", "2) Jugar 1 VS CPU.", "3) Historial", "4) Salir."]);
    Console.WriteLine("Opcion: ");
    string opcionIngresada = Console.ReadLine();
    if (int.TryParse(opcionIngresada, out opcion) && (opcion >= 1 && opcion <= 4))
    {

        Console.WriteLine($"Su opcion: {opcion}");
        break;
    }
    else
    {
        Console.WriteLine("Opcion incorrecta,ingrese nuevamente una opcion ");
    }

} while (opcion < 1 || opcion > 4);

switch (opcion)
{
    case 1:
        Console.WriteLine($"Su opcion: {opcion}");

        break;
    case 2:
        break;
    case 3:
        break;
    case 4:
        break;

    default:
        break;
}*/
# tl1-proyectofinal2024-tincho020602
"La Batalla de Springfield"
Historia:
El alcalde de Springfield ha organizado una competencia épica para decidir quién será el nuevo líder de la ciudad. Los personajes más icónicos de Springfield se enfrentarán en una serie de combates para demostrar quién es el más fuerte, astuto y habilidoso. ¡Solo uno será coronado como el campeón definitivo de Springfield!

*Cómo jugar:
Para jugar, clona el repositorio del juego, asegúrate de tener .NET instalado y ejecuta el comando dotnet run en la terminal.

*Modos de juego:

1vs1:
En este modo, dos jugadores se enfrentan en un duelo. Cada uno elige un personaje único (no se puede seleccionar el mismo personaje) y se preparan para la batalla. Durante el combate, pueden ocurrir eventos aleatorios que afectan la fuerza del ataque del jugador que está atacando en ese momento. El juego termina cuando uno de los personajes alcanza una Salud de 0 o menos.

1vsCPU:
En este modo, un solo jugador se enfrenta a una serie de oponentes controlados por la CPU. El jugador selecciona su personaje y comienza la batalla contra un oponente aleatorio. El objetivo es derrotar a todos los personajes del juego, enfrentándolos uno por uno, hasta convertirse en el líder de Springfield. Las batallas continúan hasta que el jugador haya derrotado a todos sus rivales o hasta que su personaje pierda toda su salud.

*El juego consume una API para obtener insultos. La API utilizada es:
URL de la API: https://evilinsult.com/generate_insult.php?lang=es&type=json
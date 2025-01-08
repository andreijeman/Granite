using Granite.Components;
using Granite.Utilities;

Console.Clear();

int sizeX = 10;
int sizeY = 10;
Cell[,] model = new Cell[sizeY, sizeX];

Console.Write(' ');
for (int i = 0; i < sizeX; i++) Console.Write(i);
Console.WriteLine();
for (int i = 0; i < sizeY; i++) Console.WriteLine(i);

model.SculptChessBoard(Vector2.New(0, 0), Vector2.New(sizeX, sizeY), ConsoleColor.Black, ConsoleColor.Black);
model.SculptBorder(Vector2.New(0, 0), Vector2.New(sizeX, sizeY), Cell.New(ConsoleColor.Magenta, ConsoleColor.Black), Assets.Border1);
model.SculptText(Vector2.New(1, 1), Vector2.New(sizeX - 2, sizeY - 2), Cell.New(ConsoleColor.Magenta, ConsoleColor.Black), 
    "GRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE");

Terminal.PrintModelPart(model, Rect.New(Vector2.New(3, 5), Vector2.New(3, 4)), Vector2.New(1, 1));

Console.ReadLine();
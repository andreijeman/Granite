using Granite.Components;
using Granite.Utilities;

Console.Clear();
Console.WriteLine(Console.BufferWidth + "/" + Console.BufferHeight);

Model model = new Model(Vector2.New(9, 10));
model.SculptBorder(Model.Cell.New(ConsoleColor.Magenta, ConsoleColor.Black), Assets.Border1);
model.SculptText(Model.Cell.New(ConsoleColor.Magenta, ConsoleColor.Black), 
    "GRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE\nGRANITE");

Console.ReadLine();
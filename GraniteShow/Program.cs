using Granite.Components;
using Granite.Helpers;
using Granite.Utilities;

Vector2 size = new Vector2(10, 10);

Model model = new Model(size);
model.SculptBorder(new Vector2(0, 0), new Vector2(10, 10), new Model.Cell(' ', ConsoleColor.Magenta, ConsoleColor.Black), Assets.Border1);

for (int y = 0; y < size.Y; y++)
{
    for (int x = 0; x < size.X; x++)
    {
        Console.BackgroundColor = model.Map[y, x].BackgroundColor;
        Console.Write(model.Map[y, x].Character);
    }
    Console.WriteLine();
}
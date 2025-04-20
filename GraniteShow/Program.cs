using Granite.Graphics.Components;
using Granite.Graphics.Frames;
using Granite.Graphics.Objects;
using Granite.Graphics.Utilities;
using Granite.IO;

Console.CursorVisible = false;

Frame frame = new Frame
{
    Model = new Model(64, 32).Init().DrawChessboard(new Color("808080"), new Color("E0E0E0"))
};

GObject obj = new GObject
{
    Model = new Model(4, 2).Init().DrawChessboard(new Color("FF3399"), new Color("FF66B2")),
    Left = 2,
    Top = 2,
};

var rnd = new Random();

int n = 20;
for(int i = 0; i < n; i++)
{
    frame.Add(new GObject
    {
        Model = new Model(rnd.Next(16), rnd.Next(8))
            .Init()
            .Fill(new Color($"{rnd.Next(111111, 999999)}")),
        Left = rnd.Next(frame.Width),
        Top = rnd.Next(frame.Height),
    });

}

ConsoleOutput.Add(frame);
frame.Add(obj);
frame.Draw();

    
var target = frame;

while (true)
{
      var key = Console.ReadKey(true).Key;

    switch(key)
    {
        case ConsoleKey.UpArrow:
            target.Top--;
            break;
        case ConsoleKey.DownArrow:
            target.Top++;
            break;
        case ConsoleKey.LeftArrow:
            target.Left--;
            break;
        case ConsoleKey.RightArrow:
            target.Left++;
            break;
        case ConsoleKey.Q:
            target.Model = new Model(target.Width + 1, target.Height).Init().DrawChessboard(new Color("FF3399"), new Color("FF66B2"));
            break;
        case ConsoleKey.W:
            target.Model = new Model(target.Width - 1, target.Height).Init().DrawChessboard(new Color("FF3399"), new Color("FF66B2"));
            break;
        case ConsoleKey.A:
            frame.BringForward(target);
            break;
        case ConsoleKey.S:
            frame.SendBackward(target);
            break;
        default: break;
    }
}
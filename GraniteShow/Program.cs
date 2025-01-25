using Granite.Graphics;
using Granite.Utilities;

Frame mainFrame = new Frame()
{
    Width = Console.BufferWidth,
    Height = Console.BufferHeight,
};

mainFrame.ModelChangedEvent += Output.OnModelChangedEvent;

List<Object2D> list = new List<Object2D>();
int n = 10;
var rnd = new Random();

for (int i = 0; i < n; i++)
{
    var obj = new MyObject()
    {
      Left = rnd.Next(mainFrame.Width),
      Top = rnd.Next(mainFrame.Height),
      Width = rnd.Next(mainFrame.Width / 4),
      Height = rnd.Next(mainFrame.Height / 4),
      Color = new Cell.RgbColor()
        {
          R = rnd.Next(255), 
          G = rnd.Next(255), 
          B = rnd.Next(255)
        }
    };

    list.Add(obj);
    mainFrame.AddFront(obj);
}


Console.Clear();
Console.CursorVisible = false;
mainFrame.InvokeEntireModelChangedEvent();
while (true)
{
    var key = Console.ReadKey(true).Key;
    switch (key)
    {
        case ConsoleKey.LeftArrow: list[0].Left--; break;
        case ConsoleKey.RightArrow: list[0].Left++; break;
        case ConsoleKey.UpArrow: list[0].Top--; break;
        case ConsoleKey.DownArrow: list[0].Top++; break;
        case ConsoleKey.Q: list[0].Height++; break;
        case ConsoleKey.W: list[0].Height--; break;
        case ConsoleKey.A: mainFrame.BringForward(list[0]); break;
        case ConsoleKey.S: mainFrame.SendBackward(list[0]); break;
        case ConsoleKey.Enter:
            foreach (var obj in list)
            {
                obj.Left = rnd.Next(mainFrame.Width);
                obj.Top = rnd.Next(mainFrame.Height);
                obj.Width = rnd.Next(mainFrame.Width / 4);
                obj.Height = rnd.Next(mainFrame.Height / 4);
            }

            break;
        default: break;
    }
}


public class MyObject : Object2D
{
    private Cell.RgbColor _color;

    public Cell.RgbColor Color
    {
        get => _color;
        set
        {
            _color = value;
            SculptModel();
        }
    }

    public override void SculptModel()
    {
        this.Model.SculptRectangle(
            new RectMath.Rect()
            {
                X1 = 0, 
                Y1 = 0, 
                X2 = this.Width - 1, 
                Y2 = this.Height - 1
            },
            new Cell()
            {
                BackgroundRgbColor = Color,
                ForegroundRgbColor = Color,
                Character = ' '
            });
    }
}
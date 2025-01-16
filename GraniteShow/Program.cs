using Granite.Graphics;

MyObject myObject = new MyObject()
{
    Width = 4, 
    Height = 2
};

Frame frame = new Frame()
{
    Width = 40,
    Height = 20,
};

frame.ModelChangedEvent += Output.OnModelChangedEvent;

frame.AddObject(myObject);

Console.Clear();
Console.CursorVisible = false;

frame.InvokeEntireModelChangedEvent();

Object2D obj = myObject;
while (true)
{
    var key = Console.ReadKey(true).Key;
    switch (key)
    {
        case ConsoleKey.LeftArrow: obj.Left--; break;
        case ConsoleKey.RightArrow: obj.Left++; break;
        case ConsoleKey.UpArrow: obj.Top--; break;
        case ConsoleKey.DownArrow: obj.Top++; break;
        default: break;
    }
}

public class MyObject : Object2D
{
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
                BackgroundRgbColor = new Cell.RgbColor() {R = 55, G = 155, B = 255},
                ForegroundRgbColor = new Cell.RgbColor() {R = 55, G = 155, B = 255},
                Character = 'x'
            });
    }
}
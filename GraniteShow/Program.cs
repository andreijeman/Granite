﻿using Granite.Graphics;
using Granite.Utilities;

Frame mainFrame = new Frame()
{
    Width = Console.BufferWidth,
    Height = Console.BufferHeight,
};

mainFrame.ModelChangedEvent += Output.OnModelChangedEvent;

Frame frame = new Frame()
{
    Width = 64,
    Height = 24,
    Left = 10,
    Top = 10,
};

mainFrame.AddFront(frame);

MyObject myObject = new MyObject()
{
    Width = 4,
    Height = 2,
    Color = new Cell.RgbColor()
    {
        R = 200, 
        G = 24, 
        B = 125
    }
};

frame.AddFront(myObject);

var rnd = new Random();

for (int i = 0; i < 10; i++)
{
    frame.AddFront(
        new MyObject()
        {
            Left = rnd.Next(frame.Width),
            Top = rnd.Next(frame.Height),
            Width = rnd.Next(frame.Width / 4),
            Height = rnd.Next(frame.Height / 4),
            Color = new Cell.RgbColor()
            {
                R = rnd.Next(255), 
                G = rnd.Next(255), 
                B = rnd.Next(255)
            }
        });
}

Console.Clear();
Console.CursorVisible = false;

mainFrame.InvokeEntireModelChangedEvent();

Object2D obj = frame;
while (true)
{
    var key = Console.ReadKey(true).Key;
    switch (key)
    {
        case ConsoleKey.LeftArrow: obj.Left--; break;
        case ConsoleKey.RightArrow: obj.Left++; break;
        case ConsoleKey.UpArrow: obj.Top--; break;
        case ConsoleKey.DownArrow: obj.Top++; break;
        case ConsoleKey.Q: obj.Height++; break;
        case ConsoleKey.W: obj.Height--; break;
        case ConsoleKey.A: mainFrame.BringForward(obj); break;
        case ConsoleKey.S: mainFrame.SendBackward(obj); break;
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
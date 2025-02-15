using Granite.Controls;
using Granite.Entities;
using Granite.Graphics;

InteractiveFrame c = new InteractiveFrame { Width = 50, Height = 50 };

Button b1 = new Button { Width = 6, Height = 2, Left = 3, Top = 3 };
b1.PressedEvent += () => Console.WriteLine("button 1");
Button b2 = new Button { Width = 6, Height = 2, Left = 15, Top = 3 };
b2.PressedEvent += () => Console.WriteLine("button 2");

c.AddFront(b1);
c.AddFront(b2);
c.Bind();
c.ModelChangedEvent += Output.OnModelChangedEvent;
c.InvokeEntireModelChangedEvent();

ConsoleKeyListener.Start();


Thread.Sleep(100000);





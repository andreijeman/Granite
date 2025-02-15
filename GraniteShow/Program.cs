using Granite.Entities;
using Granite.Graphics;

Controller mainController = new Controller();
mainController.BindToConsoleKeyListener();

Frame mainFrame = new Frame() { Width = 50, Height = 20 };
mainFrame.ModelChangedEvent += Output.OnModelChangedEvent;

ConsoleKeyListener.Start();

//---------------------------------------------------------------------------------------------------

Button btn1 = new Button { Width = 6, Height = 2, Left = 3, Top = 3 };
btn1.PressedEvent += () => Console.WriteLine("button 1");

Button btn2 = new Button { Width = 2, Height = 1, Left = 15, Top = 3, Text = "Button" };
btn2.PressedEvent += () => Console.WriteLine("button 2");

//---------------------------------------------------------------------------------------------------

Scroll scrl = new Scroll { Width = 10, Height = 5, Left = 20, Top = 3 };
for(int i = 0; i < 100; i++)
{
    Label l = new Label { Width = 8, Height = 2, Text = $"Label-{i}" };
    scrl.AddDown(l);
}


//---------------------------------------------------------------------------------------------------

mainController.AddFront(btn1);
mainController.AddBack(btn2);
mainController.AddFront(scrl);

mainFrame.AddFront(btn1);
mainFrame.AddFront(btn2);
mainFrame.AddFront(scrl);

mainFrame.InvokeEntireModelChangedEvent();

Thread.Sleep(100000);




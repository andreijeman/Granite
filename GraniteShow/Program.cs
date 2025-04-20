using Granite.Controls.Holders;
using Granite.Graphics.Components;
using Granite.Graphics.Frames;
using Granite.Graphics.Utilities;
using Granite.IO;
using Granite.UI.Entities;
using Granite.UI.Entities.Args;

KeyboardInput.Start();

var ctrlHolder = new ControllerHolder();
KeyboardInput.KeyPressed += ctrlHolder.OnKeyPressed; 

var frame = new Frame()
{
    Model = new Model(40, 20).Init().DrawChessboard(new Color("404040"), new Color("C0C0C0"))
};
ConsoleOutput.Add(frame);


var btn = new Button(new ButtonArgs
{
    Left = 2,
    Top = 2,
    Width = 4,
    Height = 4,
    Color = new Color("FF33FF"),
    FocusedColor = new Color("0080FF"),
    Text = "This is a button",
    keyActionDict = { { ConsoleKey.Enter, () => Console.WriteLine("Entered!") } }
});

var btn2 = new Button(new ButtonArgs
{
    Left = 7,
    Top = 2,
    Width = 4,
    Height = 4,
    Color = new Color("FF33FF"),
    FocusedColor = new Color("0080FF"),
    Text = "This is a button",
    keyActionDict = { { ConsoleKey.Enter, () => Console.WriteLine("Entered!") } }
});

var label = new Label(new LabelArgs
{
    Width = 20,
    Height = 1,
    Left = 1,
    Color = new Color("CCCC00"),
    Text = "This is a label",
});

frame.Add(btn.GObject);
frame.Add(btn2.GObject);
frame.Add(label);

ctrlHolder.Add(btn.Controller);
ctrlHolder.Add(btn2.Controller);
frame.Draw();

await Task.Delay(100000);


Console.ReadKey();

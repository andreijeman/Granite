using Granite.Controls.Holders;
using Granite.Graphics.Components;
using Granite.Graphics.Frames;
using Granite.Graphics.Utilities;
using Granite.IO;
using Granite.UI;
using Granite.UI.Entities;
using Granite.UI.Entities.Args;
using Granite.UI.Utilities;

var container = new Container(new ContainerArgs
{
    Width = 40,
    Height = 20,
    Backgound = new Color("CCFF99"),
    BackgroundFocused = new Color("0000CC")
});

container.BindToIO();


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

container.Add(btn);
container.Add(btn2);
container.Add(label);

container.Frame.Draw();

await Task.Delay(100000);



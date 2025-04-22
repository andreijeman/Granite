using Granite.Controls.Holders;
using Granite.Graphics.Components;
using Granite.Graphics.Frames;
using Granite.Graphics.Utilities;
using Granite.IO;
using Granite.UI;
using Granite.UI.Entities;
using Granite.UI.Entities.Args;
using Granite.UI.Utilities;

Console.Clear();

var container = new Container(new ContainerArgs
{
    Width = 40,
    Height = 20,
    IdleColor = new Color("C0C0C0"),
    FocusedColor = new Color("99CCFF"),
    BackgroundCOlor = new Color("808080")
});

var tbox = new TextBox(new TextBoxArgs
{
    Left = 0,
    Width = 10,
    Height = 5,
    IdleColor = new Color("C0C0C0"),
    FocusedColor = new Color("99CCFF"),
    TextColor = new Color("FFFFFF"),
    Text = "aaaaaaaa bbbbbbbb cccccccc zzz",
});

container.Add(tbox);

container.BindIO();
container.Show();

await Task.Delay(100000);



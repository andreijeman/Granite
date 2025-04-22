using Granite.UI.Entities;
using Granite.UI.EntitiesArgs;
using Granite.UI.Utilities;

Console.Clear();

var container = new Container(new ContainerArgs
{
    Width = 40,
    Height = 20,
});

var label = new Label(new LabelArgs
{
    Width = 100,
    Height = 1,
    Left = 5,
    Text = "Granite Console UI Sample",
});

var button = new Button(new ButtonArgs
{
    Top = 3,
    Left = 1, 
    Width = 10,
    Height = 3,
    Text = "Click!",
    OnPressed = Console.Beep,
});

var textBox = new TextBox(new TextBoxArgs
{
    Top = 2,
    Left = 12,
    Width = 10,
    Height = 5,
    Text = "Press Tab to move to next element, Enter to select container and Escape to escape from container!",
});

var inputBox = new InputBox(new InputBoxArgs
{
    Top = 2,
    Left = 24,
    Width = 10,
    Height = 5,
    Text = "Write something!",
});

container.Add(label);
container.Add(button);
container.Add(textBox);
container.Add(inputBox);

container.BindIO();

await Task.Delay(1000000);



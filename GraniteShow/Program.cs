using Granite.Graphics.Components;
using Granite.Graphics.Frames;
using Granite.Graphics.Objects;
using Granite.Graphics.Utilities;
using Granite.IO;

Frame frame = new Frame
{
    Model = new Model(16, 8).FillDefault()
};

frame.DrawRequested += Output.OnDrawRequested;

BaseObject obj = new BaseObject
{
    Model = new Model(4, 2).Fill('F', new Color(0, 255, 255), new Color(0, 255, 255)),
    Left = 2,
    Top = 2,
};

frame.Add(obj);

frame.Draw();



Thread.Sleep(10000);
using Granite.Controls.Controllers;
using Granite.Graphics.Components;
using Granite.Graphics.Objects;
using Granite.Graphics.Utilities;
using Granite.UI.Entities.Args;

namespace Granite.UI.Entities;

public abstract class Entity
{
    public readonly GObject GObject = new();
    public readonly Controller Controller = new();
    
    public Entity(EntityArgs args)
    {
        Controller.Focused += OnFocused;

        foreach (var pair in args.keyActionDict)
        {
            Controller.AddKeyAction(pair.Key, pair.Value);
        }
        
        GObject.Left = args.Left;
        GObject.Top = args.Top;
        GObject.Model = new Model(args.Width, args.Height).Init();
    }
    
    protected abstract void SculptModel();
    protected abstract void OnFocused(bool isFocused);
    
    public bool IsFocused { get => Controller.IsFocused; }
}
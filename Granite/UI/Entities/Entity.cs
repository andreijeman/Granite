using Granite.Controls.Controllers;
using Granite.Graphics.Components;
using Granite.Graphics.Objects;
using Granite.Graphics.Utilities;
using Granite.UI.Entities.Args;

namespace Granite.UI.Entities;

public abstract class Entity
{
    public GObject GObject { get; protected set; } = new();
    public Controller Controller { get; protected set; } = new();
    
    public Entity(EntityArgs args)
    {
        Controller.Focused += (isFocused) =>
        {
            if (isFocused) OnFocused();
            else OnUnfocused();
        }; 

        foreach (var pair in args.keyActionDict)
        {
            Controller.AddKeyAction(pair.Key, pair.Value);
        }
        
        GObject.Left = args.Left;
        GObject.Top = args.Top;
        GObject.Model = new Model(args.Width, args.Height).Init();
    }
    
    protected abstract void OnFocused();
    protected abstract void OnUnfocused();
    
    public bool IsFocused { get => Controller.IsFocused; }
}
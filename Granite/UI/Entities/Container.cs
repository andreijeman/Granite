using Granite.Controls.Holders;
using Granite.Graphics.Components;
using Granite.Graphics.Frames;
using Granite.Graphics.Objects;
using Granite.Graphics.Utilities;
using Granite.UI.Entities.Args;

namespace Granite.UI.Entities;

public class Container : Entity
{
    public Frame Frame { get; } = new();
    public ControllerHolder CtrlHolder { get; } = new();
    
    private Color _bgColor;
    private Color _bgFocusedColor;
    
    public Container(ContainerArgs args) : base(args)
    {
        CtrlHolder.Focused += (isFocused) =>
        {
            if (isFocused) OnFocused();
            else OnUnfocused();
        }; 
        
        base.GObject = Frame;
        base.Controller = CtrlHolder;
        
        _bgColor = args.Backgound;
        _bgFocusedColor = args.BackgroundFocused;
        
        Frame.Model = new Model(args.Width, args.Height).Init();
        Frame.Model.FillBackground(_bgColor);
    }

    protected override void OnFocused()             
    {
        Frame.Model.FillBackground(_bgFocusedColor);
        Frame.DrawBackground();
    }

    protected override void OnUnfocused()
    {
        Frame.Model.FillBackground(_bgColor);
        Frame.DrawBackground();
    }
    

    public void Add(Entity entity)
    {
        Frame.Add(entity.GObject);
        CtrlHolder.Add(entity.Controller);
    }
    
    public void Add(Container container)
    {
        Frame.Add(container.Frame);
        CtrlHolder.Add(container.CtrlHolder);
    }

    public void Add(GObject obj)
    {
        Frame.Add(obj);
    }
}
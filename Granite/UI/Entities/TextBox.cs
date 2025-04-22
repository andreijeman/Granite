using System.Text;
using Granite.Controls.Controllers;
using Granite.Graphics.Components;
using Granite.Graphics.Frames;
using Granite.Graphics.Maths;
using Granite.Graphics.Objects;
using Granite.Graphics.Utilities;
using Granite.UI.Entities.Args;

namespace Granite.UI.Entities;

public class TextBox : Entity
{
    public readonly Frame _frame;
    
    protected StringBuilder _text = new();
    
    protected Color _idleColor;
    protected Color _focusedColor;
    protected Color _textColor;
    protected Color _backgroundColor;
    
    protected List<GObject> _chunks = new();
    protected GObject _chunk;
    protected int _chunkX, _chunkY;
    
    public TextBox(TextBoxArgs args) : base(new Frame(), new Controller(), args)
    {
        var mainFrame = (Frame)this.GObject ;
        
        _idleColor = args.IdleColor;
        _focusedColor = args.FocusedColor;
        
        mainFrame.Model = new Model(args.Width, args.Height)
            .Init()
            .FillBackground(args.BackgroundColor)
            .DrawBorder(args.Border, _idleColor);

        _frame = new Frame
        {
            Left = 1,
            Top = 1,
            Model = new Model(args.Width - 2, args.Height - 2)
                .Init()
                .FillBackground(args.BackgroundColor)
        };
        
        mainFrame.Add(_frame);
        _textColor = args.TextColor;
        _backgroundColor = args.BackgroundColor;
        
        _chunk = GetNewTextChunk();
        _chunks.Add(_chunk);
        _frame.Add(_chunk);
        
        AddText(args.Text);
        
        Controller.AddKeyAction(ConsoleKey.UpArrow, () => { if(_frame.OriginTop > 0) _frame.OriginTop--; });
        Controller.AddKeyAction(ConsoleKey.DownArrow, () => { if(_frame.OriginTop < _chunk.Top - 2) _frame.OriginTop++; });
    }
    
    protected override void OnFocused()
    {
        DrawFrameBorder(_focusedColor);
    }

    protected override void OnUnfocused()
    {
        DrawFrameBorder(_idleColor);
    }
    
    private void DrawFrameBorder(Color color)
    {
        GObject.Model
            .FillForeground(color, new Rect(0, 0, GObject.Width - 1, 0))
            .FillForeground(color, new Rect(0, 0, 0, GObject.Height - 1))
            .FillForeground(color, new Rect(GObject.Width - 1, 0, GObject.Width - 1, GObject.Height - 1))
            .FillForeground(color, new Rect(0, GObject.Height - 1, GObject.Width - 1, GObject.Height - 1));
        
        GObject.Draw(new Rect(0, 0, GObject.Width - 1, 0));
        GObject.Draw(new Rect(0, 0, 0, GObject.Height - 1));
        GObject.Draw(new Rect(GObject.Width - 1, 0, GObject.Width - 1, GObject.Height - 1));
        GObject.Draw(new Rect(0, GObject.Height - 1, GObject.Width - 1, GObject.Height - 1));
    }
    
    public void AddText(string text)
    {
        _text.Append(text);
        
        int index = 0;
        int i, j = 0;
        
        for (i = _chunkY; i < _chunk.Height; i++)
        {
            while(index < text.Length && text[index] == ' ') index++;
            
            for (j = _chunkX; j < _chunk.Width; j++)
            {
                if (index >= text.Length) goto End; 
                _chunk.Model.Data[i, j].Character = text[index++];;
            }
        }
        
        End:
        
        _chunkY = i;
        _chunkX = j;
        
        if (index < text.Length - 1)
        {
            _chunkX = _chunkY = 0;
            _chunk = GetNewTextChunk();
            _chunks.Add(_chunk);
            _frame.Add(_chunk);
            
            AddText(text.Substring(index));
        }
    }
    
    private GObject GetNewTextChunk()
    {
        return new GObject
        {
            Left = 0,
            Top = _chunks.Count * (this.GObject.Height - 2),
            Model = new Model(this.GObject.Width - 2, this.GObject.Height - 2)
                .Init()
                .FillBackground(_backgroundColor)
                .FillForeground(_textColor)
        };
    }   
}
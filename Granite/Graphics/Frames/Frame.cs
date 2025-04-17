using Granite.Graphics.Components;
using Granite.Graphics.EventArgs;
using Granite.Graphics.Maths;
using Granite.Graphics.Objects;

namespace Granite.Graphics.Frames;

public class Frame : GObject
{
    private int _originLeft;
    private int _originTop;

    private Rect _mainRect;

    private List<GObject> _objects = new();
    private Dictionary<GObject, Rect> _objectRectDict = new();

    public override Model Model
    {
        get => _model;
        set
        {
            _model = value;
            _mainRect = GetMainRect(this);
        }
    }

    public override void Draw()
    {
        foreach (var obj in _objects)
        {
            obj.Draw();
        }

        DrawModelRects(
            GetModelDrawableRects(this, _mainRect),
            _model, _originLeft, _originTop);
    }

    public override void Draw(Rect section)
    {
        DrawFrameRect(GetMainRect(this, section));
    }

    public int OriginLeft
    {
        get => _originLeft;
        set
        {
            _originLeft = value;
            Draw();
        }
    }

    public int OriginTop
    {
        get => _originTop;
        set
        {
            _originTop = value;
            Draw();
        }
    }

    public void Add(GObject obj) => Add(obj, _objects.Count);

    public void Add(GObject obj, int layer)
    {
        if (!_objectRectDict.ContainsKey(obj))
        {
            layer = Math.Max(0, Math.Min(layer, _objectRectDict.Count));
            
            _objects.Insert(layer, obj);
            _objectRectDict.Add(obj, GetObjectRect(obj));

            obj.DrawRequested += OnDrawRequested;
            obj.LayoutChanged += OnLayoutChanged;
        }
    }

    public void BringForward(GObject obj)
    {
        int index = _objects.IndexOf(obj);

        if(index < _objects.Count - 1)
        {
            var temp = _objects[index];
            _objects[index] = _objects[index + 1];
            _objects[index + 1] = temp;
        }

        obj.Draw();
    }

    public void SendBackward(GObject obj)
    {
        int index = _objects.IndexOf(obj);

        if (index > 0)
        {
            var temp = _objects[index];
            _objects[index] = _objects[index - 1];
            _objects[index - 1] = temp;
        }

        if (_mainRect.TryGetIntersection(_objectRectDict[obj], out var intersection))
        {
            DrawFrameRect(intersection);
        }
    }

    private void OnDrawRequested(GObject sender, DrawEventArgs args)
    {
        DrawModelRects(
            GetModelDrawableRects(sender, 
                ModelSectionToRect(args.Section, args.Left, args.Top)),
            args.Model, args.Left, args.Top);
    }

    private void OnLayoutChanged(GObject sender)
    {
        var oldRect = _objectRectDict[sender];
        var newRect = GetObjectRect(sender);
        _objectRectDict[sender] = newRect;

        if (_mainRect.TryGetIntersection(oldRect, out var intersection))
        {
            if (intersection.TryGetUncoveredSections(newRect, out var sections))
            {
                foreach (var section in sections)
                {
                    DrawFrameRect(section);
                }
            }
        }

        sender.Draw();
    }

    private List<Rect> GetModelDrawableRects(GObject target, Rect targetRect)
    {
        List<Rect> rects = new List<Rect>();

        if (_mainRect.TryGetIntersection(targetRect, out Rect intersection))
        {
            rects.Add(intersection);

            for (int i = _objects.IndexOf(target) + 1; i < _objects.Count; i++)
            {
                var obj = _objects[i];
                List<Rect> temp = new List<Rect>();

                foreach (var sect in rects)
                {
                    if (sect.TryGetUncoveredSections(_objectRectDict[obj], out var results))
                    {
                        temp.AddRange(results);
                    }
                }
                rects = temp;
            }
        }

        return rects;
    }

    private void DrawModelRects(List<Rect> rects, Model model, int left, int top)
    {
        foreach (var rect in rects)
        {
            InvokeDrawRequested(new DrawEventArgs
            {
                Model = model,
                Section = RectToModelSection(rect, left, top),
                Left = _left + rect.X1 - _originLeft,
                Top = _top + rect.Y1 - _originTop
            });
        }
    }

    private void DrawFrameRect(Rect rect)
    {
        var temp = _mainRect;
        _mainRect = rect;

        foreach (var obj in _objects)
        {
            obj.Draw();
        }

        DrawModelRects(
            GetModelDrawableRects(this, _mainRect),
            _model, _originLeft, _originTop);

        _mainRect = temp;
    }

    private static Rect GetObjectRect(GObject obj)
    {
        return new Rect
        {
            X1 = obj.Left,
            Y1 = obj.Top,
            X2 = obj.Left + obj.Width - 1,
            Y2 = obj.Top + obj.Height - 1
        };
    }

    private static Rect GetMainRect(Frame frame)
    {
        return new Rect
        {
            X1 = frame.OriginLeft,
            Y1 = frame.OriginTop,
            X2 = frame.OriginLeft + frame.Width - 1,
            Y2 = frame.OriginTop + frame.Height - 1
        };
    }

    private static Rect GetMainRect(Frame frame, Rect section)
    {
        return new Rect
        {
            X1 = frame.OriginLeft + section.X1,
            Y1 = frame.OriginTop + section.Y1,
            X2 = frame.OriginLeft + section.X2,
            Y2 = frame.OriginTop + section.Y2
        };
    }

    private static Rect ModelSectionToRect(Rect section, int left, int top)
    {
        return new Rect
        {
            X1 = left + section.X1,
            Y1 = top + section.Y1,
            X2 = left + section.X2,
            Y2 = top + section.Y2
        };
    }

    private static Rect RectToModelSection(Rect section, int left, int top)
    {
        return new Rect
        {
            X1 = section.X1 - left,
            Y1 = section.Y1 - top,
            X2 = section.X2 - left,
            Y2 = section.Y2 - top
        };
    }
}

namespace Granite.Graphics;

public sealed class Frame : Object2D
{
    private LinkedList<Object2D> _objects = new();

    private  int _localLeft;
    private int _localTop;

    public int LocalLeft
    {
        get => _localLeft;
        set
        {
            _localLeft = value;
            InvokeEntireModelChangedEvent();
        }
    }

    public int LocalTop
    {
        get => _localTop;
        set
        {
            _localTop = value;
            InvokeEntireModelChangedEvent();
        }
    }

    public void AddObject(Object2D obj)
    {
        if (!_objects.Contains(obj))
        {
            obj.ModelChangedEvent += OnModelChangedEvent;
            obj.PositionChangedEvent += OnPositionChangedEvent;
            _objects.AddLast(obj);
        }
    }
    
    private void OnModelChangedEvent(ModelChangedData data)
    {
        if(RectMath.TryGetIntersection(
               new RectMath.Rect()
               {
                   X1 = this.Left,
                   Y1 = this.Top,
                   X2 = this.Left + this.Width - 1,
                   Y2 = this.Top + this.Height - 1,
               },
               new RectMath.Rect()
               {
                   X1 = data.Left,
                   Y1 = data.Top,
                   X2 = data.Left + data.X2 - data.X1,
                   Y2 = data.Top + data.Y2 - data.Y1,
               },
               out RectMath.Rect intersection))
        {
            var enumerator = _objects.GetEnumerator();
            List<RectMath.Rect> sections = new List<RectMath.Rect>();
            sections.Add(intersection);
            
            while (enumerator.MoveNext() && enumerator.Current != data.Object)
            {
                Object2D obj = enumerator.Current;
                List<RectMath.Rect> tempList = new List<RectMath.Rect>();
                foreach (RectMath.Rect section in sections)
                {
                    if (section.TryGetUncoveredSections(
                            new RectMath.Rect()
                            {
                                X1 = obj.Left,
                                Y1 = obj.Top,
                                X2 = obj.Left + obj.Width - 1,
                                Y2 = obj.Top + obj.Height - 1
                            },
                            out List<RectMath.Rect> uncovereds))
                    {
                        tempList.AddRange(uncovereds);
                    }
                }
                
                sections = tempList;
            }

            foreach (RectMath.Rect section in sections)
            {
                this.InvokeModelChangedEvent(
                    new ModelChangedData()
                    {
                        Object = data.Object,
                        X1 = section.X1 - data.Left,
                        Y1 = section.Y1 - data.Top,
                        X2 = section.X2 - data.Left,
                        Y2 = section.Y2 - data.Top,
                        Left = this.Left + section.X1 - this.LocalLeft,
                        Top = this.Top + section.Y1 - this.LocalTop
                    });
            }
        }
        
    }

    private void OnPositionChangedEvent(PositionChangedData data)
    {
        if (RectMath.TryGetUncoveredSections(
                new RectMath.Rect()
                {
                    X1 = data.Left,
                    Y1 = data.Top,
                    X2 = data.Left + data.Object.Width - 1,
                    Y2 = data.Top + data.Object.Height - 1,
                },
                new RectMath.Rect()
                {
                    X1 = data.NewLeft,
                    Y1 = data.NewTop,
                    X2 = data.NewLeft + data.Object.Width - 1,
                    Y2 = data.NewTop + data.Object.Height - 1,
                },
                out List<RectMath.Rect> sections))
        {
            for (var node = _objects.Find(data.Object)?.Next; node != null; node = node.Next)
            {
                Object2D obj = node.Value;
                foreach (RectMath.Rect section in sections)
                {
                    if (section.TryGetIntersection(
                            new RectMath.Rect()
                            {
                                X1 = obj.Left,
                                Y1 = obj.Top,
                                X2 = obj.Left + obj.Width - 1,
                                Y2 = obj.Top + obj.Height - 1
                            },
                            out RectMath.Rect intersection))
                    {
                        if (obj is Frame)
                        {
                            obj.InvokeModelChangedEvent(
                                new ModelChangedData()
                                {
                                    Object = obj,
                                    X1 = intersection.X1,
                                    
                                });
                        }
                    }
                    
                }
            }
        }
    }

    public override void InvokeModelChangedEvent(ModelChangedData data)
    {
        RectMath.Rect frameRect = new RectMath.Rect()
        {
            X1 = data.Left,
            Y1 = data.Top,
            X2 = data.Left + data.X2 - data.X1,
            Y2 = data.Top + data.Y2 - data.Y1
        };
        
        foreach (Object2D obj in _objects)
        {
            if (frameRect.TryGetIntersection(
                    new RectMath.Rect()
                    {
                        X1 = obj.Left,
                        Y1 = obj.Top,
                        X2 = obj.Left + obj.Width - 1,
                        Y2 = obj.Top + obj.Height - 1
                    },
                    out RectMath.Rect intersection))
            {
                obj.InvokeModelChangedEvent(
                    new ModelChangedData()
                    {
                        Object = data.Object,
                        X1 = intersection.X1 - data.Left,
                        Y1 = intersection.Y1 - data.Top,
                        X2 = intersection.X2 - data.Left,
                        Y2 = intersection.Y2 - data.Top,
                        Left = this.Left + intersection.X1 - this.LocalLeft,
                        Top = this.Top + intersection.Y1 - this.LocalTop
                    });
            }
        }
    }

    public override void SculptModel()
    {
        Cell cell = new Cell();
        for (int i = 0; i < this.Height; i++)
        {
            for (int j = 0; j < this.Width / 2; j++)
            {
                cell.Character = ' ';
                cell.BackgroundRgbColor = (i + j) % 2 == 0 ? new Cell.RgbColor() {R = 64, G = 64, B = 64 } : new Cell.RgbColor() {R = 128, G = 128, B = 128 };

                this.Model[i, 2 * j] = cell;
                this.Model[i, 2 * j + 1] = cell;
            }
        }
    }
}
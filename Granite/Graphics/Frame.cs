namespace Granite.Graphics;

public class Frame : Object2D
{
    private List<Object2D> _objects = new List<Object2D>();

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
    
    public void AddFront(Object2D obj)
    {
        if (!_objects.Contains(obj))
        {
            obj.ModelChangedEvent += OnModelChangedEvent;
            obj.PositionChangedEvent += OnPositionChangedEvent;
            obj.SizeChangedEvent += OnSizeChangedEvent;
            _objects.Insert(0, obj);
        }
    }
    
    public void AddBack(Object2D obj)
    {
        if (!_objects.Contains(obj))
        {
            obj.ModelChangedEvent += OnModelChangedEvent;
            obj.PositionChangedEvent += OnPositionChangedEvent;
            obj.SizeChangedEvent += OnSizeChangedEvent;
            _objects.Add(obj);
        }
    }

    public void ShiftBy(Object2D obj, int steps)
    {
        int index = _objects.IndexOf(obj);
        int newIndex = Math.Max(0, Math.Min(index - steps, _objects.Count - 1));
        var temp = _objects[newIndex];
        _objects[newIndex] = obj;
        _objects[index] = temp;
        
        InvokeModelChangedEvent(new ModelChangedData()
        {
            Object = this,
            SectX1 = obj.Left - LocalLeft,
            SectY1 = obj.Top - LocalTop,
            SectX2 = obj.Left - LocalLeft + obj.Width - 1,
            SectY2 = obj.Top - LocalTop + obj.Height - 1,
            SectLeft = Left + obj.Left - LocalLeft,
            SectTop = Top + obj.Top - LocalTop
        });
    }

    public void BringForward(Object2D obj) => ShiftBy(obj, 1);
    public void SendBackward(Object2D obj) => ShiftBy(obj, -1);
    
    
    public override void SculptModel()
    {
        Cell cell = new Cell();
        for (int i = 0; i < this.Height; i++)
        {
            for (int j = 0; j < this.Width / 2; j++)
            {
                cell.Character = ' ';
                cell.BackgroundRgbColor = (i + j) % 2 == 0 ? new RgbColor() {R = 64, G = 64, B = 64 } : new RgbColor() {R = 128, G = 128, B = 128 };

                this.Model[i, 2 * j] = cell;
                this.Model[i, 2 * j + 1] = cell;
            }
        }
    }
    public override void InvokeModelChangedEvent(ModelChangedData data)
    {
        data.SectLeft = LocalLeft + data.SectX1;
        data.SectTop = LocalTop + data.SectY1;
        OnModelChangedEvent(this, data);
        
        Rect frameSect = new Rect()
        {
            X1 = data.SectLeft,
            Y1 = data.SectTop,
            X2 = data.SectLeft + data.SectX2 - data.SectX1,
            Y2 = data.SectTop + data.SectY2 - data.SectY1
        };

        foreach (var obj in _objects)
        {
            if (frameSect.TryGetIntersection(
                    new Rect()
                    {
                        X1 = obj.Left,
                        Y1 = obj.Top,
                        X2 = obj.Left + obj.Width - 1,
                        Y2 = obj.Top + obj.Height - 1
                    },
                    out Rect isect))
            {
                obj.InvokeModelChangedEvent(new ModelChangedData() 
                {
                    Object = obj,
                    SectX1 = isect.X1 - obj.Left,
                    SectY1 = isect.Y1 - obj.Top,
                    SectX2 = isect.X2 - obj.Left,
                    SectY2 = isect.Y2 - obj.Top,
                    SectLeft = isect.X1,
                    SectTop = isect.Y1
                });
            }
        }
    }
    
    private void OnModelChangedEvent(Object2D sender, ModelChangedData data)
    {
        if (RectMath.TryGetIntersection(
                new Rect()
                {
                    X1 = LocalLeft,
                    Y1 = LocalTop,
                    X2 = LocalLeft + Width - 1,
                    Y2 = LocalTop + Height - 1
                },
                new Rect()
                {
                    X1 = data.SectLeft,
                    Y1 = data.SectTop,
                    X2 = data.SectLeft + data.SectX2 - data.SectX1,
                    Y2 = data.SectTop + data.SectY2 - data.SectY1
                },
                out Rect isect))
        {
            List<Rect> sections = new List<Rect>();
            sections.Add(isect);
            
            var enumerator = _objects.GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current != sender)
            {
                var obj = enumerator.Current;
                List<Rect> temp = new List<Rect>();
                foreach (var sect in sections)
                {
                    if (sect.TryGetUncoveredSections(
                            new Rect()
                            {
                                X1 = obj.Left,
                                Y1 = obj.Top,
                                X2 = obj.Left + obj.Width - 1,
                                Y2 = obj.Top + obj.Height - 1
                            },
                            out var results))
                    {
                        temp.AddRange(results);
                    }
                }
                sections = temp;
            }

            foreach (var sect in sections)
            {
                base.InvokeModelChangedEvent(new ModelChangedData()
                {
                    Object = data.Object,
                    SectX1 = data.SectX1 + sect.X1 - data.SectLeft,
                    SectY1 = data.SectY1 + sect.Y1 - data.SectTop,
                    SectX2 = data.SectX1 + sect.X2 - data.SectLeft,
                    SectY2 = data.SectY1 + sect.Y2 - data.SectTop,
                    SectLeft = Left + sect.X1 - LocalLeft,
                    SectTop = Top + sect.Y1 - LocalTop
                });
            }
        }
    }

    private void OnPositionChangedEvent(Object2D sender, PositionChangedData data)
    {
       FreeSection(
           data.Object, 
           new Rect()
           {
               X1 = data.PrevLeft,
               Y1 = data.PrevTop,
               X2 = data.PrevLeft + data.Object.Width - 1,
               Y2 = data.PrevTop + data.Object.Height - 1
           },
           new Rect()
           {
               X1 = data.Object.Left,
               Y1 = data.Object.Top,
               X2 = data.Object.Left + data.Object.Width - 1,
               Y2 = data.Object.Top + data.Object.Height - 1
           });
    }
    
    private void OnSizeChangedEvent(Object2D sender, SizeChangedData data)
    {
        FreeSection(
            data.Object, 
            new Rect()
            {
                X1 = data.Object.Left,
                Y1 = data.Object.Top,
                X2 = data.Object.Left + data.PrevWidth - 1,
                Y2 = data.Object.Top + data.PrevHeight - 1
            },
            new Rect()
            {
                X1 = data.Object.Left,
                Y1 = data.Object.Top,
                X2 = data.Object.Left + data.Object.Width - 1,
                Y2 = data.Object.Top + data.Object.Height - 1
            });
    }

    private void FreeSection(Object2D pivotObj, Rect prevSect, Rect newSect)
    {
        if(RectMath.TryGetIntersection(
               new Rect()
               {
                   X1 = LocalLeft,
                   Y1 = LocalTop,
                   X2 = LocalLeft + Width - 1,
                   Y2 = LocalTop + Height - 1
               },
               prevSect,
               out var isect))
        {
            if (isect.TryGetUncoveredSections(newSect, out var sections))
            {
                var enumerator = _objects.GetEnumerator();
                while (enumerator.MoveNext() && enumerator.Current != pivotObj)
                {
                    var obj = enumerator.Current;
                    List<Rect> temp = new List<Rect>();
                    foreach (var sect in sections)
                    {
                        if (sect.TryGetUncoveredSections(
                                new Rect()
                                {
                                    X1 = obj.Left,
                                    Y1 = obj.Top,
                                    X2 = obj.Left + obj.Width - 1,
                                    Y2 = obj.Top + obj.Height - 1
                                },
                                out var results))
                        {
                            temp.AddRange(results);
                        }
                    }
                    sections = temp;
                }
                
                while (enumerator.MoveNext())
                {
                    var obj = enumerator.Current;
                    var objRect = new Rect()
                    {
                        X1 = obj.Left,
                        Y1 = obj.Top,
                        X2 = obj.Left + obj.Width - 1,
                        Y2 = obj.Top + obj.Height - 1
                    };
                    
                    List<Rect> temp = new List<Rect>();
                    foreach (var sect in sections)
                    {
                        if (sect.TryGetIntersection(objRect, out var result))
                        {
                            obj.InvokeModelChangedEvent(new ModelChangedData()
                            {
                                Object = obj,
                                SectX1 = result.X1 - obj.Left,
                                SectY1 = result.Y1 - obj.Top,
                                SectX2 = result.X2 - obj.Left,
                                SectY2 = result.Y2 - obj.Top,
                                SectLeft = result.X1,
                                SectTop = result.Y1,
                            });
                        }
                        
                        if (sect.TryGetUncoveredSections(result, out var results))
                        {
                            temp.AddRange(results);
                        }
                    }
                    sections = temp;
                }
                
                foreach (var sect in sections)
                {
                    base.InvokeModelChangedEvent(new ModelChangedData()
                    {
                        Object = this,
                        SectX1 = sect.X1 - LocalLeft,
                        SectY1 = sect.Y1 - LocalTop,
                        SectX2 = sect.X2 - LocalLeft,
                        SectY2 = sect.Y2 - LocalTop,
                        SectLeft = Left + sect.X1 - LocalLeft,
                        SectTop = Top + sect.Y1 - LocalTop,
                    });
                }
            }
        }
    }
}
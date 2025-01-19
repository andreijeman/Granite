namespace Granite.Graphics;

public sealed class Frame : Object2D
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
    public override void InvokeModelChangedEvent(ModelChangedData data)
    {
        data.SectLeft += LocalLeft + data.SectX1;
        data.SectTop += LocalTop + data.SectY1;
        OnModelChangedEvent(this, data);
        
        RectMath.Rect frameSect = new RectMath.Rect()
        {
            X1 = data.SectLeft,
            Y1 = data.SectTop,
            X2 = data.SectLeft + data.SectX2 - data.SectX1,
            Y2 = data.SectLeft + data.SectY2 - data.SectY1,
        };

        foreach (var obj in _objects)
        {
            if (frameSect.TryGetIntersection(
                    new RectMath.Rect()
                    {
                        X1 = obj.Left,
                        Y1 = obj.Top,
                        X2 = obj.Left + obj.Width - 1,
                        Y2 = obj.Top + obj.Height - 1
                    },
                    out RectMath.Rect isect))
            {
                obj.InvokeModelChangedEvent(new ModelChangedData() 
                {
                    Object = obj,
                    SectX1 = isect.X1 - obj.Left,
                    SectY1 = isect.Y1 - obj.Top,
                    SectX2 = isect.X2 - obj.Left,
                    SectY2 = isect.Y2 - obj.Top,
                    SectLeft = Left + isect.X1 - LocalLeft,
                    SectTop = Top + isect.Y1 - LocalTop,
                });
            }
        }
    }
    
    private void OnModelChangedEvent(Object2D sender, ModelChangedData data)
    {
        if (RectMath.TryGetIntersection(
                new RectMath.Rect()
                {
                    X1 = LocalLeft,
                    Y1 = LocalTop,
                    X2 = LocalLeft + Width - 1,
                    Y2 = LocalTop + Height - 1
                },
                new RectMath.Rect()
                {
                    X1 = data.SectLeft,
                    Y1 = data.SectTop,
                    X2 = data.SectLeft + data.SectX2 - data.SectX1,
                    Y2 = data.SectTop + data.SectY2 - data.SectY1
                },
                out RectMath.Rect isect))
        {
            List<RectMath.Rect> sections = new List<RectMath.Rect>();
            sections.Add(isect);
            
            var enumerator = _objects.GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current != sender)
            {
                var obj = enumerator.Current;
                List<RectMath.Rect> temp = new List<RectMath.Rect>();
                foreach (var sect in sections)
                {
                    if (sect.TryGetUncoveredSections(
                            new RectMath.Rect()
                            {
                                X1 = obj.Left,
                                Y1 = obj.Top,
                                X2 = obj.Left + obj.Width - 1,
                                Y2 = obj.Top + obj.Height - 1
                            },
                            out var result))
                    {
                        temp.AddRange(result);
                    }
                }
                sections = temp;
            }

            foreach (var sect in sections)
            {
                base.InvokeModelChangedEvent(new ModelChangedData()
                {
                    Object = data.Object,
                    SectX1 = sect.X1 - data.SectLeft,
                    SectY1 = sect.Y1 - data.SectTop,
                    SectX2 = sect.X2 - data.SectLeft,
                    SectY2 = sect.Y2 - data.SectTop,
                    SectLeft = Left + sect.X1 - LocalLeft,
                    SectTop = Top + sect.Y1 - LocalTop,
                });
            }
        }
    }

    private void OnPositionChangedEvent(Object2D sender, PositionChangedData data)
    {
       
    }
    
    private void OnSizeChangedEvent(Object2D sender, SizeChangedData data)
    {
       
    }
}
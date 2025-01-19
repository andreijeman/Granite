namespace Granite.Graphics;

public class Object2D
{
    public Cell[,] Model { get; private set; } = new Cell[0, 0];
    
    private int _left;
    private int _top;
    private int _width;
    private int _height;
    
    public int Left
    {
        get => _left;
        set
        {
            int prevLeft = _left;
            _left = value;
            PositionChangedEvent?.Invoke(
                this,
                new PositionChangedData()
                {
                    Object = this,
                    PrevLeft = prevLeft,
                    PrevTop = _top
                });
        }
    }

    public int Top
    {
        get => _top;
        set
        {
            int prevTop = _top;
            _top = value;
            PositionChangedEvent?.Invoke(
                this,
                new PositionChangedData()
                {
                    Object = this,
                    PrevLeft = _left,
                    PrevTop = prevTop
                });
        }
    }
    
    public int Width
    {
        get => _width;
        set
        {
            int prevWidth = _width;
            _width = value;
            Model = new Cell[_height, _width];
            SculptModel();
            SizeChangedEvent?.Invoke(
                this,
                new SizeChangedData()
                {
                    Object = this,
                    PrevWidth = prevWidth,
                    PrevHeight = _height,
                });
            InvokeEntireModelChangedEvent();
        }
    }

    public int Height
    {
        get => _height;
        set
        {
            int prevHeight = _height;
            _height = value;
            Model = new Cell[_height, _width];
            SculptModel();
            SizeChangedEvent?.Invoke(
                this,
                new SizeChangedData()
                {
                    Object = this,
                    PrevWidth = _width,
                    PrevHeight = prevHeight,
                });
            InvokeEntireModelChangedEvent();
        }
    }
    
    public virtual void SculptModel()
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                Model[i, j] = new Cell();

            }
        }
    }

    public event Action<Object2D, ModelChangedData>? ModelChangedEvent;
    public event Action<Object2D, PositionChangedData>? PositionChangedEvent;
    public event Action<Object2D, SizeChangedData>? SizeChangedEvent;

    public virtual void InvokeModelChangedEvent(ModelChangedData modelChangedData)
    {
        ModelChangedEvent?.Invoke(this, modelChangedData);
    }

    public void InvokeEntireModelChangedEvent()
    {
        InvokeModelChangedEvent(
            new ModelChangedData()
            {
                Object = this,
                SectX1 = 0,
                SectY1 = 0,
                SectX2 = _width - 1,
                SectY2 = _height - 1,
                SectLeft = _left,
                SectTop = _top
            });
    }

    public struct ModelChangedData
    {
        public Object2D Object { get; init; }
        public int SectLeft, SectTop;
        public int SectX1, SectY1, SectX2, SectY2;
    }
    
    public struct PositionChangedData
    {
        public Object2D Object { get; init; }
        public int PrevLeft, PrevTop;
    }
    
    public struct SizeChangedData
    {
        public Object2D Object { get; init; }
        public int PrevWidth, PrevHeight;
    }
}
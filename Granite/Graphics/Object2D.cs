namespace Granite.Graphics;

public abstract class Object2D
{
    private int _left;
    private int _top;
    private int _width;
    private int _height;
    
    public int Left
    {
        get => _left;
        set
        {
            PositionChangedEvent?.Invoke(
                new PositionChangedData()
                {
                    Object = this,
                    Left = _left, Top = _top,
                    NewLeft = value, NewTop = _top,
                }); 
            _left = value;
            InvokeEntireModelChangedEvent();
        }
    }

    public int Top
    {
        get => _top;
        set
        {
            PositionChangedEvent?.Invoke(
                new PositionChangedData()
                {
                    Object = this,
                    Left = _left, Top = _top,
                    NewLeft = _left, NewTop = value,
                }); 
            _top = value;
            InvokeEntireModelChangedEvent();
        }
    }
    
    public int Width
    {
        get => _width;
        set
        {
            _width = value;
            Model = new Cell[_height, _width];
            SculptModel();
            InvokeEntireModelChangedEvent();
        }
    }

    public int Height
    {
        get => _height;
        set
        {
            _height = value;
            Model = new Cell[_height, _width]; 
            SculptModel();
            InvokeEntireModelChangedEvent();
        }
    }
    
    public Cell[,] Model { get; private set; } = new Cell[0, 0];

    public abstract void SculptModel();

    public event Action<ModelChangedData>? ModelChangedEvent;
    public event Action<PositionChangedData>? PositionChangedEvent;

    public virtual void InvokeModelChangedEvent(ModelChangedData modelChangedData)
    {
        ModelChangedEvent?.Invoke(modelChangedData);
    }

    public void InvokeEntireModelChangedEvent()
    {
        InvokeModelChangedEvent(
            new ModelChangedData()
            {
                Object = this,
                X1 = 0, Y1 = 0, X2 = _width - 1, Y2 = _height - 1,
                Left = _left, Top = _top
            });
    }

    public struct ModelChangedData
    {
        public Object2D Object { get; init; }
        //Model section 
        public int X1, Y1, X2, Y2;
        //Absolute position
        public int Left, Top;
    }
    
    public struct PositionChangedData
    {
        public Object2D Object { get; init; }
        public int Left, Top;
        public int NewLeft, NewTop;
    }
}
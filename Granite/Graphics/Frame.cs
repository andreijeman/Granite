namespace Granite.Graphics;

public sealed class Frame : Object2D
{
    private int _lowLayer, _topLayer;
    private Dictionary<Object2D, int> _objectLayerDict = new Dictionary<Object2D, int>();

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
        if (_objectLayerDict.TryAdd(obj, _topLayer))
        {
            _topLayer++;
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

    }
    
    private void OnModelChangedEvent(Object2D sender, ModelChangedData data)
    {
        
    }

    private void OnPositionChangedEvent(Object2D sender, PositionChangedData data)
    {
       
    }
    
    private void OnSizeChangedEvent(Object2D sender, SizeChangedData data)
    {
       
    }
}
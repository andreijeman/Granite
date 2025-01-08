using Granite.Components;
using Granite.Utilities;

namespace Granite.Entities;

public class Frame : Entity
{
    public Vector2 Origin { get; set; }

    private LinkedList<Entity> _entities = new LinkedList<Entity>();
    
    public void AddEntity(Entity entity)
    {
        _entities.AddLast(entity);
    }
    
    public override void SculptModel()
    {
        throw new NotImplementedException();
    }
}
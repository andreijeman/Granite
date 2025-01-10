using Granite.Utilities.Math;

namespace Granite.Entities;

public class Frame : Entity
{
    public Vector2 Origin { get; set; }

    private LinkedList<Entity> _entities = new LinkedList<Entity>();
    
    public void AddEntity(Entity entity)
    {
        entity.ModelChangedEvent += OnEntityModelChangedEvent;
        _entities.AddLast(entity);
    }


    protected override void SculptModel()
    {

    }

    protected override void OnEntityModelChangedEvent(Entity entity, Rect part, Vector2 absolutePosition)
    {
        if (TryGetAllUncoveredSections(this, entity, part, absolutePosition, out List<Rect> sections))
        {
            foreach (Rect section in sections)
            {
                InvokeModelChangedEvent(
                    entity, 
                    Rect.New(section.Pos - absolutePosition, section.Size), 
                    this.Position + section.Pos - this.Origin);
            }
        }
    }
    
    private bool TryGetAllUncoveredSections(Frame frame, Entity entity, Rect part, Vector2 absolutePosition, out List<Rect> sections)
    { 
        sections = new List<Rect>();
        Rect frameRect = new Rect(frame.Origin, frame.Size);
        
        if (frameRect.TryGetIntersection(Rect.New(absolutePosition, part.Size), out Rect inter))
        {
            sections.Add(inter);
            
            var enumerator = _entities.GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current != entity)
            {
                Rect rect = Rect.New(enumerator.Current.Position, enumerator.Current.Size);
                List<Rect> temp = new List<Rect>();
                foreach (Rect section in sections)
                {
                    if (section.TryGetUncoveredSections(rect, out var coveredSections))
                    {
                        temp.AddRange(coveredSections);
                    }
                }
                
                sections = temp;
            }

            if (sections.Count > 0) return true;
        }

        return false;
    }
}
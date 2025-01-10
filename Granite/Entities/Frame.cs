using Granite.Components;
using Granite.Utilities;
using Granite.Utilities.Math;

namespace Granite.Entities;

public class Frame : Entity
{
    public Vector2 Origin { get; set; }
    
    private LinkedList<Entity> _entities = new LinkedList<Entity>();

    public void AddEntity(Entity entity)
    {
        if (!_entities.Contains(entity))
        {
            entity.ModelChangedEvent += this.OnModelChangedEvent;
            entity.PositionChangedEvent += this.OnPositionChangedEvent;
            _entities.AddFirst(entity);
        }
    }
    
    protected override void SculptModel()
    {
    }

    protected override void OnModelChangedEvent(Entity interceptor, Entity sender, Rect part, Vector2 absolutePosition)
    {
        if (TryGetAllUncoveredSections(this, interceptor, part, absolutePosition, out List<Rect> sections))
        {
            foreach (Rect section in sections)
            {
                this.ModelChangedEvent?.Invoke(
                    this, sender, 
                    Rect.New(part.Pos + section.Pos - absolutePosition, section.Size), 
                    this.Position + section.Pos - this.Origin);
            }
        }
    }
    
    private bool TryGetAllUncoveredSections(Frame frame, Entity interceptor, Rect part, Vector2 absolutePosition, out List<Rect> sections)
    { 
        sections = new List<Rect>();
        Rect frameRect = new Rect(frame.Origin, frame.Size);
        
        if (frameRect.TryGetIntersection(Rect.New(absolutePosition, part.Size), out Rect inter))
        {
            sections.Add(inter);
            
            var enumerator = _entities.GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current != interceptor)
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

    public override void TriggerOnModelChangedEvent()
    {
        foreach (Entity entity in _entities)
        {
            entity.TriggerOnModelChangedEvent();
        }
    }

    public void OnPositionChangedEvent(Entity sender, Rect section)
    {
        if (Rect.New(this.Origin, this.Size).TryGetIntersection(section, out Rect inter))
        {
            var node = _entities.Find(sender);
            node = node?.Next;
            while(node != null)
            {
                var entity = node.Value;
                if (inter.TryGetIntersection(Rect.New(entity.Position, entity.Size), out Rect sec))
                {
                    this.ModelChangedEvent?.Invoke(
                        this, entity, 
                        Rect.New(sec.Pos - entity.Position, sec.Size), 
                        this.Position + sec.Pos - this.Origin);
                }
                node = node.Next;
            }
            
        }
    }
}
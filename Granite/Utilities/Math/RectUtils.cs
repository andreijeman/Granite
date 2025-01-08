using System.ComponentModel.Design;
using Granite.Components;

namespace Granite.Utilities;

public static class RectUtils
{ 
    public static bool Intersects(this Rect rect, Rect rect2)
    {
        return
            rect.Contains(rect2.Pos) || 
            rect.Contains(Vector2.New(rect2.Pos.X + rect2.Size.X - 1, rect2.Pos.Y)) ||
            rect.Contains(Vector2.New(rect2.Pos.X + rect2.Size.X - 1, rect2.Pos.Y + rect2.Pos.Y - 1)) ||
            rect.Contains(Vector2.New(rect2.Pos.X, rect2.Pos.Y + rect2.Size.Y - 1));
    }

    public static bool Contains(this Rect rect, Vector2 point)
    {
        return        
            point.X >= rect.Pos.X && point.X < rect.Pos.X + rect.Size.X && 
            point.Y >= rect.Pos.Y && point.Y < rect.Pos.Y + rect.Size.Y;
    }
    
    public static bool TryGetIntersection(this Rect rect, Rect rect2, out Rect result)
    {
        result = new Rect();
        
        if (rect.Intersects(rect2))
        {
            
            result.Pos.X = rect.Pos.X > rect2.Pos.X ? rect.Pos.X : rect2.Pos.X;
            result.Pos.Y = rect.Pos.Y > rect2.Pos.Y ? rect.Pos.Y : rect2.Pos.Y;

            result.Size.X = rect.Pos.X + rect.Size.X < rect2.Pos.X + rect2.Size.X ? rect.Size.X : rect2.Size.X;
            result.Size.X = rect.Pos.Y + rect.Size.Y < rect2.Pos.Y + rect2.Size.Y ? rect.Size.Y : rect2.Size.Y;

            return true;
        }
        
        return false;
    }

    public static bool TryGetUncoveredSections(this Rect rect, Rect rect2, out List<Rect> results)
    {
        results = new List<Rect>();
        
        if (rect.TryGetIntersection(rect2, out Rect inter))
        {
            Rect section = new Rect();

            Vector2 r1 = rect.Pos;
            Vector2 r2 = rect.Pos + rect.Size - Vector2.New(1, 1);
            Vector2 i1 = inter.Pos;
            Vector2 i2 = inter.Pos + inter.Size - Vector2.New(1, 1);
            
            // Left section
            if (r1.X < i1.X)
            {
                section.Pos.X = r1.X;
                section.Pos.Y = i1.Y;
                section.Size.X = i1.X - r1.X; 
                section.Size.Y = inter.Size.Y;
                
                results.Add(section);
            }
            
            // Right section
            if (r2.X > i2.X)
            {
                section.Pos.X = i2.X + 1;
                section.Pos.Y = i1.Y;
                section.Size.X = r2.X - i2.X;
                section.Size.Y = inter.Size.Y;
                
                results.Add(section);
            }
            
            // Top section
            if (r1.Y < i1.Y)
            {
                section.Pos.X = i1.X;
                section.Pos.Y = r1.Y;
                section.Size.X = inter.Size.X;
                section.Size.Y = r2.Y - i2.Y;
                
                results.Add(section);
            }
            
            // LeftTop section
            if (r1.X < i1.X && r1.Y < i1.Y)
            {
                section.Pos = r1;
                section.Size = i1 - r1;
                
                results.Add(section);
            }
            
            // RightBottom section
            if (r2.X > i2.X && r2.Y > i2.Y)
            {
                section.Pos = i2 + Vector2.New(1, 1);
                section.Size = r2 - i2;
                
                results.Add(section);
            }
            
            // LeftBottom section
            if (r1.X < i1.X && r2.Y > i2.Y)
            {
                section.Pos.X = r1.X;
                section.Pos.Y = i2.Y + 1;
                section.Size.X = i1.X - r1.X;
                section.Size.Y = r2.Y - i2.Y;
                
                results.Add(section);
            }
            
            // RightTop section
            if (r2.X > i2.X && r1.Y < i1.Y)
            {
                section.Pos.X = r2.X;
                section.Pos.Y = r1.Y;
                section.Size.X = r2.X - i2.X;
                section.Size.Y = i1.Y - r1.Y;
                
                results.Add(section);
            }
                
            return true;
        }
        
        return false;
    }
    
    
    

}
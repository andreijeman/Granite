using Granite.Engine.Primitives;

namespace Granite.Engine.Tests.Primitives;

public class RectTests
{
    [Theory]
    [InlineData(0, 0, true)]   // top-left corner
    [InlineData(10, 10, true)] // bottom-right corner
    [InlineData(5, 5, true)]   // inside
    [InlineData(-1, 5, false)] // left of rect
    [InlineData(11, 5, false)] // right of rect
    [InlineData(5, -1, false)] // above rect
    [InlineData(5, 11, false)] // below rect
    public void Contains_ReturnsExpectedResult(int x, int y, bool expected)
    {
        var rect = new Rect(new Point(0, 0), new Point(10, 10));
 
        var result = rect.Contains(new Point(x, y));
 
        Assert.Equal(expected, result);
    }
 
    [Fact]
    public void Intersects_OverlappingRects_ReturnsTrue()
    {
        var a = new Rect(new Point(0, 0), new Point(10, 10));
        var b = new Rect(new Point(5, 5), new Point(15, 15));
 
        Assert.True(a.Intersects(b));
        Assert.True(b.Intersects(a));
    }
 
    [Fact]
    public void Intersects_TouchingEdges_ReturnsTrue()
    {
        var a = new Rect(new Point(0, 0), new Point(10, 10));
        var b = new Rect(new Point(10, 0), new Point(20, 10));
 
        Assert.True(a.Intersects(b));
    }
 
    [Fact]
    public void Intersects_NonOverlappingRects_ReturnsFalse()
    {
        var a = new Rect(new Point(0, 0), new Point(10, 10));
        var b = new Rect(new Point(20, 20), new Point(30, 30));
 
        Assert.False(a.Intersects(b));
        Assert.False(b.Intersects(a));
    }
 
    [Fact]
    public void TryGetIntersection_OverlappingRects_ReturnsTrueAndCorrectIntersection()
    {
        var a = new Rect(new Point(0, 0), new Point(10, 10));
        var b = new Rect(new Point(5, 5), new Point(15, 15));
 
        var result = a.TryGetIntersection(b, out var intersection);
 
        Assert.True(result);
        Assert.Equal(new Point(5, 5), intersection.P1);
        Assert.Equal(new Point(10, 10), intersection.P2);
    }
 
    [Fact]
    public void TryGetIntersection_NonOverlappingRects_ReturnsFalseAndDefaultOut()
    {
        var a = new Rect(new Point(0, 0), new Point(10, 10));
        var b = new Rect(new Point(20, 20), new Point(30, 30));
 
        var result = a.TryGetIntersection(b, out var intersection);
 
        Assert.False(result);
        Assert.Equal(default, intersection);
    }
 
    [Fact]
    public void TryGetIntersection_TouchingEdge_ReturnsTrueWithZeroWidthOrHeightRect()
    {
        var a = new Rect(new Point(0, 0), new Point(10, 10));
        var b = new Rect(new Point(10, 0), new Point(20, 10));
 
        var result = a.TryGetIntersection(b, out var intersection);
 
        Assert.True(result);
        Assert.Equal(new Point(10, 0), intersection.P1);
        Assert.Equal(new Point(10, 10), intersection.P2);
    }
 
    [Fact]
    public void Subtract_NoIntersection_ReturnsOriginalRectUnchanged()
    {
        var a = new Rect(new Point(0, 0), new Point(10, 10));
        var b = new Rect(new Point(20, 20), new Point(30, 30));
 
        var result = a.Subtract(b).ToList();
 
        var single = Assert.Single(result);
        Assert.Equal(a, single);
    }
 
    [Fact]
    public void Subtract_OtherFullyCoversThis_ReturnsEmpty()
    {
        var a = new Rect(new Point(2, 2), new Point(8, 8));
        var b = new Rect(new Point(0, 0), new Point(10, 10));
 
        var result = a.Subtract(b).ToList();
 
        Assert.Empty(result);
    }
 
    [Fact]
    public void Subtract_CenteredHole_ReturnsFourSurroundingParts()
    {
        var a = new Rect(new Point(0, 0), new Point(9, 9));
        var b = new Rect(new Point(3, 3), new Point(6, 6));
 
        var result = a.Subtract(b).ToList();
 
        Assert.Equal(4, result.Count);
 
        // Top part: full width, above the hole
        Assert.Contains(result, r => r.P1 == new Point(0, 0) && r.P2 == new Point(9, 2));
        // Bottom part: full width, below the hole
        Assert.Contains(result, r => r.P1 == new Point(0, 7) && r.P2 == new Point(9, 9));
        // Left part: constrained to the hole's Y-range, left of it
        Assert.Contains(result, r => r.P1 == new Point(0, 3) && r.P2 == new Point(2, 6));
        // Right part: constrained to the hole's Y-range, right of it
        Assert.Contains(result, r => r.P1 == new Point(7, 3) && r.P2 == new Point(9, 6));
    }
 
    [Fact]
    public void Subtract_OverlapAlongOneEdgeOnly_ReturnsSinglePart()
    {
        var a = new Rect(new Point(0, 0), new Point(9, 9));
        var b = new Rect(new Point(5, 0), new Point(15, 9));
 
        var result = a.Subtract(b).ToList();
 
        var single = Assert.Single(result);
        Assert.Equal(new Point(0, 0), single.P1);
        Assert.Equal(new Point(4, 9), single.P2);
    }
 
    [Fact]
    public void Translate_MovesBothPointsByOffset()
    {
        var rect = new Rect(new Point(1, 1), new Point(5, 5));
        var offset = new Point(3, -2);
 
        var result = rect.Translate(offset);
 
        Assert.Equal(new Point(4, -1), result.P1);
        Assert.Equal(new Point(8, 3), result.P2);
    }
 
    [Fact]
    public void ChangeOrigin_RebasesBothPoints()
    {
        var rect = new Rect(new Point(5, 5), new Point(10, 10));
        var oldOrigin = new Point(0, 0);
        var newOrigin = new Point(2, 2);
 
        var result = rect.Rebase(oldOrigin, newOrigin);
 
        Assert.Equal(new Point(7, 7), result.P1);
        Assert.Equal(new Point(12, 12), result.P2);
    }
 
    [Fact]
    public void RecordStruct_ValueEquality_WorksAsExpected()
    {
        var a = new Rect(new Point(0, 0), new Point(5, 5));
        var b = new Rect(new Point(0, 0), new Point(5, 5));
        var c = new Rect(new Point(0, 0), new Point(6, 6));
 
        Assert.Equal(a, b);
        Assert.NotEqual(a, c);
        Assert.True(a == b);
        Assert.False(a == c);
    }
}
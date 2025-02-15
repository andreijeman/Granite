namespace Granite.Entities;

public interface IInteractive
{
    public void Focus();
    public void Unfocus();
    public void ProcessPressedKey(ConsoleKey key);
}

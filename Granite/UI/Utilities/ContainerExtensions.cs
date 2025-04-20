using Granite.IO;
using Container = Granite.UI.Entities.Container;

namespace Granite.UI.Utilities;

public static class ContainerExtensions
{
    public static void BindToIO(this Container container)
    {
        ConsoleOutput.Bind(container.Frame);
        KeyboardInput.Bind(container.CtrlHolder); 
    }
}
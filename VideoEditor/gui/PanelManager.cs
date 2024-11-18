using OpenTK.Mathematics;
using VideoEditor.input;

namespace VideoEditor.gui;

public static class PanelManager
{
    private static Panel? _root;

    public static void Init()
    {
        Mouse.Down += _ =>
        {
            PanelManager.Drag(Mouse.Position);
            Mouse.Drag += PanelManager.Drag;
        };

        Mouse.Up += _ => { Mouse.Drag -= PanelManager.Drag; };

        PanelManager._root = new Split(
            new Split(
                new View(),
                new Split(
                    new View(),
                    new View(),
                    true
                    )
            ),
            new View(),
            true
        );

        ((Split)PanelManager._root).Layout();
        PanelManager._root.Ready();

    }

    private static void Drag(Vector2 position)
    {
        float newY = position.Y / Window.CurrentWindow!.ClientSize.Y * -2 + 1;
    }

    public static void Render()
    {
        PanelManager._root!.Render();
    }
}
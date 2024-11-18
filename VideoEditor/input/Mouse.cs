using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace VideoEditor.input;

public static class Mouse
{
    public delegate void DragEvent(Vector2 pos);

    private static float _x;
    private static float _y;

    private static bool _dragging;

    public static Vector2 Position => new(Mouse._x, Mouse._y);
    public static float X => Mouse._x;
    public static float Y => Mouse._y;

    public static bool Dragging => Mouse._dragging;

    public static event DragEvent Drag = _ => { };

    public static event Action<MouseMoveEventArgs> Move = e =>
    {
        Mouse._x = e.X;
        Mouse._y = e.Y;

        if (Mouse._dragging) Mouse.Drag(Mouse.Position);
    };

    public static event Action<MouseButtonEventArgs> Down = _ => { Mouse._dragging = true; };

    public static event Action<MouseButtonEventArgs> Up = _ => { Mouse._dragging = false; };

    public static void RunMove(MouseMoveEventArgs e)
    {
        Mouse.Move(e);
    }

    public static void RunDown(MouseButtonEventArgs e)
    {
        Mouse.Down(e);
    }

    public static void RunUp(MouseButtonEventArgs e)
    {
        Mouse.Up(e);
    }

    public static Vector2 NormalizedToScreen()
    {
        // Scale to -1 -> 1 and flip Y axis
        return (Mouse.Position / Window.CurrentWindow!.ClientSize * 2 - (1, 1)) * (1, -1);
    }
}
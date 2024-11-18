using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using VideoEditor.input;
using VideoEditor.rendering;

namespace VideoEditor.gui;

public class Handle
{
    private readonly Quad _quad;

    public Handle(Split parent)
    {
        Window.CurrentWindow!.TryGetCurrentMonitorDpiRaw(out float xScale, out float yScale);

        Console.WriteLine(xScale);
        Console.WriteLine(yScale);

        float yPixel = 1f / yScale;
        float xPixel = 1f / xScale;

        this._quad = new()
        {
            Top = yPixel * 5,
            Bottom = -yPixel * 5,
            Left = -xPixel * 5,
            Right = xPixel * 5,
        };

        this._quad.MoveTo(parent.quad.Center - (this._quad.Width / 2, -this._quad.Height / 2));
        Console.WriteLine(parent.quad.Center);

        this.OnDown(_ =>
        {
            parent.Drag();
            Mouse.Move += parent.Drag;

            Action<MouseButtonEventArgs> upCallback = null;
            
            upCallback = _ =>
            {
                Mouse.Move -= parent.Drag;
                Mouse.Up -= upCallback;
            };

            Mouse.Up += upCallback;
        });
    }

    public void Render()
    {
        this._quad.Render();
    }

    private void OnDown(Action<MouseButtonEventArgs> callback)
    {
        Mouse.Down += e =>
        {
            Vector2 norm = Mouse.NormalizedToScreen();
            if (this._quad.Contains(norm))
            {
                callback(e);
            }
        };
    }
    
    private void OnUp(Action<MouseButtonEventArgs> callback)
    {
        Mouse.Up += e =>
        {
            Vector2 norm = Mouse.NormalizedToScreen();
            if (this._quad.Contains(norm))
            {
                callback(e);
            }
        };
    }
}


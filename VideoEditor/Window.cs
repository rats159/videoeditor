using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using VideoEditor.gui;
using VideoEditor.input;

namespace VideoEditor;

public class Window
    : GameWindow
{
    private Shader? _shader;

    public Window() : base(GameWindowSettings.Default, new() { ClientSize = (1280, 720), Title = "VideoEditor" })
    {
        Window.CurrentWindow = this;
    }

    public static Window? CurrentWindow { get; private set; }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);

        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GLFW.SwapInterval(1);
        GL.ClearColor(1, 1, 1, 1);
        this._shader = new("../../../shaders/panel.vert", "../../../shaders/panel.frag");
        PanelManager.Init();
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);
        Mouse.RunMove(e);
    }

    protected override void OnMouseDown(MouseButtonEventArgs e)
    {
        base.OnMouseDown(e);
        Mouse.RunDown(e);
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        base.OnMouseUp(e);
        Mouse.RunUp(e);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        this._shader!.Use();

        PanelManager.Render();

        this.SwapBuffers();
    }
}
using OpenTK.Graphics.OpenGL;
using VideoEditor.rendering;

namespace VideoEditor.gui;

public class View : Panel
{
    public Panel container;
    
    public View()
    {
        this.quad = new();
    }
    

    public override void Render()
    {
        float top = this.container.quad.Top + this.quad.Top * this.container.quad.Height;
        float right = this.container.quad.Right - this.quad.Right * this.container.quad.Width;
        float bottom = this.container.quad.Bottom - this.quad.Bottom * this.container.quad.Height;
        float left = this.container.quad.Left + this.quad.Left * this.container.quad.Width;
        
        Quad absoluteQuad = new()
        {
            Top = top,
            Bottom = bottom,
            Left = left,
            Right = right
        };
        absoluteQuad.Render();
    }
}
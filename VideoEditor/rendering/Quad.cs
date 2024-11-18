using System.Collections;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace VideoEditor.rendering;

public class Quad
{
    private readonly int _positionVboId = GL.GenBuffer();
    private readonly int _vaoId;

    public Quad()
    {
        this._vaoId = GL.GenVertexArray();

        GL.BindVertexArray(this._vaoId);

        int eboId = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboId);
        GL.BufferData(
            BufferTarget.ElementArrayBuffer,
            this._elements.Length * sizeof(uint),
            this._elements,
            BufferUsageHint.StaticDraw
        );

        float[] uvs = this._uv;
        int ubVboId = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, ubVboId);
        GL.BufferData(BufferTarget.ArrayBuffer, uvs.Length * sizeof(float), uvs,
            BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);

        GL.BindVertexArray(0);
    }

    ~Quad()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
        GL.DeleteBuffer(this._positionVboId);
        GL.DeleteVertexArray(this._vaoId);
    }

    public void Render()
    {
        this.Prepare();
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        this.Finish();
    }

    private void Prepare()
    {
        GL.BindVertexArray(this._vaoId);

        GL.BindBuffer(BufferTarget.ArrayBuffer, this._positionVboId);
        GL.BufferData(BufferTarget.ArrayBuffer, 8 * sizeof(float), this.Vertices,
            BufferUsageHint.DynamicDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 0, 0);

        GL.EnableVertexAttribArray(0);
        GL.EnableVertexAttribArray(1);
    }

    private void Finish()
    {
        GL.DisableVertexAttribArray(0);
        GL.DisableVertexAttribArray(1);

        GL.BindVertexArray(0);
    }

    public bool Contains(Vector2 point)
    {
        return point.X > this.Left && point.X < this.Right && point.Y > this.Bottom && point.Y < this.Top;
    }

    public float Bottom { get; set; } = -1;

    public float Top { get; set; } = 1;

    public float Left { get; set; } = -1;

    public float Right { get; set; } = 1;

    public float Height => this.Top - this.Bottom;

    public float Width => this.Right - this.Left;

    private float[] Vertices =>
    [
        this.Left, this.Top,
        this.Left, this.Bottom,
        this.Right, this.Bottom,
        this.Right, this.Top,
    ];

    public Vector2 Center => new((this.Top + this.Bottom) / 2, (this.Left + this.Right) / 2);

    public Quad Copy()
    {
        return new()
        {
            Top = this.Top,
            Right = this.Right,
            Bottom = this.Bottom,
            Left = this.Left,
        };
    }

    private readonly float[] _uv =
    [
        0, 1,
        0, 0,
        1, 0,
        1, 1
    ];

    private readonly int[] _elements =
    [
        0, 1, 3,
        3, 1, 2
    ];

    public void MoveTo(Vector2 vec)
    {
        this.MoveTo(vec.X,vec.Y);
    }
    
    public void MoveTo(float x, float y)
    {
        float initialWidth = this.Width;
        float initialHeight = this.Height;
        this.Top = y;
        this.Left = x;
        this.Bottom = this.Top - initialHeight;
        this.Right = this.Left + initialWidth;
    }
}
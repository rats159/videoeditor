using OpenTK.Graphics.OpenGL;

namespace VideoEditor;

public sealed class Shader : IDisposable
{
    private readonly int _handle;
    private bool _disposedValue;

    public Shader(string vertexPath, string fragmentPath)
    {
        string vertexSource = File.ReadAllText(vertexPath);
        string fragmentSource = File.ReadAllText(fragmentPath);

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexSource);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentSource);

        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(vertexShader);
            Console.Error.WriteLine(infoLog);
        }

        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
        if (success == 0)
        {
            string infoLog = GL.GetShaderInfoLog(fragmentShader);
            Console.Error.WriteLine(infoLog);
        }

        this._handle = GL.CreateProgram();

        GL.AttachShader(this._handle, vertexShader);
        GL.AttachShader(this._handle, fragmentShader);

        GL.LinkProgram(this._handle);

        GL.GetProgram(this._handle, GetProgramParameterName.LinkStatus, out success);

        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(this._handle);
            Console.WriteLine(infoLog);
        }

        GL.DetachShader(this._handle, vertexShader);
        GL.DetachShader(this._handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }


    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Use()
    {
        GL.UseProgram(this._handle);
    }

    private void Dispose(bool disposing)
    {
        if (this._disposedValue) return;
        GL.DeleteProgram(this._handle);

        this._disposedValue = true;
    }

    ~Shader()
    {
        if (this._disposedValue == false) Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
    }
}
using VideoEditor.rendering;

namespace VideoEditor.gui;

public abstract class Panel
{
    public Quad quad = new();

    public abstract void Render();

    public virtual void Ready()
    {
    }

}
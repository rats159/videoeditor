using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using VideoEditor.input;

namespace VideoEditor.gui;

public class Split : Panel
{
    private Panel First { get; }
    private Panel Second { get; }

    private Handle _handle;
    private readonly bool _vertical;

    public Split(Panel first, Panel second, bool vertical = false)
    {
        this._vertical = vertical;
        this.First = first;
        this.Second = second;
    }


    public override void Render()
    {
        this.First.Render();
        this.Second.Render();
        this._handle.Render();
    }

    public void Layout()
    {
        this.First.quad = new();
        this.Second.quad = new();
        
        if (this._vertical)
        {
            this.First.quad.Bottom = 0f;
            this.Second.quad.Top = 0f;
        }
        else
        {
            this.First.quad.Right = 0f;
            this.Second.quad.Left = 0f;
        }
        
        if(this.First is Split splitFirst)
        {
            splitFirst.Layout();
        }
        if(this.Second is Split splitSecond)
        {
            splitSecond.Layout();
        }

        if (this.First is View viewFirst) viewFirst.container = this;
        if (this.Second is View viewSecond) viewSecond.container = this;
        
        this.First.Ready();
        this.Second.Ready();
    }

    public override void Ready()
    {
        this._handle = new(this);
    }
    
    public void Drag(MouseMoveEventArgs _)
    {
        this.Drag();
    }

    public void Drag()
    {
        if (this._vertical)
        {
            this.VerticalDrag();
        }
        else
        {
            this.HorizontalDrag();
        }
    }
    
    private void VerticalDrag()
    {
        Vector2 normalized = Mouse.NormalizedToScreen();
        this.First.quad.Bottom = normalized.Y; 
        this.Second.quad.Top = normalized.Y;
    }

    private void HorizontalDrag()
    {
        
    }
    


}
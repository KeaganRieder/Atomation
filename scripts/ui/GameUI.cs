namespace Atomation.Ui;

using Atomation.Resources;
using Godot;

public abstract partial  class GameUI : Control
{
    public bool IsOpen { get; private set; }

    protected GameUI(){
        ZIndex = VisualLayer.UI;
    }

    public void Open()
    {
        Visible = true;
        IsOpen = true;
    }

    public void Close()
    {
        Visible = false;
        IsOpen = false;
    }
    public virtual void SetAnchor(LayoutPreset preset)
    {
        SetAnchorsAndOffsetsPreset(preset);
    }

}
namespace Atomation.Ui;

using Godot;

public partial class UserInterface : Control
{
    protected PanelContainer background;

    protected bool isOpen;

    protected int uiPadding = 5;

    protected UserInterface(Node parent = null, int gameLayer = GameLayers.Ui)
    {
        ZIndex = gameLayer;

        if (parent != null)
        {
            parent.AddChild(this);
        }

        FormatUserInterface();

        isOpen = false;
        Visible = isOpen;
    }

    public bool IsOpen { get => isOpen; set => isOpen = value; }

    /// <summary>
    /// function used to format/create a ui
    /// </summary>
    protected virtual void FormatUserInterface()
    {
        GD.Print("method to format Ui is not implemented");

    }

    /// <summary>
    /// toggles wether the ui is shown or not
    /// </summary>
    public virtual void ToggleUI()
    {
        isOpen = !isOpen;
        Visible = isOpen;
    }

}
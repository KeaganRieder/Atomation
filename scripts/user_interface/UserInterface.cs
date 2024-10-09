namespace Atomation.Ui;

using System;
using Godot;

public partial class UserInterface : Control
{
    protected PanelContainer background;

    protected bool isOpen;

    protected int uiPadding = 5;
    protected LayoutPreset layoutPreset;

    protected UserInterface(Node parent = null, int gameLayer = GameLayers.Ui, int uiPadding = 5)
    {
        this.uiPadding = uiPadding;
        ZIndex = gameLayer;

        if (parent != null)
        {
            parent.AddChild(this);
        }
    }

    public bool IsOpen { get => isOpen; set => isOpen = value; }

    /// <summary>
    /// used to either hide or show the user interface
    /// </summary>
    public virtual void ToggleUI()
    {
        isOpen = !isOpen;
        Visible = isOpen;
    }

    /// <summary>
    /// sets the anchors for the ui 
    /// </summary>
    public virtual void SetAnchor(LayoutPreset preset)
    {
        layoutPreset = preset;
        background.SetAnchorsAndOffsetsPreset(layoutPreset);
    }

    /// <summary>
    /// sets the anchors for the ui to the current one
    /// </summary>
    public virtual void SetToCurrentAnchor()
    {
        background.SetAnchorsAndOffsetsPreset(layoutPreset);
    }

    /// <summary>
    /// creates and formats the user interface
    /// </summary>
    protected virtual void CreateUIElements()
    {
        background.LayoutMode = 1;
        isOpen = false;
        Visible = isOpen;
    }

    /// <summary>
    /// creates a button ui element that does the given action
    /// when pressed
    /// </summary>
    protected Button CreateButton(string name, Action buttonAction)
    {
        Button button = new Button() { Text = name };
        button.Pressed += buttonAction;

        return button;
    }

    /// <summary>
    /// creates a horizontal box container ui element with specified
    /// dimensions
    /// </summary>
    protected HBoxContainer CreateHBox(string name, Vector2 minimumSize)
    {
        var hBox = new HBoxContainer();

        hBox.AddChild(new Label
        {
            CustomMinimumSize = minimumSize,
            Text = name
        });

        return hBox;
    }

    
}
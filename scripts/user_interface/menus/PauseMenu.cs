namespace Atomation.Ui;

using System;
using Godot;

public partial class PauseMenu : UserInterface
{
    private bool paused;
    private VBoxContainer container;

    private SettingsMenu settingsMenu;

    public PauseMenu(Node parent = null, int gameLayer = GameLayers.Ui)
    : base(parent, gameLayer)
    {
        Name = "pause Menu";
        ProcessMode = ProcessModeEnum.Always;

        settingsMenu = new SettingsMenu(this, parent);

        paused = false;

        CreateUIElements();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("Pause"))
        {
            togglePause();
        }
        base._Input(@event);
    }

    protected override void CreateUIElements()
    {
        background = new PanelContainer()
        {
            CustomMinimumSize = new Vector2(100, 100),
        };

        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(uiPadding);

        container = new();
        marginContainer.AddChild(container);

        background.AddChild(marginContainer);

        container.AddChild(CreateButton("Resume",() => togglePause()));

        container.AddChild(CreateButton("Settings",() =>
        {
            base.ToggleUI();

            settingsMenu.ToggleUI();
        }));
        
        container.AddChild(CreateButton("Save Game",()=>  GD.Print("saving ui Not implemented")));
        container.AddChild(CreateButton("Load Game", () =>  GD.Print("Loading ui Not implemented")));
        container.AddChild(CreateButton("Quit",() =>  GetTree().Quit()));

        background.SetAnchorsAndOffsetsPreset(LayoutPreset.Center);
        AddChild(background);

        base.CreateUIElements();
    }

    public void togglePause()
    {
        if (!settingsMenu.IsOpen)
        {
            base.ToggleUI();
            paused = !paused;

            if (paused)
            {
                GetTree().Paused = true;
            }
            else
            {
                GetTree().Paused = false;
            }
        }
        else if (settingsMenu.IsOpen)
        {
            base.ToggleUI();

            settingsMenu.ToggleUI();
        }
    }

}
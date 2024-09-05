namespace Atomation.Ui;

using Godot;

public partial class SettingsMenu : UserInterface
{
    private VBoxContainer container;

    private PauseMenu pauseMenu;

    public SettingsMenu(PauseMenu menu, Node parent = null, int gameLayer = GameLayers.Ui) :
    base(parent, gameLayer)
    {
        Name = "SettingsMenu";
        ProcessMode = ProcessModeEnum.Always;
        this.pauseMenu = menu;
    }

    public override void ToggleUI()
    {
        base.ToggleUI();
    }

    protected override void FormatUserInterface()
    {
        background = new PanelContainer()
        {
            CustomMinimumSize = new Vector2(100, 100)
        };

        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(uiPadding);

        container = new();
        marginContainer.AddChild(container);

        background.AddChild(marginContainer);
        AddChild(background);

        Button generalButton = new Button { Text = "General" };
        generalButton.Pressed += OnGeneralPressed;
        container.AddChild(generalButton);

        Button controlsButton = new Button { Text = "Controls" };
        controlsButton.Pressed += OnControlsPressed;
        container.AddChild(controlsButton);

        Button backButton = new Button { Text = "Back" };
        backButton.Pressed += OnBackPressed;
        container.AddChild(backButton);

        background.SetAnchorsAndOffsetsPreset(LayoutPreset.Center);

    }

    /// <summary>
    /// 
    /// </summary>
    private void OnGeneralPressed()
    {
        GD.Print("general settings button not implemented");
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnControlsPressed()
    {
        GD.Print("control settings button not implemented");
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnBackPressed()
    {
        ToggleUI();
        pauseMenu.ToggleUI();

    }
}
namespace Atomation.Ui;

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
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("Pause"))
        {
            togglePause();
        }
        base._Input(@event);
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

        Button resumeButton = new Button { Text = "Resume" };
        resumeButton.Pressed += OnResumePressed;
        container.AddChild(resumeButton);

        Button settingsButton = new Button { Text = "Settings" };
        settingsButton.Pressed += OnSettingsPressed;
        container.AddChild(settingsButton);

        Button saveButton = new Button { Text = "Save Game" };
        saveButton.Pressed += OnSavePressed;
        container.AddChild(saveButton);

        Button loadButton = new Button { Text = "Load Game" };
        loadButton.Pressed += OnLoadPressed;
        container.AddChild(loadButton);

        Button quitButton = new Button { Text = "Quit" };
        quitButton.Pressed += OnQuitPressed;
        container.AddChild(quitButton);

        background.SetAnchorsAndOffsetsPreset(LayoutPreset.Center);
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
            OnSettingsPressed();
        }
    }

    /// <summary>
    /// close this menu and un pauses game
    /// </summary>
    private void OnResumePressed()
    {
        togglePause();
    }

    /// <summary>
    /// opens settings menu
    /// </summary>
    private void OnSettingsPressed()
    {
        base.ToggleUI();

        settingsMenu.ToggleUI();
    }

    /// <summary>
    /// opens save menu
    /// </summary>
    private void OnSavePressed()
    {
        GD.Print("saving ui Not implemented");
    }

    /// <summary>
    /// opens load menu
    /// </summary>
    private void OnLoadPressed()
    {
        GD.Print("Loading ui Not implemented");
    }

    /// <summary>
    /// quits (close application) the game
    /// </summary>
    private void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
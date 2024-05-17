namespace Atomation.Ui;

using Godot;

public partial class PauseMenu : GameUI
{
    private static PauseMenu instance;
    public static PauseMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PauseMenu();
            }
            return instance;
        }
    }

    private int padding;

    private PanelContainer background;
    private GridContainer buttonContainer;

    private OptionsMenu optionsMenu;

    private Label gameTitle;

    private Button resumeButton;
    private Button saveButton;
    private Button loadButton;
    private Button optionsButton;
    private Button quitButton;

    private PauseMenu() : base()
    {
        Name = "PauseMenu";
        padding = 5;

        optionsMenu = new OptionsMenu(this);
        background = new PanelContainer(){Name = "Menu BackGround"};
        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(padding);
    
        buttonContainer = new GridContainer() { Columns = 1 };
        marginContainer.AddChild(buttonContainer);
        background.AddChild(marginContainer);

        gameTitle = new Label() {
            CustomMinimumSize = new Vector2(25,16),
            Text = "Atomation",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Position = new Vector2(0,0),
            ZIndex = 1,
        };
        buttonContainer.AddChild(gameTitle);

        resumeButton = new Button() { Text = "Resume Game", };
        buttonContainer.AddChild(resumeButton);
        resumeButton.Pressed += ResumeGame;

        saveButton = new Button() { Text = "Save Game", };
        buttonContainer.AddChild(saveButton);
        saveButton.Pressed += SaveGame;

        loadButton = new Button() { Text = "Load Game", };
        buttonContainer.AddChild(loadButton);
        loadButton.Pressed += LoadGame;

        optionsButton = new Button() { Text = "Options", };
        buttonContainer.AddChild(optionsButton);
        optionsButton.Pressed += OptionsMenu;

        quitButton = new Button() { Text = "Quit Game", };
        buttonContainer.AddChild(quitButton);
        quitButton.Pressed += PromptQuitGame;

        AddChild(background);
        SetAnchor(LayoutPreset.Center);

        Close();
    }

    private void ResumeGame()
    {
        Close();
    }

    private void SaveGame()
    {
        GD.Print("save");
    }

    private void LoadGame()
    {
        GD.Print("load");
    }

    private void OptionsMenu()
    {
        GD.Print("options");
        optionsMenu.Open();
        background.Visible = false;
    }

    private void PromptQuitGame()
    {
        GD.Print("quit");
    }

}
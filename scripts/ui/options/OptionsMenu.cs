namespace Atomation.Ui;

using Godot;

public partial class OptionsMenu : GameUI
{
    private PauseMenu pauseMenu;

    private PanelContainer background;
    private TabContainer optionTabs;

    private RebindControls rebindControls;

    private Button backButton;
    private Button confirmButton;

    public OptionsMenu(PauseMenu pauseMenu)
    {
        this.pauseMenu = pauseMenu;
        Name = "Options Menu";
        background = new PanelContainer();
        optionTabs = new TabContainer();

        rebindControls = new RebindControls();
        optionTabs.AddChild(rebindControls);

        // backButton = new Button() { Text = "Back", };
        // background.AddChild(backButton);
        // backButton.Pressed += Back;

        // confirmButton = new Button() { Text = "Confirm", };
        // background.AddChild(confirmButton);
        // confirmButton.Pressed += Confirm;

        background.AddChild(optionTabs);
        AddChild(background);
        pauseMenu.AddChild(this);
        Close();
    }

    private void Back() { 
        Close();
        pauseMenu.Open();
    }

    private void Confirm()
    {
        Close();
        pauseMenu.Open();
    }
}
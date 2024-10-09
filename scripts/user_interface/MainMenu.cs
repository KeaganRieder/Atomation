namespace Atomation.Ui;

using Atomation.GameMap;
using Godot;

public partial class MainMenu : UserInterface
{
    private WorldSettingsUi worldSettingsUi;

    public MainMenu()
    {
        Name = "Main Menu";

        CreateUIElements();
    }

    public override void _Ready()
    {
        Size = DisplayServer.WindowGetSize();
        Position = Size / -2;
        // background.Size = Size;

        worldSettingsUi.Size = Size ;

        base._Ready();
    }

    protected override void CreateUIElements()
    {
        // background = new PanelContainer();
        worldSettingsUi = new WorldSettingsUi();

        // AddChild(background);
        AddChild(worldSettingsUi);
    }



    //todo other stuff for main menu
}
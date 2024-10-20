namespace Atomation.Ui;

using Godot;

public partial class SettingsMenu : UserInterface
{
    private VBoxContainer container;

    private PauseMenu pauseMenu;
    private bool fromMainMenu;//maybe

    public SettingsMenu(PauseMenu menu, Node parent = null, int gameLayer = GameLayers.Ui) :
    base(parent, gameLayer)
    {
        Name = "SettingsMenu";
        ProcessMode = ProcessModeEnum.Always;
        this.pauseMenu = menu;
    }

    protected override void CreateUIElements()
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

        container.AddChild(CreateButton("General",() => GD.Print("general settings button not implemented")));

        container.AddChild(CreateButton("Controls",() => GD.Print("control settings button not implemented")));

        container.AddChild(CreateButton("Back", () =>
        {
            ToggleUI();
            pauseMenu.ToggleUI();
        }));

        background.SetAnchorsAndOffsetsPreset(LayoutPreset.Center);
        base.CreateUIElements();
    }


}
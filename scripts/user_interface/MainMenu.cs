namespace Atomation.Ui;

using Atomation.GameMap;
using Godot;

public partial class MainMenu : UserInterface
{
    private WorldSettingsUi worldSettingsUi;
    private PanelContainer buttonContainer;

    public MainMenu() : base()
    {
        Name = "Main Menu";
        background = new PanelContainer();
        AddChild(background);

        OffWhenCreated = false;
    }

    protected override void CreateUIElements()
    {
        worldSettingsUi = new WorldSettingsUi();
        AddChild(worldSettingsUi);

        Size = DisplayServer.WindowGetSize();
        Position = Size / -2;

        background.Size = Size;
        worldSettingsUi.Size = Size;

        CreateMenuButtons();

        base.CreateUIElements();
    }

    private void CreateMenuButtons()
    {
        buttonContainer = new PanelContainer();
        VBoxContainer vBox = new VBoxContainer();

        var margins = new MarginContainer();
        margins.AddMargin(5);
        buttonContainer.AddChild(margins);
        margins.AddChild(vBox);

        // todo make a continue button that only appears if save is present

        vBox.AddChild(CreateButton("New Game", () =>
        {
            ToggleUI();
            worldSettingsUi.ToggleUI();
        }));

        vBox.AddChild(CreateButton("Settings", () =>
        {
            GD.Print("settings ui Not implemented");
            // base.ToggleUI();

            // settingsMenu.ToggleUI(); todo
        }));

        vBox.AddChild(CreateButton("Save Game", () => GD.Print("saving ui Not implemented")));
        vBox.AddChild(CreateButton("Load Game", () => GD.Print("Loading ui Not implemented")));
        vBox.AddChild(CreateButton("Quit", () => GetTree().Quit()));

        margins = new MarginContainer();
        margins.AddMargin(20);
        margins.AddChild(buttonContainer);
        AddChild(margins);

        margins.LayoutMode = 1;
        margins.SetAnchorsAndOffsetsPreset(LayoutPreset.BottomRight);
    }

    public override void ToggleUI()
    {
        isOpen = !isOpen;

        buttonContainer.Visible = isOpen;
        background.Visible = isOpen;
    }
}
namespace Atomation.Controls;

using Atomation.PlayerChar;
using Atomation.Resources;
using Godot;

public partial class GameController : Node2D
{
    private SaveHandler SaveHandler;
    private PlayerControls playerControls;
    private WorldControls worldControls;
    private GameBindings gameBindings;

    public GameController(Player player)
    {
        gameBindings = new GameBindings();

        SaveHandler = SaveHandler.Instance;
        playerControls = new PlayerControls(player);
        worldControls = new WorldControls(this);

        player.AddChild(this);
    }

    public override void _Input(InputEvent input)
    {
        base._Input(input);
        playerControls.HandleInput(input);

        worldControls.HandleInput(input);

        if (input.IsActionPressed("QuickSave"))
        {
            SaveHandler.QuickSave();

        }
        if (input.IsActionPressed("QuickLoad"))
        {
            SaveHandler.QuickLoad();
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        //todo stuff here

    }  
}
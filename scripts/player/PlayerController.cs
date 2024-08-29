namespace Atomation.Player;

using Atomation.Settings;
using Godot;

/// <summary>
/// handles receiving inputs made by the player. which it then process and decide
/// what to todo with it. either handling the input itself or
/// passing to another class depending on the inputs complexity/purpose
/// </summary>
public partial class PlayerController : Node
{
    private PlayerCharacter player;

    public PlayerController(PlayerCharacter player)
    {
        Name = "PlayerController";
        this.player = player;
        player.AddChild(this);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);
        HandleMovementInput();
        HandleCameraInput();
        HandleUiInput();
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        base._UnhandledInput(inputEvent);
    }

    private void HandleMovementInput()
    {
        if (player == null)
        {
            GD.PushWarning("Player character not assigned to player controller");
            return;
        }
        Vector2 inputDirection = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
        player.Velocity = inputDirection * player.StatSheet.GetStat("moveSpeed").CurrentValue;
    }

    private void HandleCameraInput(){
        if (Input.IsActionPressed("ZoomIn"))
        {
            player.Camera.ZoomIn();
        }
        if (Input.IsActionPressed("ZoomOut"))
        {
            player.Camera.ZoomOut();
        }
    }

    private void HandleUiInput(){
        if (Input.IsActionPressed("Pause"))
        {
            GD.Print("Pause");
        }
        if (Input.IsActionPressed("Inventory"))
        {
            GD.Print("inventory");
            
        }
        if (Input.IsActionPressed("QuickSave"))
        {
            GD.Print("save");
            
        }
        if (Input.IsActionPressed("QuickLoad"))
        {
            GD.Print("load");
            
        }
    }
}
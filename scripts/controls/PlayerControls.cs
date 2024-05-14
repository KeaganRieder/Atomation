namespace Atomation.Controls;

using Atomation.PlayerChar;
using Godot;

public class PlayerControls
{
    private Player player;

    public PlayerControls(Player player)
    {
        this.player = player;
    }

    public void HandleInput(InputEvent input)
    {
        if (input.IsAction("MoveLeft") || input.IsAction("MoveRight") || input.IsAction("MoveUp") || input.IsAction("MoveDown"))
        {
            player.Move();
        }
        CameraControls(input);
        Inventory(input);
    }

    private void CameraControls(InputEvent input)
    {
        if (input.IsActionPressed("ZoomIn"))
        {
            player.GetCamera().ZoomIn();
        }
        if (input.IsActionPressed("ZoomOut"))
        {
            player.GetCamera().ZoomOut();
        }
    }

    private void Inventory(InputEvent input)
    {
        if (input.IsActionPressed("Inventory"))
        {
            if (player.GetInventory().IsOpen)
            {
                player.GetInventory().Close();
            }
            else
            {
                player.GetInventory().Open();
            }
        }
    }

}
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

    public void HandleInput(InputEvent input){
        if (input.IsAction("MoveLeft") || input.IsAction("MoveRight") || input.IsAction("MoveUp") || input.IsAction("MoveDown"))
        {
            player.Move();
        }
        CameraControls(input);
    }
    
    private void CameraControls(InputEvent input){
        //zoom in/out todo
    }

}
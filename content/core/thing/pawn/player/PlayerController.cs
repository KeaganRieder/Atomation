using Godot;

/// <summary>
/// handles the input by the player for the current 
/// node object 
/// </summary>
public partial class PlayerController : Node
{

    // public InputMap inputMap;
    public override void _Input(InputEvent inputEvent)
    {
        // InputMap
        base._Input(inputEvent);
        MovementEvents(inputEvent);
    }

    public void MovementEvents(InputEvent inputEvent){
        //left 
        if (inputEvent.IsActionPressed("MoveLeft"))
        {
            
        }
        //right 
        if (inputEvent.IsActionPressed("MoveRight"))
        {
            
        }
        //forward
        if (inputEvent.IsActionPressed("MoveUp"))
        {
            
        }
        //backward
        if (inputEvent.IsActionPressed("MoveDown"))
        {
            
        }
    }
   
}
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// this handles and allows interaction with the many compnots of a player
/// from there body, to interactions
/// </summary>
public partial class Player : Node2D
{
    [JsonProperty]
    private Dictionary<string,StatBase> stats; 

    [JsonProperty]
    private Graphic graphic;

    private CharacterBody2D body;
    private Controls controls;
    private Camera camera;

    public Player(){
        stats = new Dictionary<string, StatBase>(){
            {"MoveSpeed", new Stat("MoveSpeed","players MoveSpeed",1,0.1f,2)}
        };  

        graphic = new Graphic("", new Color(255,255,100));
        body = new CharacterBody2D(){Name = "body"};  
        camera = new Camera();

        AddChild(body);
        AddChild(camera);
        AddChild(graphic.GetTexture());
        
        controls = new Controls(); //this is tempory need to be move to a manegr class at some point
        controls.LoadBindings();
    }


    //run every time theres input
    public override void _Input(InputEvent inputEvent)
    {
        // InputMap
        base._Input(inputEvent);
        if (inputEvent.IsActionPressed("ZoomIn"))
        {
            camera.ZoomIn(); 
        }
        if (inputEvent.IsActionPressed("ZoomOut"))
        {
            camera.ZoomOut();
        }
        Move();       
    }


    public void Move(){
       
        Vector2 velocityVector = Input.GetVector("MoveLeft", "MoveRight","MoveDown","MoveUp"); 
        Position += velocityVector.Normalized() * stats["MoveSpeed"].Value;
        //todo: animation code here at some point
    }

    public void PlayAnimation(){

    }

}
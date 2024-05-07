namespace Atomation.Player;

using Godot;
using Atomation.Map;
using Atomation.Things;
using Atomation.Resources;

public partial class Controller : Node2D
{
    private static Controller instance;

    private SaveHandler saveTarget;
    private PlayerChar playerTarget;
    private WorldMap MapTarget;
    public Bindings bindings { get; private set; }

    private Controller()
    {
        Name = "Controller";
        bindings = new Bindings();

        MapTarget = WorldMap.GetInstance();
        playerTarget = PlayerChar.GetInstance();
        saveTarget = SaveHandler.GetInstance();
    }

    public static Controller GetInstance()
    {
        if (instance == null)
        {
            instance = new Controller();
        }

        return instance;
    }

    public override void _Input(InputEvent input)
    {
        base._Input(input);
        WorldControls(input);

        if (input.IsAction("MoveLeft") || input.IsAction("MoveRight") || input.IsAction("MoveUp") || input.IsAction("MoveDown"))
        {
            //figure out how to get "IsActionPressed" to work
            playerTarget.Move();
        }
        else if (input.IsActionPressed("LeftClick"))
        {
            Interact((InputEventMouseButton)input);
        }
        else if (input.IsActionPressed("RightClick"))
        {
            GD.Print("RightClick");
        }
        else if (input.IsActionPressed("QuickSave"))
        {
            saveTarget.QuickSave();
        }
        else if (input.IsActionPressed("QuickLoad"))
        {
            saveTarget.QuickLoad();
        }
    }

    private Coordinate GetMousePosition(InputEventMouseButton input)
    {
        Vector2 mousePos = input.Position - GetViewport().CanvasTransform.Origin;
        return new Coordinate(mousePos);
    }

    private void WorldControls(InputEvent input)
    {
        if (input.IsActionPressed("NewSeed"))
        {
            GD.Print("Randomizing Seed");
            MapData.GetData().RandomizeSeed();
        }
        if (input.IsActionPressed("GenerateNewMap"))
        {
            WorldGenerator.GetInstance().GenerateMap();
        }
        else if (input.IsActionPressed("VisualizeDefault"))
        {
            GD.Print("Default Mode");
            MapTarget.SetVisualizationMode(VisualizationMode.Default);
        }
        else if (input.IsActionPressed("VisualizeMoisture"))
        {
            GD.Print("Moisture Mode");

            MapTarget.SetVisualizationMode(VisualizationMode.Moisture);
        }
        else if (input.IsActionPressed("VisualizeHeat"))
        {
            GD.Print("Heat Mode");
            MapTarget.SetVisualizationMode(VisualizationMode.Heat);
        }
        else if (input.IsActionPressed("VisualizeHeight"))
        {
            GD.Print("Elevation Mode");
            MapTarget.SetVisualizationMode(VisualizationMode.Height);
        }
    }

    private void Interact(InputEventMouseButton input)
    {
        //redo some point
        Coordinate cords = GetMousePosition(input);
        if (MapTarget.GetStructure(cords) != null)
        {
            Structure structure = MapTarget.GetStructure(cords);

            structure.Damage(playerTarget.StatSheet);
            
            GD.Print($"{structure.Name} HP: {structure.StatSheet.GetStat(StatKeys.MAX_HEALTH).CurrentValue}");

        }

        else if (MapTarget.GetTerrain(cords) != null)
        {
            GD.Print("terrain");
            Terrain terrain = MapTarget.GetTerrain(cords);
            if (terrain.Visible)
            {
                terrain.Visible = false;
            }
            else
            {
                terrain.Visible = true;
            }
        }

        else
        {
            GD.Print("nothing here");
        }
    }


}
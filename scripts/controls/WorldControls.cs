namespace Atomation.Controls;

using Atomation.Map;
using Atomation.PlayerChar;
using Atomation.Things;
using Godot;

public class WorldControls
{
    private WorldMap worldMap;

    public WorldControls()
    {
        worldMap = WorldMap.Instance;
    }

    public void HandleInput(InputEvent input)
    {
        if (input.IsActionPressed("Interact"))
        {
            WorldInteraction();
        }
        GenerationControls(input);
        ChangeVisualMode(input);

    }

    private void WorldInteraction()
    {
        Coordinate cords = new Coordinate(Player.Instance.GetMousePosition());

        if (worldMap.GetStructure(cords) != null)
        {
            Structure structure = worldMap.GetStructure(cords);

            structure.Damage(Player.Instance.GetStatSheet());
            GD.Print($"{structure.GetNode().Name} HP: {structure.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue}");
        }
        else if (worldMap.GetTerrain(cords) != null)
        {
            Terrain terrain = worldMap.GetTerrain(cords);
            GD.Print($"{terrain.GetNode().Name}");
            if (terrain.GetNode().Visible)
            {
                terrain.GetNode().Visible = false;
            }
            else
            {
                terrain.GetNode().Visible = true;
            }
        }
        else
        {
            GD.Print("nothing here");
        }
    }

    private void ChangeVisualMode(InputEvent input)
    {
        if (input.IsActionPressed("VisualizeDefault"))
        {
            GD.Print("Default Mode");
            worldMap.SetVisualizationMode(VisualizationMode.Default);
        }
        else if (input.IsActionPressed("VisualizeMoisture"))
        {
            GD.Print("Moisture Mode");

            worldMap.SetVisualizationMode(VisualizationMode.Moisture);
        }
        else if (input.IsActionPressed("VisualizeHeat"))
        {
            GD.Print("Heat Mode");
            worldMap.SetVisualizationMode(VisualizationMode.Heat);
        }
        else if (input.IsActionPressed("VisualizeHeight"))
        {
            GD.Print("Elevation Mode");
            worldMap.SetVisualizationMode(VisualizationMode.Height);
        }
    }

    private void GenerationControls(InputEvent input)
    {
        if (input.IsActionPressed("NewSeed"))
        {
            GD.Print("Randomizing Seed");
            MapData.GetData().RandomizeSeed();
        }
        if (input.IsActionPressed("GenerateNewMap"))
        {
            WorldGenerator.Instance.GenerateMap();
        }
    }


}
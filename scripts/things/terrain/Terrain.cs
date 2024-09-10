namespace Atomation.Things;

using Resources;
using GameMap;
using StatSystem;

using Godot;
using Newtonsoft.Json;

/// <summary>
/// Terrain make up the floor or ground in the game. it is the building
/// block in which structure and other things are place on top of 
/// </summary>
public class Terrain : Thing
{
    private float elevation;
    private float temperature;
    private float moisture;

    private SupportType supportProvided;
    private SupportType supportReq;

    [JsonConstructor]
    public Terrain() { }

    public Terrain(Vector2 position)
    {
        graphic = new Graphic();
        graphic.Position = position * Map.CELL_SIZE;
        // graphic.SetTexture();
    }

    public void Configure(TerrainDef def, bool loading = false)
    {
        if (!loading)
        {
            name = def.DefName;
            statSheet = new StatSheet(def.StatSheet, this);
        }

        description = def.Description;
        supportProvided = def.SupportProvided;
        supportReq = def.SupportReq;

        GridLayer = def.GridLayer;
        graphic.Name = $"{name} {Position}";
        graphic.Configure(def.GraphicData);
        
        // UpdateGraphic(VisualizationMode.Default);
        // collisionBox.Shape = new RectangleShape2D() { Size = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE) };
        // collisionBox.Position = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE) / 2;
    }

    public float Elevation { get => elevation; set => elevation = value; }
    public float Temperature { get => temperature; set => temperature = value; }
    public float Moisture { get => moisture; set => moisture = value; }

    public SupportType SupportProvided { get => supportProvided; set => supportProvided = value; }
    public SupportType SupportReq { get => supportReq; set => supportReq = value; }


    // public void UpdateGraphic(VisualizationMode displayMode)
    // {
    //     // if (displayMode == VisualizationMode.Default)
    //     // {
    //     //     graphic.SetDefaultColor();
    //     // }
    //     // else if (displayMode == VisualizationMode.Height)
    //     // {
    //     //     GetHeightColor();
    //     // }
    //     // else if (displayMode == VisualizationMode.Heat)
    //     // {
    //     //     GetHeatColor();
    //     // }
    //     // else
    //     // {
    //     //     GetMoistureColor();
    //     // }
    // }

    public bool CanSupport(SupportType supportReq)
    {
        if (supportProvided <= supportReq)
        {
            return true;
        }
        return false;
    }

}
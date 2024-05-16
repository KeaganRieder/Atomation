namespace Atomation.Things;

using Atomation.Resources;
using Atomation.Map;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// Terrain make up the floor or ground in the game. it is the building
/// block in which structure and other things are place on top of 
/// </summary>
public class Terrain : ThingBase
{
    private SupportType supportProvided;
    private SupportType supportReq;
    private bool collidable;

    private StaticGraphic graphic;

    [JsonProperty("elevation")]
    public float Elevation;
    [JsonProperty("temperature")]
    public float Temperature;
    [JsonProperty("moisture")]
    public float Moisture;

    [JsonConstructor]
    public Terrain() { }

    public Terrain(Terrain loaded)
    {
        node = new Node2D();
        graphic = new StaticGraphic();
        node.AddChild(graphic);
        SetPosition(loaded.GetCoordinate());

        defName = loaded.defName;
        Elevation = loaded.Elevation;
        Temperature = loaded.Temperature;
        Moisture = loaded.Moisture;
        statSheet = new StatSheet(loaded.statSheet, this);

        Configure(ThingDatabase.Instance.GetTerrainDef(loaded.defName), true);
    }

    public Terrain(Coordinate cord)
    {
        node = new Node2D();
        graphic = new StaticGraphic();
        node.AddChild(graphic);

        SetPosition(cord);
    }
    
    ~Terrain()
    {
        DestroyNode();
    }

    public override void DestroyNode()
    {
        if (GodotObject.IsInstanceValid(graphic))
        {
            graphic.QueueFree();
        }
        base.DestroyNode();
    }

    public void Configure(TerrainDef def, bool loading = false)
    {
        defName = def.defName;
        description = def.description;
        collidable = def.collidable;

        supportProvided = def.supportProvided;
        supportReq = def.supportReq;

        node.Name = $"{defName} {cords}";
        graphic.Configure(def.graphicData);
        UpdateGraphic(VisualizationMode.Default);

        if (!loading)
        {
            statSheet = new StatSheet(def.statSheet, this);

        }
    }

    public void SetElation(float val)
    {
        Elevation = val;
    }
    public void SetMoisture(float val)
    {
        Moisture = val;
    }
    public void SetTemperature(float val)
    {
        Temperature = val;
    }

    public float GetElation()
    {
        return Elevation;
    }
    public float GetMoisture()
    {
        return Moisture;
    }
    public float GetTemperature()
    {
        return Temperature;
    }

    public void UpdateGraphic(VisualizationMode displayMode)
    {
        if (displayMode == VisualizationMode.Default)
        {
            graphic.SetDefaultColor();
        }
        else if (displayMode == VisualizationMode.Height)
        {
            GetHeightColor();
        }
        else if (displayMode == VisualizationMode.Heat)
        {
            GetHeatColor();
        }
        else
        {
            GetMoistureColor();
        }
    }

    public StaticGraphic GetGraphic()
    {
        return graphic;
    }
    public void GetHeatColor()
    {
        Color heatColor;

        if (Temperature < -1.3)
        {
            heatColor = Colors.White;
        }
        else if (Temperature < -1.0)
        {
            heatColor = Colors.Pink;
        }
        else if (Temperature < -0.8)
        {
            heatColor = Colors.Purple;
        }
        else if (Temperature < -0.5)
        {
            heatColor = Colors.DarkBlue;
        }
        else if (Temperature < -0.25)
        {
            heatColor = Colors.Cyan;
        }
        else if (Temperature < 0.25)
        {
            heatColor = Colors.Green;
        }
        else if (Temperature < 0.7)
        {
            heatColor = Colors.DarkGreen;
        }
        else if (Temperature < 1)
        {
            heatColor = Colors.Yellow;
        }
        else if (Temperature < 1.25)
        {
            heatColor = Colors.Orange;
        }
        else if (Temperature < 1.7)
        {
            heatColor = Colors.Red;
        }
        else
        {
            heatColor = Colors.DarkRed;
        }

        graphic.Modulate = heatColor;
    }

    public void GetHeightColor()
    {
        graphic.Modulate = new Color(Elevation, Elevation, Elevation);
    }

    public void GetMoistureColor()
    {
        //this needs work
        Color moistureColor = Colors.Gray;

        if (Moisture < 0)
        {
            moistureColor = new Color(Mathf.Abs(Moisture), .5f, .5f);
        }
        if (Moisture >= 0)
        {
            moistureColor = new Color(1, Moisture, 1);

        }

        graphic.Modulate = moistureColor;
    }


    public bool Supports(SupportType supportReq)
    {
        if (supportProvided <= supportReq)
        {
            return true;
        }
        return false;
    }

}
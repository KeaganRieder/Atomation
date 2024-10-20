namespace Atomation.Things;

using Resources;
using GameMap;
using StatSystem;

using Godot;
using Newtonsoft.Json;
using System.Collections.Generic;


/// <summary>
/// Terrain make up the floor or ground in the game. it is the building
/// block in which structure and other things are place on top of 
/// </summary>
public partial class Terrain : Thing
{
    private bool layerAble;
    private float elevation;
    private float temperature;
    private float moisture;

    private SupportType supportProvided;
    private SupportType supportReq;

    public bool LayerAble { get => layerAble; set => layerAble = value; }

    public float Elevation { get => elevation; set => elevation = value; }
    public float Temperature { get => temperature; set => temperature = value; }
    public float Moisture { get => moisture; set => moisture = value; }

    public SupportType SupportProvided { get => supportProvided; set => supportProvided = value; }
    public SupportType SupportReq { get => supportReq; set => supportReq = value; }


    public override Dictionary<string, object> FormatThingDef()
    {
        Dictionary<string, object> thingDef = base.FormatThingDef();
        thingDef.Add("Supports", SupportProvided);
        thingDef.Add("LayerAble", layerAble);

        return thingDef;
    }

    [JsonConstructor]
    public Terrain() { }

    public Terrain(Vector2 position)
    {
        graphic = new Graphic();
        graphic.Position = position ;//* Map.CELL_SIZE;
    }

    public override void DestroyNode()
    {
        base.DestroyNode();
    }

    public override void Configure(string defId)
    {
        base.Configure(ThingDefDatabase.Instance.GetTerrainDef(defId), defId);
    }

    public override void ConfigureFromDef(Dictionary<string, object> def)
    {
        base.ConfigureFromDef(def);
        if (gridLayer == -1)
        {
            gridLayer = GameLayers.Terrain;
        }
    }

    public override void Save()
    {
        GD.Print("saving of things not implemented");
    }

    public override void Load()
    {
        GD.Print("loading of things not implemented");

    }

    public bool CanSupport(SupportType supportReq)
    {
        if (supportProvided <= supportReq)
        {
            return true;
        }
        return false;
    }
}
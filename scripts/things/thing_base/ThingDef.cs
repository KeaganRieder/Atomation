namespace Atomation.Things;

using Map;
using Resources;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// Data class used to structure def files which are read and used 
/// in the creation of new complex thing instances, during game runtime
/// </summary>
public abstract class ThingDef : Def
{
    public GraphicData graphicData { get; set; }
    public StatSheet statSheet { get; set; }

    [JsonConstructor]
    protected ThingDef() { }
    protected ThingDef(string name, string description, StatSheet statSheet, GraphicData graphicData) : base(name, description)
    {
        defName = name;
        this.description = description;
        this.statSheet = statSheet;
        this.graphicData = graphicData;
        if (graphicData.graphicSize == Vector2I.Zero)
        {
            this.graphicData.graphicSize = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE);
        }
    }
   

    public override string GetKey()
    {
        if (defName == "" || defName == null)
        {
            defName = "DefaultThing";
        }

        return defName;
    }
}
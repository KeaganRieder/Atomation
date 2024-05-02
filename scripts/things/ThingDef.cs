namespace Atomation.Things;

using Atomation.Map;
using Atomation.Resources;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// Data class used to structure def files which are read and used 
/// in the creation of new complex thing instances, during game runtime
/// </summary>
public abstract class ThingDef : Def
{
    [JsonProperty("Parent")]
    public string Parent { get; set; }
    [JsonProperty("Graphic Data")]
    public GraphicData GraphicData { get; set; }
    [JsonProperty("Stat Sheet")]
    public StatSheet StatSheet { get; set; }

    protected ThingDef() { }
    protected ThingDef(string name, string description, StatSheet statSheet, GraphicData graphicData) : base(name, description)
    {
        Name = name;
        Description = description;
        StatSheet = statSheet;
        GraphicData = graphicData;
        if (GraphicData.GraphicSize == Vector2I.Zero)
        {
            graphicData.GraphicSize = new Vector2I(MapData.CELL_SIZE,MapData.CELL_SIZE);
        }
    }

    public override string GetKey()
    {
        if (Name == "" || Name == null)
        {
            Name = "DefaultThing";
        }

        return Name;
    } 
}
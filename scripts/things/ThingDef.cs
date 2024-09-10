namespace Atomation.Things;

using GameMap;
using Resources;
using StatSystem;
using Godot;
using Newtonsoft.Json;

public interface IDef
{
    /// <summary>
    /// objects name
    /// </summary/
    public string DefName { get; set; }

    /// <summary>
    /// key for cache dictionary, in order to properly sort
    /// and retrieve the item from it
    /// </summary>
    public abstract string GetKey();
}
/// <summary>
/// class used to define initial value of a thing in teh game. this also acts as a way 
/// or formatting def file to be read,cached and used to create new thing during play time
/// </summary>
public abstract class ThingDef : IDef
{
    protected string defName;
    protected string description;

    protected StatSheet statSheet;
    protected GraphicData graphicData;
    private int gridLayer;

    [JsonProperty(Order = 1)]
    public string DefName { get => defName; set => defName = value; }
    [JsonProperty(Order = 1)]
    public string Description { get => description; set => description = value; }
    [JsonProperty(Order = 1)]
    public StatSheet StatSheet { get => statSheet; set => statSheet = value; }
    [JsonProperty(Order = 1)]
    public GraphicData GraphicData { get => graphicData; set => graphicData = value; }
    [JsonProperty(Order = 1)]
    public int GridLayer { get => gridLayer; set => gridLayer = value; }

    [JsonConstructor]
    protected ThingDef() { }
    protected ThingDef(string name, string description, StatSheet statSheet, GraphicData graphicData, int gridLayer = -1)
    {
        defName = name;
        this.description = description;
        this.statSheet = statSheet;
        this.graphicData = graphicData;
        if (graphicData.GraphicSize == Vector2I.Zero)
        {
            this.graphicData.GraphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE);
        }
        this.GridLayer = gridLayer;
    }

    public virtual string GetKey()
    {
        if (defName == "" || defName == null)
        {
            defName = "DefaultThing";
        }

        return defName;
    }
}
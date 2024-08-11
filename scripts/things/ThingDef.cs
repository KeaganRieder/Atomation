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

    public StatSheet StatSheet { get => statSheet; set => statSheet = value; }
    public GraphicData GraphicData { get => graphicData; set => graphicData = value; }
    public string DefName { get => defName; set => defName = value; }
    public string Description { get => description; set => description = value; }

    [JsonConstructor]
    protected ThingDef() { }
    protected ThingDef(string name, string description, StatSheet statSheet, GraphicData graphicData)
    {
        defName = name;
        this.description = description;
        this.statSheet = statSheet;
        this.graphicData = graphicData;
        if (graphicData.graphicSize == Vector2I.Zero)
        {
            this.graphicData.graphicSize = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE);
        }
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
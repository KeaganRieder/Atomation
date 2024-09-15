namespace Atomation.Things;

using Resources;
using StatSystem;
using GameMap;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// The base class of all things that make up the games world.
/// </summary>

public abstract partial class Thing : Node2D
{
    protected string name;
    protected string description;

    protected StatSheet statSheet;

    protected Graphic graphic;
    protected CollisionShape2D collisionBox;

    protected Chunk chunk;

    /// <summary>
    /// the rendering/interaction layer the thing is on the grid
    /// </summary>
    protected int gridLayer;

    public Thing() { }

    public Thing(ThingDef configs)
    {
        Configure(configs);
    }

    // [JsonProperty(Order = 1)]
    // public string Name { get => name; private set => name = value; }
    [JsonIgnore]
    public string Description { get => description; private set => description = value; }
    [JsonProperty(Order = 1)]
    public StatSheet StatSheet { get => statSheet; set => statSheet = value; }
    // [JsonProperty(Order = 1)]
    // public Vector2 Position { get => graphic.Position; set => graphic.Position = value; }

    [JsonIgnore]
    public Graphic Graphic { get => graphic; set => graphic = value; }
    [JsonIgnore]
    public CollisionShape2D CollisionBox { get => collisionBox; set => collisionBox = value; }

    [JsonIgnore]
    public int GridLayer
    {
        get => gridLayer;
        set
        {
            gridLayer = value;
            if (graphic != null)
            {
                graphic.RenderingLayer = value;
            }
        }
    }

    [JsonIgnore]
    public Chunk Chunk { get => chunk; set => chunk = value; }

    /// <summary>
    /// configures the thing, using values present in a thingDef file
    /// </summary>
    public virtual void Configure(ThingDef config, Vector2 offset = default)
    {
        //maybe make from readFromDef?
        if (config == null)
        {
            GD.PrintErr("can't configure thing from null configs");
            return;
        }
        name = config.DefName;
        description = config.Description;
        statSheet = new StatSheet(config.StatSheet, this);

        if (config.GridLayer > 0)
        {
            gridLayer = config.GridLayer;
        }
    }

    public virtual void DestroyNode()
    {        
        if (IsInstanceValid(graphic))
        {
            graphic.QueueFree();
            graphic = null;
        }
        if (IsInstanceValid(collisionBox))
        {
            collisionBox.QueueFree();
            collisionBox = null;
        }
        QueueFree();
    }
}
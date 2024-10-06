namespace Atomation.Things;

using Resources;
using StatSystem;
using GameMap;
using Godot;
using System.Collections.Generic;
using System;

/// <summary>
/// The base class of all things that make up the games world.
/// </summary>
public abstract partial class Thing : IThing
{
    protected string id;
    protected string description;

    protected StatSheet statSheet;

    protected Graphic graphic;
    protected CollisionShape2D collisionBox;

    protected Chunk chunk; //maybe mak this the chunks coordinate?

    public string ID { get => id; }
    public string Description { get => description; private set => description = value; }

    public virtual Node2D Node { get => graphic; }
    public Vector2 Position
    {
        get
        {
            if (GodotObject.IsInstanceValid(Node))
            {
                return Node.Position;
            }
            return default;
        }
        set
        {
            if (GodotObject.IsInstanceValid(Node))
            {
                Node.Position = value;
            }
        }
    }

    public StatSheet StatSheet { get => statSheet; set => statSheet = value; }
    public Graphic Graphic { get => graphic; set => graphic = value; }
    public CollisionShape2D CollisionBox { get => collisionBox; set => collisionBox = value; }
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

    public Chunk Chunk { get => chunk; set => chunk = value; }

    /// <summary>
    /// the rendering/interaction layer the thing is on the grid
    /// </summary>
    protected int gridLayer;

    protected Thing() { }

    protected Thing(string name, string description, StatSheet statSheet, Graphic graphic)
    {
        id = name;
        this.description = description;
        this.statSheet = statSheet;
        this.graphic = graphic;
    }

    ~Thing()
    {
        DestroyNode();
    }

    /// <summary>
    /// destroys the thing and it's children
    /// </summary>
    public virtual void DestroyNode()
    {
        if (GodotObject.IsInstanceValid(graphic))
        {
            graphic.QueueFree();
            graphic = null;
        }
        if (GodotObject.IsInstanceValid(collisionBox))
        {
            collisionBox.QueueFree();
            collisionBox = null;
        }
    }

    /// <summary>
    /// configures the thing, using values present in a thingDef file
    /// </summary>
    public virtual void Configure(Dictionary<string, object> def, string defId)
    {
        if (graphic == null)
        {
            graphic = new Graphic();
        }
        id = defId;
        ConfigureFromDef(def);
    }

    public virtual void Configure(string id)
    {
        GD.Print("configuration not implement");
    }

    /// <summary>
    /// formats the thing into a thing def, meant to be written to
    /// a json. it's then read in and used in creating new instances
    /// of things
    /// </summary>
    public virtual Dictionary<string, object> FormatThingDef()
    {
        Dictionary<string, object> thingDef = new Dictionary<string, object>{
                {"Description", description},
                {"GridLayer", gridLayer},
                {"StatSheet", StatSheet},
                {"Graphic",graphic.FormatGraphicConfigs()}
            };

        return thingDef;
    }

    /// <summary>
    /// configures the instance of the thing to be based on the values in the def file
    /// </summary>
    public virtual void ConfigureFromDef(Dictionary<string, object> def)
    {
        if (def == null)
        {
            GD.Print("it's null");
        }
        description = def.ContainsKey("Description") ? def["Description"].ToString() : "default description";
        gridLayer = def.ContainsKey("GridLayer") ? Convert.ToInt32(def["GridLayer"]) : -1;

        statSheet = def.ContainsKey("StatSheet") ? def["StatSheet"].ConvertJsonObject<StatSheet>() : default;

        graphic.Configure(def.ContainsKey("Graphic") ? def["Graphic"].ConvertJsonObject<Dictionary<string, object>>() : null);
    }

    /// <summary>
    /// formats the thing into a saved thing
    /// </summary>
    public virtual void Save()
    {
        GD.Print("saving of things not implemented");
    }

    /// <summary>
    /// uses saved data to properly configure the instance of a thing
    /// </summary>
    public virtual void Load()
    {
        GD.Print("loading of things not implemented");

    }


}
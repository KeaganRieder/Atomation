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
public abstract partial class Thing :  Node2D, IThing
{
    protected string thingName;
    protected string description;

    protected StatSheet statSheet;

    protected Graphic graphic;
    protected CollisionShape2D collisionBox;

    protected Chunk chunk; //maybe mak this the chunks coordinate?

    /// <summary>
    /// the rendering/interaction layer the thing is on the grid
    /// </summary>
    protected int gridLayer;

    protected Thing() { }

    protected Thing(string name, string description, StatSheet statSheet, Graphic graphic)
    {
        thingName = name;
        Name = thingName;
        this.description = description;
        this.statSheet = statSheet;
        this.graphic = graphic;
    }
    protected string ThingName { get => thingName; set => thingName = value; }

    public Node Node{get => this;}

    public string Description { get => description; private set => description = value; }
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
    /// configures the thing, using values present in a thingDef file
    /// </summary>
    public virtual void Configure(Dictionary<string, object> def, string defName)
    {
        if (graphic == null)
        {
            graphic = new Graphic();
            AddChild(graphic);
        }
        thingName = defName;
        Name = defName + ":" + Name;
        ConfigureFromDef(def);
    }
    public virtual void Configure(string defName)
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
        gridLayer =  def.ContainsKey("GridLayer") ? Convert.ToInt32(def["GridLayer"]) : -1;

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

    /// <summary>
    /// destroys the thing and it's children
    /// </summary>
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
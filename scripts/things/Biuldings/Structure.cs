namespace Atomation.Things;

using Atomation.Resources;
using Atomation.GameMap;
using Godot;
using Newtonsoft.Json;
using System.Collections.Generic;
using Atomation.StatSystem;

//maybe rename to walls?

/// <summary>
/// a structure or building is something that is either naturally
/// occurring in the game or is built and placed by the player
/// </summary>
public partial class Structure : Thing
{
    private SupportType supportReq;

    private Dictionary<string, int> resources;

    [JsonConstructor]
    public Structure() { }

    public Structure(Vector2 position)
    {
        graphic = new Graphic();
        collisionBox = new CollisionShape2D();
        graphic.AddChild(collisionBox);

        graphic.Position = position;
    }

    public override void Configure(string defName)
    {
        base.Configure(ThingDefDatabase.Instance.GetStructureDef(defName), defName);
    }

    public override Dictionary<string, object> FormatThingDef()
    {
        Dictionary<string, object> thingDef = base.FormatThingDef();
        thingDef.Add("Resources", resources);

        return thingDef;
    }

    public override void ConfigureFromDef(Dictionary<string, object> def)
    {
        base.ConfigureFromDef(def);
        if (gridLayer == -1)
        {
            gridLayer = GameLayers.Structure;
        }
        resources = def.ContainsKey("Resources") ? def["Resources"].ConvertJsonObject<Dictionary<string, int>>() : null;
    }

    public override void Save()
    {
        GD.Print("saving of things not implemented");
    }

    public override void Load()
    {
        GD.Print("loading of things not implemented");

    }
    
    /// <summary>
    /// destroys node
    /// </summary>
    public override void DestroyNode()
    {
        collisionBox.QueueFree();
        base.DestroyNode();
    }

    /// <summary>
    /// applies the specified damage to the structure. if the damage received 
    /// is fatal, destroy structure and given back an amount of resources
    /// </summary>
    public void Damage(float amount)
    {
        //todo make mining damage? or at least have mining effect resources given
        // if approbate
        statSheet.GetStat("health").Damage = statSheet.GetStat("health").Damage + amount;
        GD.Print($"Health remaining: {statSheet.GetStat("health").CurrentValue}");

        if (statSheet.GetStat("health").CurrentValue <= 0)
        {
            Vector2 position = Node.Position.GlobalToMap();
            chunk.RemoveGridObject<Structure>(position, gridLayer);

            foreach (var item in resources)
            {
                //todo make have to find next valid space
                Item droppedItem = new Item(position * Map.CELL_SIZE);
                droppedItem.Configure(item.Key);
                droppedItem.CurrentStackSize = item.Value;
                chunk.SetGridObject(position, droppedItem);
            }

            DestroyNode();
            return;
        }
    }

    /// <summary>
    /// applies the specified damage to the structure.this damage is gotten from 
    /// the provide stat in the statSheet
    /// </summary>
    public void Damage(StatSheet statSheet)
    {
        StatBase dmg = statSheet.GetStat("attack");
        if (dmg != null)
        {
            Damage(dmg.CurrentValue);
        }
    }

    /// <summary>
    /// applies the specified healing amount to the structure. 
    /// </summary>
    public void Heal(float amount)
    {
        statSheet.GetStat("health").Damage -= amount;
    }




}
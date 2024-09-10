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
public class Structure : Thing
{
    private SupportType supportReq;

    // private bool buildAble;
    private Dictionary<string, int> resources;

    [JsonConstructor]
    public Structure() { }

    public Structure(Vector2 position)
    {
        graphic = new Graphic();
        collisionBox = new CollisionShape2D();

        graphic.AddChild(collisionBox);

        graphic.Position = position * Map.CELL_SIZE;
    }

    public void Configure(StructureDef def, bool loading = false)
    {
        if (!loading)
        {
            name = def.DefName;
            statSheet = new StatSheet(def.StatSheet, this);
        }
        description = def.Description;
        supportReq = def.SupportReq;

        resources = new Dictionary<string, int>();

        foreach (var item in def.BuildCost)
        {
            resources.Add(item.Key, item.Value);
        }

        GridLayer = def.GridLayer;
        graphic.Name = $"{name} {Position}";
        graphic.ZIndex = def.GridLayer;
        graphic.Configure(def.GraphicData);

        // collisionBox.Shape = new RectangleShape2D() { Size = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE) };
        // collisionBox.Position = new Vector2I(Map.CELL_SIZE, Map.CELL_SIZE) / 2;
    }

    public override void DestroyNode()
    {
        if (GodotObject.IsInstanceValid(graphic))
        {
            graphic.QueueFree();
        }
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
            Vector2 position = Position.GlobalToMap();
            chunk.RemoveStructure(position);

            foreach (var item in resources)
            {
                //todo make have to find next valid space
                Item droppedItem = new Item(position);
                droppedItem.Configure(ThingDatabase.Instance.GetItemDef(item.Key));
                droppedItem.CurrentStackSize = item.Value;
                chunk.SetItem(position, droppedItem);
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
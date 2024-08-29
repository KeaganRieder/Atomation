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
        graphic = new StaticGraphic();
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

        // foreach (var items in def.BuildCost)
        // {
        //     resources.Add(items.Key, items.Value);
        // }

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

    public StaticGraphic GetGraphic()
    {
        return graphic;
    }
    public SupportType GetRequiredSupport()
    {
        return supportReq;
    }

    public void Damage(float amount)
    {
        statSheet.GetStat("health").Damage += amount;
        if (statSheet.GetStat("health").CurrentValue <= 0)
        {
            // Map.Instance.SetStructure(Position, null);
            foreach (var item in resources)
            {
                // Item dropped = new Item(Position);
                // dropped.Configure(ThingDatabase.Instance.GetItemDef(item.Key));
                // dropped.SetQuantity(item.Value);
                // WorldMap.Instance.SetItem(cords, dropped);
            }

            DestroyNode();
            return;
        }
    }
    public void Damage(StatSheet statSheet)
    {
        StatBase dmg = statSheet.GetStat("attack");

        if (dmg != null)
        {
            Damage(dmg.CurrentValue);
        }
    }
    public void Heal(float amount)
    {
        statSheet.GetStat("health").Damage -= amount;
    }
}
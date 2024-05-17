namespace Atomation.Things;

using Atomation.Resources;
using Atomation.Map;
using Godot;
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// a structure or building is something that is either naturally
/// occurring in the game or is built and placed by the player
/// </summary>
public class Structure : ThingBase
{
    private SupportType supportReq;
    private StaticGraphic graphic;

    // private bool buildAble;
    private Dictionary<string, int> resources;


    [JsonConstructor]
    public Structure() { }

    public Structure(Structure loaded)
    {
        node = new Node2D();
        graphic = new StaticGraphic();
        collisionBox = new CollisionShape2D();

        node.AddChild(graphic);
        node.AddChild(collisionBox);

        SetPosition(loaded.cords);
        defName = loaded.defName;
        statSheet = new StatSheet(loaded.statSheet, this);

        Configure(ThingDatabase.Instance.GetStructureDef(defName), true);
    }
    public Structure(Coordinate cord)
    {
        // buildAble = false;
        node = new Node2D();
        graphic = new StaticGraphic();
        collisionBox = new CollisionShape2D();

        node.AddChild(graphic);
        node.AddChild(collisionBox);

        cords = cord;
        SetPosition(cord);
    }

    public void Configure(StructureDef def, bool loading = false)
    {
        if (!loading)
        {
            defName = def.defName;
            statSheet = new StatSheet(def.statSheet, this);
        }
        description = def.description;
        supportReq = def.supportReq;
        resources = new Dictionary<string, int>();
        foreach (var items in def.buildCost)
        {
            resources.Add(items.Key, items.Value);
        }

        node.Name = $"{defName} {cords}";
        graphic.Configure(def.graphicData);
        collisionBox.Shape = new RectangleShape2D() { Size = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE) };
        collisionBox.Position = new Vector2I(MapData.CELL_SIZE, MapData.CELL_SIZE) / 2;
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
        statSheet.GetStat(StatKeys.MAX_HEALTH).Damage(amount);
        if (statSheet.GetStat(StatKeys.MAX_HEALTH).CurrentValue <= 0)
        {
            WorldMap.Instance.SetStructure(cords, null);
            foreach (var item in resources)
            {
                Item dropped = new Item(cords);
                dropped.Configure(ThingDatabase.Instance.GetItemDef(item.Key));
                dropped.SetQuantity(item.Value);
                WorldMap.Instance.SetItem(cords, dropped);
            }

            DestroyNode();
            return;
        }
    }
    public void Damage(StatSheet statSheet)
    {
        StatBase dmg = statSheet.GetStat(StatKeys.ATTACK_DAMAGE);

        if (dmg != null)
        {
            Damage(dmg.CurrentValue);
        }
    }
    public void Heal(float amount)
    {
        statSheet.GetStat(StatKeys.MAX_HEALTH).Heal(amount);
    }
}
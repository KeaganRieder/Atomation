namespace Atomation.Things;

using Atomation.Resources;
using Atomation.Map;
using Godot;
using Newtonsoft.Json;

/// <summary>
/// a structure or building is something that is either naturally
/// occurring in the game or is built and placed by the player
/// </summary>
public class Structure : ThingBase
{
    private SupportType supportReq;
    private StaticGraphic graphic;
    // private  

    [JsonConstructor]
    public Structure() { }

    public Structure(Structure loaded)
    {
        node = new Node2D();
        graphic = new StaticGraphic();
        node.AddChild(graphic);

        SetPosition(loaded.cords);
        defName = loaded.defName;
        statSheet = new StatSheet(loaded.statSheet, this);

        Configure(DefDatabase.GetInstance().GetStructureDef(defName), true);
    }

    public Structure(Coordinate cord)
    {
        node = new Node2D();
        graphic = new StaticGraphic();
        node.AddChild(graphic);

        cords = cord;
        SetPosition(cord);
    }
    ~Structure()
    {
        DestroyNode();
    }

    public void Configure(StructureDef def, bool loading = false)
    {
        defName = def.defName;
        description = def.description;
        supportReq = def.supportReq;

        node.Name = $"{defName} {cords}";
        graphic.Configure(def.graphicData);
        if (!loading)
        {
            statSheet = new StatSheet(def.statSheet, this);
        }
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
            WorldMap.GetInstance().SetStructure(cords,null);
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
namespace Atomation.Things;

using Newtonsoft.Json;
using Godot;
using Resources;
using Map;


/// <summary>
/// items come in many forms and are used in a variety of things
/// </summary>
public partial class Item : ThingBase
{
    private float stackLimit;
    [JsonProperty]
    private float quantity;
    private StaticGraphic graphic;

    [JsonConstructor]
    public Item()
    {

    }

    public Item(Item loaded)
    {
        node = new Node2D();
        graphic = new StaticGraphic();
        SetPosition(loaded.cords);
        defName = loaded.defName;
        quantity = loaded.quantity;

        ReadConfigs(DefDatabase.GetInstance().GetItemDef(defName), true);
    }


    public Item(Coordinate cords)
    {
        node = new Node2D();
        graphic = new StaticGraphic();
        SetPosition(cords);
    }

    public void ReadConfigs(ItemDef itemDef, bool loading = false)
    {
        defName = itemDef.defName;
        description = itemDef.description;
        stackLimit = itemDef.stackLimit;

        node.Name = defName + cords.ToString();
        graphic.Configure(itemDef.graphicData);
        node.AddChild(graphic);
        if (!loading)
        {
            statSheet = new StatSheet(itemDef.statSheet, this);
        }
    }

    public StaticGraphic GetGraphic()
    {
        return graphic;
    }

    public void IncreaseQuantity(int amt)
    {
        quantity += amt;
        if (quantity >= stackLimit)
        {
            quantity = stackLimit;
        }
    }
    public void DecreaseQuantity(int amt)
    {
        quantity -= amt;
        if (quantity <= 0)
        {
            quantity = 0;
        }
    }
}
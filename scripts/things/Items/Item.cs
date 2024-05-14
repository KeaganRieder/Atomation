namespace Atomation.Things;

using Newtonsoft.Json;
using Godot;
using Resources;
using Map;
using Atomation.PlayerChar;


/// <summary>
/// items come in many forms and are used in a variety of things
/// </summary>
public partial class Item : ThingBase
{
    private int stackLimit;
    [JsonProperty]
    private int quantity;
    private StaticGraphic graphic;

    [JsonConstructor]
    public Item()
    {

    }

    public Item(Item loaded, bool inventoryItem = false)
    {
        graphic = new StaticGraphic();

        if (!inventoryItem)
        {
            node = new Node2D();
            node.AddChild(graphic);
        }

        SetPosition(loaded.cords);
        defName = loaded.defName;
        quantity = loaded.quantity;

        ReadConfigs(DefDatabase.Instance.GetItemDef(defName), true, inventoryItem);
    }
    public Item(Coordinate cords)
    {
        quantity = 1;
        node = new Node2D();
        graphic = new StaticGraphic();
        statSheet = new StatSheet();

        node.AddChild(graphic);
        SetPosition(cords);
    }

    public void ReadConfigs(ItemDef itemDef, bool loading = false, bool inventoryItem = false)
    {
        defName = itemDef.defName;
        description = itemDef.description;
        stackLimit = itemDef.stackLimit;

        graphic.Configure(itemDef.graphicData);

        if (!inventoryItem)
        {
            node.Name = defName + cords.ToString();
        }
        if (!loading)
        {
            statSheet = (itemDef.statSheet == null) ? new StatSheet() : new StatSheet(itemDef.statSheet, this);
        }
    }

    public StaticGraphic GetGraphic()
    {
        return graphic;
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public bool Stackable()
    {
        return stackLimit > 0 && quantity > stackLimit;
    }

    // public void PickUp(){

    // }
}
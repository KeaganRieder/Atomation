namespace Atomation.Things;

using Newtonsoft.Json;
using Godot;
using Resources;
using Map;
using Atomation.PlayerChar;
using Atomation.Systems;


/// <summary>
/// items come in many forms and are used in a variety of things
/// </summary>
public partial class Item : ThingBase
{
    private bool stackable;
    private int stackLimit;

    [JsonProperty]
    protected int quantity;

    protected StaticGraphic graphic;

    [JsonConstructor]
    public Item() { }
    public Item(Item loaded)
    {
        defName = loaded.defName;
        quantity = loaded.quantity;
        graphic = new StaticGraphic();

        SetPosition(loaded.cords);

        Configure(ThingDatabase.Instance.GetItemDef(defName), true);
    }
    public Item(Coordinate cords)
    {
        quantity = 1;
        graphic = new StaticGraphic();
        statSheet = new StatSheet();
        SetPosition(cords);
    }
    ~Item()
    {
        DestroyNode();
    }

    public void Configure(ItemDef itemDef, bool loading = false)
    {
        defName = itemDef.defName;
        description = itemDef.description;
        stackable = itemDef.stackable;

        stackLimit = stackable ? itemDef.stackLimit : 1;

        graphic.Configure(itemDef.graphicData);
        graphic.Name = defName + cords.ToString();

        if (!loading)
        {
            statSheet = (itemDef.statSheet == null) ? new StatSheet() : new StatSheet(itemDef.statSheet, this);
        }
    }
    /// <summary> destroys all nodes of item </summary>
    public override void DestroyNode()
    {
        if (GodotObject.IsInstanceValid(graphic))
        {
            graphic.QueueFree();
            graphic = null;
        }
    }

    public override void SetPosition(Coordinate cord)
    {
        cords = cord;
        if (graphic != null)
        {
            graphic.Position = cords.GetWorldPosition();
        }
    }

    /// <summary> sets quantity of item </summary>
    public void SetQuantity(int amt)
    {
        quantity = amt;
        if (quantity > stackLimit)
        {
            quantity = stackLimit;
            GD.PrintErr($"Set Item {defName} to be over stack limit");
        }
        if (quantity < 0)
        {
            quantity = 0;
            GD.PrintErr($"Set Item {defName} to be less then 0");
        }
    }
    
    /// <summary>
    /// adds amount until stack limit is reached and then returns
    /// the remaining
    /// </summary>
    public int AddAmount(int amount)
    {
        int remaining = 0;
        quantity += amount;

        if (quantity > stackLimit)
        {
            remaining = (quantity - stackLimit > 0) ? quantity - stackLimit : 0;
            quantity = stackLimit;
        }

        return remaining;
    }
    /// <summary>
    /// adds amount until stack limit is reached and then returns
    /// the remaining
    /// </summary>
    public void AddAmount(Item item)
    {
        item.SetQuantity(AddAmount(item.quantity));
    }

    /// <summary> 
    /// attempts removes the provided amount otherwise returns
    /// the total amount removed
    /// </summary>
    public int RemoveAmount(int amount)
    {
        if (amount > quantity)
        {
            amount -= quantity;
            quantity = 0;
            return amount;
        }

        quantity -= amount;
      
        return 0;
    }

    public override StaticGraphic GetNode(){
        return graphic;
    }
    public int GetQuantity()
    {
        return quantity;
    }
    public bool Stackable()
    {
        return stackable && quantity < stackLimit;
    }

    public void PickUp(Inventory inventory){
        inventory.AddItem(this);
        if (quantity == 0)
        {
            WorldMap.Instance.SetItem(cords,null);
            DestroyNode();
        }
    }
    public void Drop(){
        //todo
    }
}
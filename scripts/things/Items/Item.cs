namespace Atomation.Things;

using Resources;
using StatSystem;
using Godot;
using Atomation.GameMap;
using System.Collections.Generic;
using System;
using Atomation.InventorySystems;

/// <summary>
/// items are things that are held in the inventories of things in the game.
/// items are gotten from resources, crafting or other means. and used in a
/// variety of things
/// 
/// Todo make item disappear/get delete if currentStackSize = 0
/// </summary>
public partial class Item : Thing
{
    private int stackLimit;

    private int currentStackSize;

    public Item(Vector2 position)
    {
        graphic = new Graphic();

        graphic.Position = position;
        currentStackSize = 1;
    }

    public Item(Item item)
    {
        graphic = new Graphic();

        Configure(item.id);
    }

    public int StackLimit { get => stackLimit; set => stackLimit = value; }
    public int CurrentStackSize { get => currentStackSize; set => currentStackSize = value; }

    public override void Configure(string defId)
    {
        base.Configure(ThingDefDatabase.Instance.GetItemDef(defId), defId);
    }

    /// <summary>
    /// adds the amount provided to stack. if over stack limit returns
    /// the remainder
    /// </summary>
    public int AddToStack(int amount)
    {
        int remainder = amount;

        if (Stackable())
        {
            currentStackSize += amount;

            if (currentStackSize > stackLimit)
            {
                remainder = currentStackSize - stackLimit;
                currentStackSize = stackLimit;
                if (remainder < 0)
                {
                    remainder = 0;
                }
            }
        }

        return remainder;
    }

    /// <summary>
    /// combines the items current stack amount with the item stack amount passed in
    /// </summary>
    public int AddToStack(Item item)
    {
        int remainder = AddToStack(item.CurrentStackSize);
        item.CurrentStackSize = remainder;
        return remainder;
    }

    /// <summary> 
    /// attempts removes the provided amount otherwise returns
    /// the total amount removed
    /// </summary>
    public int RemoveAmount(int amount)
    {
        int remainder = 0;

        if (amount > currentStackSize)
        {
            amount -= currentStackSize;
            CurrentStackSize = 0;
            remainder = amount;
        }
        else
        {
            currentStackSize -= amount;
        }

        return remainder;
    }

    /// <summary>
    /// returns true if the item is stackable
    /// </summary>
    public bool Stackable()
    {
        if (stackLimit > 1 && currentStackSize < stackLimit)
        {
            return true;
        }

        return false;
    }

    public void PickUpItem(Inventory inventory)
    {
        GD.Print("Item picked up");

        //todo inventory

        if (currentStackSize == 0)
        {
            chunk.RemoveGridObject<Item>(Node.Position.GlobalToMap(), gridLayer);
            DestroyNode();
        }
    }

    public override Dictionary<string, object> FormatThingDef()
    {
        Dictionary<string, object> thingDef = base.FormatThingDef();
        thingDef.Add("StackLimit", stackLimit);

        return thingDef;
    }

    public override void ConfigureFromDef(Dictionary<string, object> def)
    {
        base.ConfigureFromDef(def);
        if (gridLayer == -1)
        {
            gridLayer = GameLayers.Items;
        }
        stackLimit = def.ContainsKey("StackLimit") ? Convert.ToInt32(def["StackLimit"]) : 1;
    }

    public override void Save()
    {
        GD.Print("saving of things not implemented");
    }

    public override void Load()
    {
        GD.Print("loading of things not implemented");

    }
}
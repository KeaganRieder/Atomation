namespace Atomation.Things;

using Resources;
using StatSystem;
using Godot;
using Atomation.GameMap;

/// <summary>
/// items are things that are held in the inventories of things in the game.
/// items are gotten from resources, crafting or other means. and used in a
/// variety of things
/// 
/// Todo make item disappear/get delete if currentStackSize = 0
/// </summary>
public class Item : Thing
{
    private int stackLimit;

    private int currentStackSize;

    public Item(Vector2 position)
    {
        graphic = new Graphic();

        Position = position * Map.CELL_SIZE;
        currentStackSize = 1;
    }

    public int StackLimit { get => stackLimit; set => stackLimit = value; }
    public int CurrentStackSize { get => currentStackSize; set => currentStackSize = value; }

    public void Configure(ItemDef itemDef, bool loading = false)
    {
        name = itemDef.DefName;
        description = itemDef.Description;

        stackLimit = itemDef.StackLimit;
        gridLayer = itemDef.GridLayer;
        
        graphic.Configure(itemDef.GraphicData);
        graphic.Name = name + Position;

        if (!loading)
        {
            statSheet = new StatSheet(itemDef.StatSheet, this);
        }
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
    /// returns if the item is stackable
    /// </summary>
    /// <returns></returns>
    public bool Stackable()
    {
        if (stackLimit > 1 && currentStackSize < stackLimit)
        {
            return true;
        }

        return false;
    }

    public void PickUpItem()
    {
        GD.Print("Item picked up");

        //todo inventory

        if (currentStackSize == 0)
        {
            chunk.RemoveStructure(Position.GlobalToMap());
            DestroyNode();
        }
    }

    public void DropItem()
    {
        GD.Print("drop implementation required");
    }
}
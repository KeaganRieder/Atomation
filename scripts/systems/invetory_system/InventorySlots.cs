namespace Atomation.InventorySystems;

using Godot;
using Atomation.Ui;
using Atomation.Things;

/// <summary>
/// slots are what make up the inventory. they hold individual stacks of items
/// and are what the inventory is divided up into
/// </summary>
public partial class InventorySlot : Panel
{
    private Item item;

    private Label itemCount;
    private float textSize;

    private Inventory parentInventory;

    public InventorySlot(Inventory inventory, Vector2 position)
    {
        parentInventory = inventory;
        Position = position;

        CustomMinimumSize = Vector2.One * Inventory.SLOT_SIZE;

        itemCount = new Label
        {
            CustomMinimumSize = Vector2.One * Inventory.SLOT_SIZE,
            Text = "",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
            Position = new Vector2(-2, 2),
            // ZIndex = 1,
        };

        itemCount.AddThemeColorOverride("font_shadow_color", Colors.Black);
        itemCount.AddThemeConstantOverride("shadow_outline_size", 5);
        itemCount.AddThemeFontSizeOverride("font_size", 16);

        AddChild(itemCount);
    }

    private void SetUpGUiInput()
    {
        //todo
    }

    public bool CanAdd(Item item)
    {
        if (item == null)
        {
            return true;
        }

        return item.Stackable() && item.Name == item.Name;
    }

    /// <summary>
    /// adds item to slot, either adding to existing stack or
    /// starting a new one
    /// </summary>
    public void AddItem(Item toAdd)
    {
        if (item == null)
        {
            SetSlotsItem(toAdd);
        }
        else
        {
            item.AddToStack(toAdd);
        }

        UpdateItemCount();
    }

    /// <summary>
    /// sets item of slot
    /// </summary>
    private void SetSlotsItem(Item toAdd)
    {
        item = new Item(toAdd);

        Vector2I size = new Vector2I(Mathf.RoundToInt(CustomMinimumSize.X / 2), Mathf.RoundToInt(CustomMinimumSize.Y / 2));
        item.Graphic.GraphicSize = size;
        item.Position = size; // center it onto the inventory

        item.AddToStack(toAdd.CurrentStackSize);
        AddChild(item);

        toAdd.CurrentStackSize = 0;
        toAdd.DestroyNode();
    }

    /// <summary>
    /// removes the provided amount of items form slot
    /// </summary>
    public Item RemoveItem(int amount)
    {
        //work on
        Item removedItem = null;

        if (item == null)
        {
            return removedItem;
        }
        item.RemoveAmount(amount);

        removedItem = new Item(item);
        removedItem.CurrentStackSize = amount;

        if (item.CurrentStackSize == 0)
        {
            item.DestroyNode();
            item = null;
        }
        UpdateItemCount();

        return removedItem;
    }

    public Item GetItem()
    {
        return item;
    }

    public Label GetItemCount()
    {
        return itemCount;
    }

    private void UpdateItemCount()
    {
        if (item == null)
        {
            itemCount.Text = $"";
        }
        else if (item.CurrentStackSize <= 1)
        {
            itemCount.Text = $"";
        }
        else
        {
            itemCount.Text = $"{item.CurrentStackSize}";
        }
    }

    private void HandleLeftClick()
    {
        //todo
    }

    private void HandleRightCLick()
    {
        //todo
    }
}
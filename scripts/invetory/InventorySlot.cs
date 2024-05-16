namespace Atomation.Systems;

using Atomation.Map;
using Atomation.Resources;
using Atomation.Things;
using Godot;

public partial class InventorySlot : Panel
{
    private Item slotItem;

    private Label itemCount;
    private float textSize;

    private Inventory inventory;
    private Vector2 pos;

    public InventorySlot(Inventory inv, Node parent,Vector2 position)
    {
        inventory = inv;
        pos = position;
        parent.AddChild(this);

        CustomMinimumSize = Vector2.One * Inventory.SLOT_SIZE;
        itemCount = new Label
        {
            CustomMinimumSize = Vector2.One * Inventory.SLOT_SIZE,
            Text = "",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
            Position = new Vector2(-2, 2),
            ZIndex = 1,
        };
        itemCount.AddThemeColorOverride("font_shadow_color", Colors.Black);
        itemCount.AddThemeConstantOverride("shadow_outline_size", 5);
        itemCount.AddThemeFontSizeOverride("font_size", 16);

        AddChild(itemCount);

        
    }

    private void SetUpGUiInput(){

    }

    public bool CanAdd(Item item)
    {
        if (slotItem == null)
        {
            return true;
        }

        return slotItem.Stackable() && item.GetName() == slotItem.GetName();
    }

    /// <summary>
    /// adds item to slot, either adding to existing stack or
    /// starting a new one
    /// </summary>
    public void AddItem(Item toAdd)
    {
        if (slotItem == null)
        {
            SetItem(toAdd);
        }
        else
        {
            slotItem.AddAmount(toAdd);
        }

        UpdateItemCount();
    }

    /// <summary>
    /// sets item of slot
    /// </summary>
    private void SetItem(Item toAdd)
    {
        slotItem = new Item(toAdd);

        Vector2I size = new Vector2I(Mathf.RoundToInt(CustomMinimumSize.X / 2), Mathf.RoundToInt(CustomMinimumSize.Y / 2));
        slotItem.GetNode().SetSize(size);
        slotItem.GetNode().Position = size;

        AddChild(slotItem.GetNode());
        toAdd.SetQuantity(0);
    }

    /// <summary>
    /// removes the provided amount of items form slot
    /// </summary>
    public Item RemoveItem(int amount)
    {
        //work on
        Item removedItem = null;

        if (slotItem == null)
        {
            return removedItem;
        }
        slotItem.RemoveAmount(amount);

        removedItem = new Item(slotItem);
        removedItem.SetQuantity(amount);

        if (slotItem.GetQuantity() == 0)
        {
            slotItem.GetNode().QueueFree();
            slotItem = null;
        }
        UpdateItemCount();

        return removedItem;
    }

    public Item GetItem()
    {
        return slotItem;
    }

    public Label GetItemCount()
    {
        return itemCount;
    }

    private void UpdateItemCount()
    {
        if (slotItem == null)
        {
            itemCount.Text = $"";
        }
        else if (slotItem.GetQuantity() <= 1)
        {
            itemCount.Text = $"";
        }
        else
        {
            itemCount.Text = $"{slotItem.GetQuantity()}";
        }
    }

    private void HandleLeftClick(){

    }
    
    private void HandleRightCLick(){
        
    }
}
namespace Atomation.Systems;

using Atomation.Resources;
using Atomation.Things;
using Atomation.Ui;
using Godot;

public partial class Inventory : GameUI
{
    public const int SLOT_SIZE = 32;

    private int padding = 2;
    private int rows;
    private int columns;

    private InventorySlot[,] slots;
     
    private LayoutPreset layoutPreset;
    private PanelContainer slotContainer;
    private GridContainer gridContainer;

    public Inventory(Node2D parent, int columns = 10, int rows = 10, int padding = 10) : base()
    {
        Name = "Inventory";
        this.columns = columns;
        this.rows = rows;
        this.padding = padding;
        parent.AddChild(this);

        slotContainer = new PanelContainer();
        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(padding);
        slotContainer.AddChild(marginContainer);

        gridContainer = new GridContainer { Columns = columns };
        marginContainer.AddChild(gridContainer);

        InitializeSlots();

        AddChild(slotContainer);
        SetAnchor(LayoutPreset.CenterTop);

        Close();
    }

    private void InitializeSlots()
    {
        slots = new InventorySlot[rows, columns];
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                InventorySlot slot = new InventorySlot(this, gridContainer,new Vector2(x,y));
                slots[x, y] = slot;
            }
        }
    }
    public override void SetAnchor(LayoutPreset preset)
    {
        layoutPreset = preset;
        base.SetAnchor(preset);
        slotContainer.SetAnchorsAndOffsetsPreset(preset);
    }

    /// <summary>
    /// sets item at slot if it works
    /// </summary>
    public void SetItem(int x, int y, Item item)
    {
        //work on
        if (x >= 0 && y >= 0 && x < rows && y < columns)
        {
            slots[x, y].AddItem(item);
        }
        else
        {
            GD.PushError($"{x},{y} are out of bounds for current inventory");
        }
    }

    /// <summary>
    /// adds item to inventory, first to any slots that contains the item and are stackable
    /// and then moving through the inventory tell it finds a free slot
    /// </summary>
    public void AddItem(Item item)
    {
        AddToExistingSlot(item);

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (item.GetQuantity() == 0)
                {
                    return;
                }
                if (slots[x, y].CanAdd(item))
                {
                    slots[x, y].AddItem(item);
                }
            }
        }
    }

    /// <summary>
    /// adds item to any slot that it's in and can more of
    /// </summary>
    private void AddToExistingSlot(Item item)
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                if (item.GetQuantity() == 0)
                {
                    return;
                }

                if (slots[x, y].GetItem() != null && slots[x, y].CanAdd(item))
                {
                    slots[x, y].AddItem(item);
                }
            }
        }
    }
    /// <summary>
    /// removes item at slot
    /// </summary>
    public void RemoveItem(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < rows && y < columns)
        {
            
        }
        else
        {
            GD.PushError($"{x},{y} are out of bounds for current inventory");
        }
    }

    public Item GetItem(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < rows && y < columns)
        {
            return slots[x, y].GetItem();
        }
        else
        {
            GD.PushError($"{x},{y} are out of bounds for current inventory");
            return null;
        }
    }
    public InventorySlot GetSlot(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < rows && y < columns)
        {
            return slots[x, y];
        }
        else
        {
            GD.PushError($"{x},{y} are out of bounds for current inventory");
            return null;
        }
    }

}
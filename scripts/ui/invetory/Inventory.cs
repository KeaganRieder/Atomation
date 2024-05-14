namespace Atomation.PlayerChar;

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
                InventorySlot slot = new InventorySlot(this, gridContainer);
                slots[x, y] = slot;
            }
        }
    }

    public void SetAnchor(LayoutPreset preset)
    {
        layoutPreset = preset;
        SetAnchorsAndOffsetsPreset(preset);
        slotContainer.SetAnchorsAndOffsetsPreset(preset);
    }

    public void SetItem(int x, int y, Item item)
    {
        slots[x,y].SetItem(item);
    }

    public void Sort(){
        GD.Print("Sorting inventory needing implementation");

    }

    public void TakeAll(){
        GD.Print("Take All Implementation is needed");
    }


}
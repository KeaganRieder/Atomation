namespace Atomation.Player;

using Atomation.Resources;
using Atomation.Ui;

using System.Collections.Generic;
using Godot;

public partial class Inventory : Control
{
    public const int SLOT_SIZE = 16;

    public bool IsOpen { get; private set; }


    private int padding = 2;
    private int rows;
    private int columns;

    private InventorySlot[,] slots;

    private LayoutPreset layoutPreset;
    private PanelContainer slotContainer;
    private StyleBox inventoryGraphic; //todo
    private GridContainer gridContainer; //todo

    public Inventory(int columns = 10, int rows = 10, int padding = 10)
    {
        Name = "Inventory";
        this.columns = columns;
        this.rows = rows;
        this.padding = padding;

        slotContainer = new PanelContainer();
        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(padding);
        slotContainer.AddChild(marginContainer);

        gridContainer = new GridContainer {Columns = columns};
        marginContainer.AddChild(gridContainer);

        InitializeSlots();

        AddChild(slotContainer);
        SetAnchor(LayoutPreset.CenterTop);

        // Close();
    }

    private void InitializeSlots()
    {
        slots = new InventorySlot[rows, columns];
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                InventorySlot slot = new InventorySlot(this);
                gridContainer.AddChild(slot);
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

    public void Open()
    {
        Visible = true;
        IsOpen = true;
    }

    public void Close()
    {
        Visible = false;
        IsOpen = false;
    }


}
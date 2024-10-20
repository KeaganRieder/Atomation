namespace Atomation.InventorySystems;

using Godot;
using Atomation.Ui;

/// <summary>
/// defines the storage system of items by object and player in the game
/// this also is a graphical display of current contents in the inventory
/// </summary>
public partial class Inventory : UserInterface
{
    public const int SLOT_SIZE = 32; //maybe change when it comes to scaling?

    private Vector2 inventorySize;
    private int rows;
    private int columns;
    private InventorySlot[,] slots;

    private GridContainer slotContainer;

    public Inventory(string parentName, int columns = 10, int rows = 10, int padding = 10)
    : base(null, GameLayers.Ui, padding)
    {
        Name = parentName + "_" + "Inventory";

        this.rows = rows;
        this.columns = columns;

        // CreateUIElements();      
    }

    protected override void CreateUIElements()
    {
        background = new PanelContainer();
        background.CustomMinimumSize = new Vector2(300, 300);

        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(uiPadding);
        background.AddChild(marginContainer);

        slotContainer = new GridContainer() { Columns = columns };
        marginContainer.AddChild(slotContainer);

        slots = new InventorySlot[rows, columns];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                InventorySlot slot = new InventorySlot(this, new Vector2(x, y));
                slots[x, y] = slot;
                slotContainer.AddChild(slot);
            }
        }

        AddChild(background);
        SetAnchor(LayoutPreset.Center);

        base.CreateUIElements();
    }

    public override void ToggleUI()
    {
        SetToCurrentAnchor();

        base.ToggleUI();
    }

    public override void SetAnchor(LayoutPreset preset)
    {
        base.SetAnchor(preset);

        slotContainer.SetAnchorsAndOffsetsPreset(layoutPreset);
    }


}

namespace Atomation.PlayerChar;

using Atomation.Map;
using Atomation.Resources;
using Atomation.Things;
using Godot;

public partial class InventorySlot : Panel
{
    private Item item;

    private Label itemCount;
    private float textSize;

    private Inventory inventory;

    public InventorySlot(Inventory inv, Node parent)
    {
        inventory = inv;
        parent.AddChild(this);

        CustomMinimumSize = Vector2.One * Inventory.SLOT_SIZE;
        itemCount = new Label
        {
            CustomMinimumSize = Vector2.One * Inventory.SLOT_SIZE,
            Text = "00",
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
            Position = new Vector2(-2, 2),
            ZIndex = 1,
        };
        itemCount.AddThemeColorOverride("font_shadow_color",Colors.Black);
        itemCount.AddThemeConstantOverride("shadow_outline_size",5);
        itemCount.AddThemeFontSizeOverride("font_size", 16);

        AddChild(itemCount);
    }

    public void SetItem(Item toAdd)
    {
        item = new Item(toAdd,true);
        toAdd.DestroyNode();

        Vector2I size = new Vector2I(Mathf.RoundToInt(CustomMinimumSize.X / 2), Mathf.RoundToInt(CustomMinimumSize.Y / 2));
        item.GetGraphic().SetSize(size);
        item.GetGraphic().Position = size;

        itemCount.Text = $"{item.GetQuantity()}";


        AddChild(item.GetGraphic());
    }

    public Item GetItem()
    {
        return item;
    }
    public void RemoveItem()
    {

    }
    public void TransferItem()
    {
        if (item == null)
        {
            return;
        }
    }





}

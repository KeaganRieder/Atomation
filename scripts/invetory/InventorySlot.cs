namespace Atomation.PlayerChar;

using Atomation.Map;
using Atomation.Resources;
using Atomation.Things;
using Godot;

public partial class InventorySlot : Panel
{
    private Item item;

    private Inventory inventory;

    public InventorySlot(Inventory inv)
    {
        inventory = inv;

        CustomMinimumSize = Vector2.One * Inventory.SLOT_SIZE;
        // Size= new Vector2I(Inventory.SLOT_SIZE, )
    }




}

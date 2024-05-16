namespace Atomation.Systems;

using System.Collections.Generic;
using Atomation.Map;
using Atomation.PlayerChar;
using Atomation.Things;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

[TestSuite]
public class TestInventory
{
    [TestCase]
    public void TestAddingItems()
    {
        Item item = new Item(new Coordinate(Vector2.Zero));
        item.Configure(ItemDef.TestDef());
        item.SetQuantity(10);

        Item item2 = new Item(new Coordinate(Vector2.Zero));
        item2.Configure(ItemDef.TestDef());
        item2.SetQuantity(5);

        AssertThat(item.GetQuantity()).IsEqual(10);
        AssertThat(item2.GetQuantity()).IsEqual(5);

        item2.AddAmount(item);
        AssertThat(item.GetQuantity()).IsEqual(5);
        AssertThat(item2.GetQuantity()).IsEqual(10);

        item2.DestroyNode();
        item.DestroyNode();
    }

    [TestCase]
    public void TestAddingToInventory()
    {
        Node2D parent = AutoFree(new Node2D());
        Inventory inventory = AutoFree(new Inventory(parent));

        AssertThat(inventory.GetSlot(0, 0)).IsNotNull();
        AssertThat(inventory.GetItem(0, 0)).IsNull();

        Item item = new Item(new Coordinate(Vector2.Zero));
        item.Configure(ItemDef.TestDef());

        AssertThat(inventory.GetSlot(0, 0).CanAdd(item)).IsEqual(true);

        AssertThat(item.GetQuantity()).IsEqual(1);
        AssertThat(item).IsNotNull();
        inventory.AddItem(item);
        AssertThat(item.GetQuantity()).IsEqual(0);
        AssertThat(inventory.GetItem(0, 0).GetName()).IsEqual("Test Item");
        AssertThat(inventory.GetItem(0, 0).GetQuantity()).IsEqual(1);
        item.DestroyNode();

        item = new Item(new Coordinate(Vector2.Zero));
        item.Configure(ItemDef.TestDef());
        item.SetQuantity(10);

        AssertThat(item.GetQuantity()).IsEqual(10);

        AssertThat(inventory.GetItem(0, 0).GetQuantity()).IsEqual(1);
        AssertThat(inventory.GetSlot(0, 0).CanAdd(item)).IsEqual(true);

        inventory.AddItem(item);
        AssertThat(inventory.GetItem(0, 0).GetQuantity()).IsEqual(10);

        AssertThat(inventory.GetItem(0, 1)).IsNotNull();
        AssertThat(inventory.GetItem(0, 1).GetName()).IsEqual("Test Item");
        AssertThat(inventory.GetItem(0, 1).GetQuantity()).IsEqual(1);
        
        AssertThat(item.GetQuantity()).IsEqual(0);
        item.DestroyNode();
    }
}

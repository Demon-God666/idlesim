using System.Collections.Generic;
using Godot;
using IdleSim.scenes.auto_cooks.components;
using IdleSim.scenes.inventory.components;
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.inventory;

public partial class Inventory : Control
{
    [Signal]
    public delegate void InventoryUpdatedEventHandler();

    public List<BoughtItem> InventoryItems { get; } = [];
    private GridContainer _inventoryItemContainer;
    private Control _inventoryItemTemplate;


    public override void _Ready()
    {
        _inventoryItemContainer = GetNode<GridContainer>("ScrollContainer/GridContainer");
        _inventoryItemTemplate = GetNode<Control>("Templates/InventoryItem");
        
        EmitSignal(SignalName.InventoryUpdated);
        InventoryUpdated += UpdateInventory;
    }

    private void UpdateInventory()
    {
        foreach (var child in _inventoryItemContainer.GetChildren())
            child.QueueFree();

        foreach (var item in InventoryItems)
        {
            var inventoryItem = (InventoryItem)_inventoryItemTemplate.Duplicate();
            _inventoryItemContainer.AddChild(inventoryItem);

            inventoryItem.SetInventoryItem(item);
        }
    }
    
    public void Add(ItemData item, int amount)
    {
        var checkExistingItem = InventoryItems.Find(x => x.Item == item);

        if (checkExistingItem != null)
            checkExistingItem.Amount += amount;
        else
            InventoryItems.Add(new BoughtItem(item, amount));

        EmitSignal(SignalName.InventoryUpdated);
    }

    private void PrintInventory()
    {
        GD.Print("Inventory:");
        foreach (var item in InventoryItems) GD.Print($"{item.Item.ProductName}: {item.Amount}");
    }

    public void Remove(ItemData item, int amount)
    {
        var findItem = InventoryItems.Find(x => x.Item == item);

        if (findItem != null) findItem.Amount -= amount;
    }

    public bool CheckIngredientAvailable(List<IngredientList> ingredientList)
    {
        foreach (var ingredient in ingredientList)
        {
            var checkItem = InventoryItems.Find(x => x.Item.ProductName == ingredient.ItemName);

            if (checkItem == null || checkItem.Amount < ingredient.ItemAmount) return false;
        }

        return true;
    }
}
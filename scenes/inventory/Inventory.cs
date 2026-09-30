using System.Collections.Generic;
using Godot;
using IdleSim.scenes.auto_cooks.components;
using IdleSim.scenes.inventory.components;
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.inventory;

public partial class Inventory : Control
{
	public List<BoughtItem> InventoryItems { get; set; } = new();
	
	[Signal]
	public delegate void InventoryUpdatedEventHandler();

	private InventoryItem _inventoryItem;
	private GridContainer _inventoryItemsContainer;
	
	public override void _Ready()
	{
		_inventoryItem = GetNode<InventoryItem>("Templates/InventoryItem");
		_inventoryItemsContainer = GetNode<GridContainer>("ScrollContainer/GridContainer");
		
		EmitSignal(SignalName.InventoryUpdated);
		
		LoadInventoryItems();
	}

	public void Add(ItemData item, int amount)
	{
		BoughtItem checkExistingItem = InventoryItems.Find(x => x.Item == item);
		
		if (checkExistingItem != null)
		{
			checkExistingItem.Amount += amount;
		}
		else
		{
			InventoryItems.Add(new BoughtItem(item, amount));
		}

		LoadInventoryItems();
		EmitSignal(SignalName.InventoryUpdated);
	}
	
	private void PrintInventory()
	{
		GD.Print("Inventory:");
		foreach (BoughtItem item in InventoryItems)
		{
			GD.Print($"{item.Item.ProductName}: {item.Amount}");
		}
		
	}
	
	public void Remove(ItemData item, int amount)
	{
		BoughtItem findItem = InventoryItems.Find(x => x.Item == item);

		if (findItem != null)
		{
			findItem.Amount -= amount;
		}
	}
	
	public bool CheckIngredientAvailable(List<IngredientList> ingredientList)
	{
		foreach (var ingredient in ingredientList)
		{
			var checkItem = InventoryItems.Find(
				x => x.Item.ProductName == ingredient.ItemName
			);

			if (checkItem == null || checkItem.Amount < ingredient.ItemAmount)
			{
				return false;
			}
		}
		
		return true;
	}
	
	public void LoadInventoryItems()
	{
		foreach (var item in _inventoryItemsContainer.GetChildren())
		{
			item.QueueFree();	
		}
		
		foreach (var item in InventoryItems)
		{
			var inventoryItemChild = (InventoryItem)_inventoryItem.Duplicate();

			_inventoryItemsContainer.AddChild(inventoryItemChild);
			inventoryItemChild.SetInventoryItem(item);
		}
	}
	
}

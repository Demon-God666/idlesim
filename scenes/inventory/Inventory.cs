using System.Collections.Generic;
using Godot;
using IdleSim.scenes.inventory.components;
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.inventory;

public partial class Inventory : Control
{
	public List<BoughtItem> InventoryItems { get; set; } = new();
	
	private Button _showInventoryButton;
	
	[Signal]
	public delegate void InventoryUpdatedEventHandler();
	
	
	public override void _Ready()
	{
		_showInventoryButton = GetNode<Button>("Control/ShowInventoryButton");
		_showInventoryButton.Pressed += PrintInventory;
		
		EmitSignal(SignalName.InventoryUpdated);
	}

	public void Add(ShopItemData item, int amount)
	{
		BoughtItem checkExistingItem = InventoryItems.Find(x => x.Item == item);
		
		if (checkExistingItem != null)
		{
			checkExistingItem.Amount += amount;
			return;
		}
		
		InventoryItems.Add(new BoughtItem(item, amount));
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
	
	
}

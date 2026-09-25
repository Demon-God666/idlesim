using Godot;
using System;
using System.Collections.Generic;
using IdleSim.scenes.auto_cooks.components;
using IdleSim.scenes.inventory;
using IdleSim.scenes.inventory.components;

public partial class AutoCookItem : Control
{
	private bool _isCooking;
	private Label _ingredientList;
	private Label _produceItem;
	private Label _producedProductValue;
	private Label _produceTime;
	private Inventory _inventory;
	private Dishes _dish;
	
	public override void _Ready()
	{
		_inventory = GetNode<Inventory>("../../../Inventory");
		
		_ingredientList = GetNode<Label>("VBoxContainer/IngredientListLabel");
		_produceItem = GetNode<Label>("VBoxContainer/ProduceItemLabel");
		_producedProductValue = GetNode<Label>("VBoxContainer/ProducedProductValueLabel");
		_produceTime = GetNode<Label>("VBoxContainer/ProduceTimeLabel");
		
		_ingredientList.Text = "Ingredients: \n";
		_produceItem.Text = "Produce: \n";
		_producedProductValue.Text = "Value: \n";
		_produceTime.Text = "Time: \n";
		
		_inventory.InventoryUpdated += UpdateInventory;
	}
	
	public void SetAutoCookItem(Dishes dish)
	{
		_dish = dish;
		
		_produceItem.Text += dish.ProduceItem;
		_producedProductValue.Text += dish.ProducedProductValue + "$";
		_produceTime.Text += dish.ProduceTime + "s";
		
		UpdateInventory();
	}
	
	private int GetInventoryItemCount(string itemName)
	{
		var itemIndex = _inventory.InventoryItems.FindIndex(i => i.Item.ProductName == itemName);
		if (itemIndex < 0)
			return 0;
		
		var itemAmount = _inventory.InventoryItems[itemIndex];
		GD.Print(itemAmount);
		return itemAmount?.Amount ?? 0;
	}

	private void UpdateInventory()
	{
		if (_dish == null)
			return;

		_ingredientList.Text = "Ingredients: \n";
		
		foreach (var ingredient in _dish.IngredientList)
		{
			_ingredientList.Text += $"{ingredient.ItemName} {ingredient.ItemAmount}/ {GetInventoryItemCount(ingredient.ItemName)}";
			if (_dish.IngredientList.Count > 1 && _dish.IngredientList.IndexOf(ingredient) < _dish.IngredientList.Count - 1)
			{
				_ingredientList.Text += "\n";
			}
		}
	}
}

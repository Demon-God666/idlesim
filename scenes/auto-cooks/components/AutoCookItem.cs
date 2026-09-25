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
	}
	
	public void SetAutoCookItem(Dishes dish)
	{
		foreach (var ingredient in dish.IngredientList)
		{
			_ingredientList.Text += $"{ingredient.ItemName} {ingredient.ItemAmount}/ {GetInventoryItemCount(ingredient.ItemName)}";
			if (dish.IngredientList.Count > 1 && dish.IngredientList.IndexOf(ingredient) < dish.IngredientList.Count - 1)
			{
				_ingredientList.Text += "\n";
			}
		}
		
		_produceItem.Text += dish.ProduceItem;
		_producedProductValue.Text += dish.ProducedProductValue + "$";
		_produceTime.Text += dish.ProduceTime + "s";
		
		GD.Print(_inventory.InventoryItems.Count);
	}
	
	private int GetInventoryItemCount(string itemName)
	{
		var item = _inventory.InventoryItems.Find(i => i.Item.ProductName == itemName);
		return item == null ? 0 : item.Amount;
	}
}

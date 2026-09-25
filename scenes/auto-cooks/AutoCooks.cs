using Godot;
using System;
using System.Collections.Generic;
using IdleSim.scenes.auto_cooks.components;
using IdleSim.scenes.shop.components;

public partial class AutoCooks : Control
{
	private List<Dishes> _autoCookDishes = new();
	private Shop _shop;

	private GridContainer _autoCookItemContainer;
	private AutoCookItem _autoCookItemTemplate;
	
	public override void _Ready()
	{
		_shop = GetTree().Root.GetNode<Shop>("Main/Shop");
		_autoCookItemContainer = GetNode<GridContainer>("AutoCookItemContainer");
		_autoCookItemTemplate = GetNode<AutoCookItem>("AutoCookItemTemplate/AutoCookItem");
		
		AddDish(
			new List<string> { "Milk", "Water" },
			new List<int> { 1, 2 },
			FormatDishData("Milk Rice", 20, "MilkRice.png", 0),
			10
		);

		AddDish(
			new List<string> { "Water" },
			new List<int> { 1 },
			FormatDishData("Water Rice", 20, "WaterRice.png", 0),
			10
		);

		AddDish(
			new List<string> { "Sugar" },
			new List<int> { 1 },
			FormatDishData("Sugar Rice", 20, "SugarRice.png", 0),
			10
		);

		AddDish(
			new List<string> { "Salt" },
			new List<int> { 1 },
			FormatDishData("Salt Rice", 20, "SaltRice.png", 0),
			10
		);

		AddDish(
				new List<string> { "Flour" },
				new List<int> { 1 },
				FormatDishData("Flour Rice", 20, "FlourRice.png", 0),
				10
			);
		
		LoadAutoCooks();
	}

	private void AddDish(List<string> ingredientName, List<int> ingredientAmount, ItemData itemData, int produceTime)
	{
		var ingredients = new List<IngredientList>();

		for (int i = 0; i < ingredientName.Count; i++)
		{
			ingredients.Add(
				new IngredientList(
					ingredientName[i],
					ingredientAmount[i]
				)
			);
		}

		var dish = new Dishes(
			ingredients,
			itemData,
			produceTime
		);

		_autoCookDishes.Add(dish);

		GD.Print($"Dish: {dish.ItemData.ProductName}");
		GD.Print($"Im Shop: {_shop.HasItem(dish.ItemData)}");

	}

	private void LoadAutoCooks()
	{
		foreach (var child in _autoCookItemContainer.GetChildren())
		{
			child.QueueFree();
		}

		foreach (var dish in _autoCookDishes)
		{
			var autoCookItem = (AutoCookItem)_autoCookItemTemplate.Duplicate();
			
			_autoCookItemContainer.AddChild(autoCookItem);
			autoCookItem.SetAutoCookItem(dish);
		}
	}

	private ItemData FormatDishData(
		string productName,
		int productPrice,
		string productImagePath,
		int categoryId)
	{
		return _shop.AddItemToShop(
			productName,
			productPrice,
			productImagePath,
			categoryId
		);
	}
}

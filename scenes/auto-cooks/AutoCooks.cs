using Godot;
using System;
using System.Collections.Generic;
using IdleSim.scenes.auto_cooks.components;

public partial class AutoCooks : Control
{
	private List<Dishes> _autoCookDishes = new();

	private GridContainer _autoCookItemContainer;
	private AutoCookItem _autoCookItem;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_autoCookItemContainer = GetNode<GridContainer>("AutoCookItemContainer");
		_autoCookItem = GetNode<AutoCookItem>("AutoCookItemContainer/AutoCookItem");

		AddDish(
			new List<string> { "Milk" },
			new List<int> { 1 },
			"Milk Rice",
			20,
			10
		);
		AddDish(
			new List<string> { "Water" },
			new List<int> { 1 },
			"Water Rice",
			20,
			10
		);

		AddDish(
			new List<string> { "Sugar" },
			new List<int> { 1 },
			"Sugar Rice",
			20,
			10
		);

		AddDish(
			new List<string> { "Salt" },
			new List<int> { 1 },
			"Salt Rice",
			20,
			10
		);

		AddDish(
			new List<string> { "Flour" },
			new List<int> { 1 },
			"Flour Rice",
			20,
			10
		);
		
		LoadAutoCooks();
	}

	private void AddDish(List<string> ingredientName, List<int> ingredientAmount, string produceItem,
		int producedProductValue, int produceTime)
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

		_autoCookDishes.Add(new Dishes(
			ingredients,
			produceItem,
			producedProductValue,
			produceTime
		));
	}

	private void LoadAutoCooks()
	{
		foreach (var child in _autoCookItemContainer.GetChildren())
		{
			child.QueueFree();
		}

		foreach (var dish in _autoCookDishes)
		{
			var autoCookItem = _autoCookItem.Duplicate() as AutoCookItem;
			autoCookItem.SetAutoCookItem(dish);
			_autoCookItemContainer.AddChild(autoCookItem);
		}
	}
}

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Godot;
using IdleSim.scenes.auto_cooks.components;
using IdleSim.scenes.shop;
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.auto_cooks;

public partial class AutoCooks : Control
{
	private readonly List<Dish> _autoCookDishes = [];

	private GridContainer _autoCookItemContainer;
	private AutoCookItem _autoCookItemTemplate;

	private Shop _shop;

	public override void _Ready()
	{
		_shop = GetTree().Root.GetNode<Shop>("Main/Shop");
		_autoCookItemContainer = GetNode<GridContainer>("AutoCookItemContainer");
		_autoCookItemTemplate = GetNode<AutoCookItem>("AutoCookItemTemplate/AutoCookItem");

		LoadDishes();
		LoadAutoCooks();
	}

	private void AddDish(
		ItemData itemData,
		List<IngredientListJson> ingredientData,
		int produceTime)
	{
		var ingredients = ingredientData
			.Select(_ => new IngredientList(IngredientListJson.Name, IngredientListJson.Amount)).ToList();

		var dish = new Dish(
			ingredients,
			itemData,
			produceTime
		);

		_autoCookDishes.Add(dish);

		GD.Print($"Dish: {dish.ItemData.ProductName}");
		GD.Print($"In shop: {_shop.HasItem(dish.ItemData)}");
	}

	private void LoadAutoCooks()
	{
		foreach (var child in _autoCookItemContainer.GetChildren()) child.QueueFree();

		foreach (var dish in _autoCookDishes)
		{
			var autoCookItem = (AutoCookItem)_autoCookItemTemplate.Duplicate();

			_autoCookItemContainer.AddChild(autoCookItem);
			autoCookItem.SetAutoCookItem(dish);
		}
	}

	public void UpdateAllAutoCookItems()
	{
		foreach (var child in _autoCookItemContainer.GetChildren())
			if (child is AutoCookItem autoCookItem)
				autoCookItem.UpdateInventory();
	}

	private void LoadDishes()
	{
		var json = FileAccess.GetFileAsString("res://data/Dishes.json");
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		var dishes = JsonSerializer.Deserialize<List<DishesJson>>(json, options);

		foreach (var item in dishes)
		{
			var itemData = _shop.GetItemData().Find(x => x.ProductName == item.ItemData);

			AddDish(itemData, item.Ingredients, item.ProduceTime);
		}
	}
}

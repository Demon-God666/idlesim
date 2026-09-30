using Godot;
using System.Collections.Generic;
using System.Text.Json;
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

        LoadDishes();
        LoadAutoCooks();
    }

    private void AddDish(
        ItemData itemData,
        List<IngredientListJson> ingredientData,
        int produceTime)
    {
        var ingredients = new List<IngredientList>();

        foreach (var ingredient in ingredientData)
        {
            ingredients.Add(
                new IngredientList(
                    ingredient.Name,
                    ingredient.Amount
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

    public void UpdateAllAutoCookItems()
    {
        foreach (var child in _autoCookItemContainer.GetChildren())
        {
            if (child is AutoCookItem autoCookItem)
            {
                autoCookItem.UpdateInventory();
            }
        }
    }

    private void LoadDishes()
    {
        var json = FileAccess.GetFileAsString("res://data/Dishes.json");
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        var dishes = JsonSerializer.Deserialize<List<DishesJson>>(json, options);

        foreach (var item in dishes)
        {
            var itemData = _shop.GetItemData().Find(x => x.ProductName == item.ItemData
            );

            AddDish(itemData, item.Ingredients, item.ProduceTime);
        }
    }
}
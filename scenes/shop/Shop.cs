using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Godot;
using IdleSim.scenes.currency_system;
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.shop;

public partial class Shop : Control
{
    private CurrencySystem _currencySystem;
    private int _money;
    private readonly List<ItemData> _items = [];
    private GridContainer _gridContainer;
    private PageSwitcher _pageSwitcher;
    private int _itemCountWithoutDishes;

    private string _imagePath = "res://assets/images/";

    public override void _Ready()
    {
        _pageSwitcher = GetNode<PageSwitcher>("ColorRect/PageSwitcher");

        _gridContainer = GetNode<GridContainer>("ColorRect/ShopCatalog/HBoxContainer/CatalogRect/BoxContainer");
        foreach (var child in _gridContainer.GetChildren())
        {
            child.QueueFree();
        }

        _currencySystem = GetNode<CurrencySystem>("../CurrencySystem");
        _currencySystem.MoneyUpdated += UpdateMoney;

        _money = _currencySystem.GetMoney();

        LoadItemData();

        GetItemCountWithoutDishes(_items);
        _pageSwitcher.GetMaxPage(_itemCountWithoutDishes);

        foreach (var item in _items)
        {
            var shopItem = GD.Load<PackedScene>("res://scenes/shop/components/ShopItem.tscn")
                .Instantiate<ShopItem>();
            _gridContainer.AddChild(shopItem);
            shopItem.SetItem(item);
        }

        DisplayShopItems();
    }

    private void UpdateMoney(int value)
    {
        _money = value;
    }

    private int GetMoney()
    {
        return _money;
    }

    private void AddItemToShop(string productName,
        int productPrice,
        string productImagePath,
        int categoryId)
    {
        var itemData = new ItemData(
            productName,
            productPrice,
            GD.Load<Texture2D>(_imagePath + productImagePath),
            categoryId
        );

        _items.Add(itemData);
    }

    public void DisplayShopItems()
    {
        foreach (var child in _gridContainer.GetChildren())
        {
            child.QueueFree();
        }

        var startIndex = (_pageSwitcher.CurrentPage - 1) * 8;

        foreach (var item in _items
                     .Skip(startIndex)
                     .Take(8))
        {
            if (item.Category == 0) return;

            var shopItem = GD.Load<PackedScene>(
                "res://scenes/shop/components/ShopItem.tscn"
            ).Instantiate<ShopItem>();

            _gridContainer.AddChild(shopItem);
            shopItem.SetItem(item);
        }
    }

    public bool HasItem(ItemData itemData)
    {
        return _items.Contains(itemData);
    }

    private void LoadItemData()
    {
        var json = FileAccess.GetFileAsString("res://data/ItemData.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var items = JsonSerializer.Deserialize<List<ItemDataJson>>(json, options);

        foreach (var _ in items)
        {
            AddItemToShop(ItemDataJson.Name, ItemDataJson.Price, ItemDataJson.Image, ItemDataJson.Category);
        }
    }

    private void GetItemCountWithoutDishes(List<ItemData> items)
    {
        foreach (var _ in items.Where(item => item.Category != 0))
        {
            _itemCountWithoutDishes++;
        }
    }

    public List<ItemData> GetItemData()
    {
        return _items;
    }
}
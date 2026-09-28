using System.Collections.Generic;
using Godot;
using IdleSim.scenes.shop.components;
using System.Linq;
using System.Text.Json;

public partial class Shop : Control
{
	private CurrencySystem _currencySystem;
	private int _money;
	private List<ItemData> _items = new();
	private GridContainer _gridContainer;
	private PageSwitcher _pageSwitcher;
	private int _itemCountWithoutDishes; 
	
	private string _imagePath = "res://assets/images/";
	public override void _Ready()
	{
		
		_pageSwitcher = GetNode<PageSwitcher>("ColorRect/PageSwitcher");

		_gridContainer = GetNode<GridContainer>("ColorRect/ShopCatalog/HBoxContainer/CatalogRect/BoxContainer");	
		foreach (Node child in _gridContainer.GetChildren())
		{
			child.QueueFree();
		}
		
		_currencySystem = GetNode<CurrencySystem>("../CurrencySystem");
		_currencySystem.MoneyUpdated += UpdateMoney;
		
		_money = _currencySystem.GetMoney();
	
		LoadItemData();

		GetItemCountWithoutDishes(_items);
		_pageSwitcher.GetMaxPage(_itemCountWithoutDishes);
		
		foreach (ItemData item in _items )
		{
			ShopItem shopItem = GD.Load<PackedScene>("res://scenes/shop/components/ShopItem.tscn").Instantiate<ShopItem>();
			_gridContainer.AddChild(shopItem);
			shopItem.SetItem(item);
		}	
		DisplayShopItems();
	}
	
	private void UpdateMoney(int value)
	{
		_money = value;
	}

	public int GetMoney()
	{
		return _money;
	}
	
	public ItemData AddItemToShop(
		string productName,
		int productPrice,
		string productImagePath,
		int categoryId)
	{
		List<Category> categories = new()
		{
			new (0, "Dishes"),
			new(1, "Fruits and Vegetables"),
			new(2, "Bread"),
			new(3, "Milk Products")
		};

		Category category = categories.Find(c => c.CategoryId == categoryId);

		ItemData itemData = new ItemData(
			productName,
			productPrice,
			GD.Load<Texture2D>(_imagePath + productImagePath),
			category
		);

		_items.Add(itemData);
		
		return itemData;
	}

	public void DisplayShopItems()
	{
		
		foreach (Node child in _gridContainer.GetChildren())
		{
			child.QueueFree();
		}

		int startIndex = (_pageSwitcher.CurrentPage - 1) * 8;

		foreach (ItemData item in _items
					 .Skip(startIndex)
					 .Take(8))
		{
			if (item.Category.CategoryId == 0) return;
			
			ShopItem shopItem = GD.Load<PackedScene>(
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
		
		foreach (var item in items)
		{
			AddItemToShop(item.Name, item.Price, item.Image, item.Category);
		}
	}

	private void GetItemCountWithoutDishes(List<ItemData> items)
	{
		foreach (ItemData item in items)
		{
			if (item.Category.CategoryId != 0)
			{
				_itemCountWithoutDishes++;
			}
		}
	}
	
	public List<ItemData> GetItemData()
	{
		return _items;
	}
}

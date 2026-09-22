using System.Collections.Generic;
using Godot;
using IdleSim.scenes.shop.components;
using System.Linq;

public partial class Shop : Control
{
	private CurrencySystem _currencySystem;
	private int _money;
	private List<ShopItemData> _items = new();
	private GridContainer _gridContainer;
	private PageSwitcher _pageSwitcher;
	
	const string ImagePath = "res:///assets/images/";
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
	
		AddItemToShop("Milk", 10, "Milk.png",3);
		AddItemToShop("Cheese", 20, "Cheese.png",3);
		AddItemToShop("Eggs", 8, "Eggs.png",2);
		AddItemToShop("Butter", 15, "Butter.png",3);
		AddItemToShop("Bread", 12, "Bread.png", 2);
		AddItemToShop("Ham", 25, "Ham.png", 2);
		AddItemToShop("Bananas", 10, "Bananas.png", 1);
		AddItemToShop("Apples", 5, "Apples.png", 1);
		
		AddItemToShop("Oranges", 7, "Oranges.png",1);
		AddItemToShop("Pasta", 15, "Pasta.png",2);
		AddItemToShop("Rice", 10, "Rice.png",2);
		AddItemToShop("Tomatoes", 12, "Tomatoes.png",1);
		AddItemToShop("Cucumber", 8, "Cucumber.png",1);
		AddItemToShop("Chicken", 20, "Chicken.png",2);
		AddItemToShop("Beef", 30, "Beef.png", 2);
		AddItemToShop("Cheese", 20, "Cheese2.png", 3);
		
		_pageSwitcher.GetMaxPage(_items.Count);
		
		foreach (ShopItemData item in _items )
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
	
	private void AddItemToShop(
		string productName,
		int productPrice,
		string productImagePath,
		int categoryId)
	{
		List<Category> categories = new()
		{
			new(1, "Fruits and Vegetables"),
			new(2, "Bread"),
			new(3, "Milk Products")
		};

		Category category = categories.Find(c => c.CategoryId == categoryId);

		_items.Add(new ShopItemData(
			productName,
			productPrice,
			GD.Load<Texture2D>(ImagePath + productImagePath),
			category
		));
	}

	public void DisplayShopItems()
	{
		
		foreach (Node child in _gridContainer.GetChildren())
		{
			child.QueueFree();
		}

		int startIndex = (_pageSwitcher.CurrentPage - 1) * 8;

		foreach (ShopItemData item in _items
					 .Skip(startIndex)
					 .Take(8))
		{
			ShopItem shopItem = GD.Load<PackedScene>(
				"res://scenes/shop/components/ShopItem.tscn"
			).Instantiate<ShopItem>();

			_gridContainer.AddChild(shopItem);
			shopItem.SetItem(item);
		}
	}
}

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
	
		AddItemToShop("Milk", 10, "Milk.png");
		AddItemToShop("Cheese", 20, "Cheese.png");
		AddItemToShop("Eggs", 8, "Eggs.png");
		AddItemToShop("Butter", 15, "Butter.png");
		AddItemToShop("Bread", 12, "Bread.png");
		AddItemToShop("Ham", 25, "Ham.png");
		AddItemToShop("Bananas", 10, "Bananas.png");
		AddItemToShop("Apples", 5, "Apples.png");
		AddItemToShop("Oranges", 7, "Oranges.png");

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
	
	private void AddItemToShop(string productName, int productPrice, string productImagePath)
	{
		_items.Add(new ShopItemData(productName, productPrice, GD.Load<Texture2D>(ImagePath + productImagePath)));
	}

	public void DisplayShopItems()
	{
		var itemCount =  _gridContainer.GetChildCount();
		_pageSwitcher.GetMaxPage(itemCount);
		
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

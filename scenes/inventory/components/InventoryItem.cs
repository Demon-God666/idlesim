using System;
using System.Collections.Generic;
using Godot;

namespace IdleSim.scenes.inventory.components;

public partial class InventoryItem : Control
{
	private readonly List<int> _multipliers = [1, 5, 10];
	private bool _add = true;
	private Button _addAmount;
	private Inventory _inventory;
	private TextureRect _inventoryImage;
	private Label _itemAvailable;
	private int _itemAvailableValue;
	private Label _itemSellAmount;
	private int _itemSellAmountValue;
	private Label _itemSellPrice;
	private float _itemSellPriceValue;
	private float _itemSingleSellPrice;
	private Button _multiplierButtonTemplate;

	private HBoxContainer _multiplierContainer;

	private Label _productName;
	private bool _remove;
	private Button _removeAmount;
	private Button _sellButton;

	private HBoxContainer _sellContainer;

	public override void _Ready()
	{
		_inventory = GetTree().GetRoot().GetNode<Inventory>("Main/TabContainer/Inventory");

		_productName = GetNode<Label>("ColorRect/VBoxContainer/ProductNameLabel");
		_inventoryImage = GetNode<TextureRect>("ColorRect/VBoxContainer/AspectRatioContainer/InventoryImage");
		_removeAmount = GetNode<Button>("ColorRect/VBoxContainer/HBoxContainerAddRemove/RemoveAmountButton");
		_addAmount = GetNode<Button>("ColorRect/VBoxContainer/HBoxContainerAddRemove/AddAmountButton");

		_multiplierContainer = GetNode<HBoxContainer>("ColorRect/VBoxContainer/HBoxContainerButtons");
		_multiplierButtonTemplate = GetNode<Button>("Templates/MultiplierButton");

		_sellContainer = GetNode<HBoxContainer>("ColorRect/VBoxContainer/HBoxContainer");
		_itemAvailable = GetNode<Label>("ColorRect/VBoxContainer/HBoxContainer/ItemAvailableLabel");
		_itemSellAmount = GetNode<Label>("ColorRect/VBoxContainer/HBoxContainer/ItemAmountLabel");
		_itemSellPrice = GetNode<Label>("ColorRect/VBoxContainer/HBoxContainer/ItemPriceLabel");
		_sellButton = GetNode<Button>("ColorRect/VBoxContainer/HBoxContainer/SellButton");

		LoadMultipliers();

		foreach (var child in _multiplierContainer.GetChildren())
			if (child is Button button)
				button.Pressed += () => CalculateProductAmount(button);

		var buttonGroup = new ButtonGroup();

		_addAmount.ToggleMode = true;
		_removeAmount.ToggleMode = true;

		_addAmount.ButtonGroup = buttonGroup;
		_removeAmount.ButtonGroup = buttonGroup;

		_addAmount.ButtonPressed = true;
		SetActiveButton(_addAmount);

		_addAmount.Pressed += () => SetActiveButton(_addAmount);
		_removeAmount.Pressed += () => SetActiveButton(_removeAmount);

		_sellButton.Pressed += SellItem;
	}

	private void SetActiveButton(Button button)
	{
		_add = button == _addAmount;
		_remove = button == _removeAmount;
	}

	public void SetInventoryItem(BoughtItem item)
	{
		_productName.Text = item.Item.ProductName;
		_inventoryImage.Texture = GD.Load<Texture2D>(
			"res://assets/images/" + item.Item.ProductImage
		);

		_itemAvailableValue = item.Amount;
		_itemAvailable.Text = $"Own: {_itemAvailableValue}x";

		_itemSellAmountValue = 1;
		_itemSellAmount.Text = $"{_itemSellAmountValue}x";

		_itemSingleSellPrice = SetSellPrice(item.Item.ProductPrice);
		_itemSellPriceValue = _itemSingleSellPrice;
		_itemSellPrice.Text = $"{_itemSellPriceValue}$";
	}

	private void CalculateProductAmount(Button button)
	{
		var add = button.Text.EndsWith("x");

		var buttonValue = button.Text.Trim('x', '%').ToInt();


		var amount = add
			? buttonValue
			: (int)Math.Round(_itemAvailableValue * (buttonValue / 100.0));


		if (amount < 1) amount = 1;
		
		_itemSellAmountValue += _add ? amount : -amount;

		if (CheckItemAvailable())
		{
			_itemSellAmount.Text = $"{_itemSellAmountValue}x";
			_itemSellPriceValue = _itemSellAmountValue * _itemSingleSellPrice;
			_itemSellPrice.Text = $"{_itemSellPriceValue}$";
		}
		else
		{
			GD.Print("Something went wrong");
		}
	}

	private bool CheckItemAvailable()
	{
		if (_itemSellAmountValue < 1)
		{
			_itemSellAmountValue = 1;
			GD.Print("Min Reached");
			return true;
		}

		if (_itemSellAmountValue <= _itemAvailableValue) return _itemSellAmountValue <= _itemAvailableValue;

		_itemSellAmountValue = _itemAvailableValue;
		GD.Print("Max Reached");
		return true;
	}

	private void LoadMultipliers()
	{
		foreach (var buttons in _multiplierContainer.GetChildren()) buttons.QueueFree();

		for (var i = 0; i < _multipliers.Count * 2; i++)
		{
			var multiplierIndex = i % _multipliers.Count;
			var multiplierSuffix = i < _multipliers.Count ? "x" : "0%";
			var newMultiplier = (Button)_multiplierButtonTemplate.Duplicate();
			newMultiplier.Text = $"{_multipliers[multiplierIndex]}{multiplierSuffix}";
			_multiplierContainer.AddChild(newMultiplier);
		}
	}

	private static float SetSellPrice(int buyPrice)
	{
		var sellPrice = (float)Math.Round(buyPrice * 0.7 - 1, 2);
		return sellPrice <= 0.01 ? 0.01f : sellPrice;
	}

	private void SellItem()
	{
	}
}

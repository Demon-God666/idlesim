using Godot;
using System;
using System.Collections.Generic;
using IdleSim.scenes.inventory;
using IdleSim.scenes.inventory.components;

public partial class InventoryItem : Control
{
	private Inventory _inventory;
	private BoughtItem _inventoryItems;
	
	private Label _productName;
	private TextureRect _inventoryImage;
	private Button _removeAmount;
	private Button _addAmount;
	
	private HBoxContainer _multiplierContainer;
	private Button _multiplierButtonTemplate;
	private List<string> _multipliers = ["1", "5", "10"];
	
	private HBoxContainer _sellContainer;
	private Label _itemAvailable;
	private Label _itemSellAmount;
	private Label _itemSellPrice;
	private Button _sellButton;
	
	public override void _Ready()
	{
		_inventory = GetTree().GetRoot().GetNode<Inventory>("Main/Inventory");
		
		_productName = GetNode<Label>("VBoxContainer/ProductNameLabel");
		_inventoryImage = GetNode<TextureRect>("VBoxContainer/AspectRatioContainer/InventoryImage");
		_removeAmount = GetNode<Button>("VBoxContainer/HBoxContainerAddRemove/RemoveAmountButton");
		_addAmount = GetNode<Button>("VBoxContainer/HBoxContainerAddRemove/AddAmountButton");
		
		_multiplierContainer = GetNode<HBoxContainer>("VBoxContainer/HBoxContainerButtons");
		_multiplierButtonTemplate = GetNode<Button>("Templates/MultiplierButton");
		
		_sellContainer = GetNode<HBoxContainer>("VBoxContainer/HBoxContainer");
		_itemAvailable = GetNode<Label>("VBoxContainer/HBoxContainer/ItemAvailableLabel");
		_itemSellAmount = GetNode<Label>("VBoxContainer/HBoxContainer/ItemAmountLabel");
		_itemSellPrice = GetNode<Label>("VBoxContainer/HBoxContainer/ItemPriceLabel");
		_sellButton = GetNode<Button>("VBoxContainer/HBoxContainer/SellButton");
		
	}

	public void SetInventoryItem(BoughtItem item)
	{
		_productName.Text = item.Item.ProductName;
	}

	
	
}

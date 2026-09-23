using Godot;
using System;

public partial class AutoCookItem : Control
{
	private bool _isCooking;
	private Label _ingredientList;
	private Label _produceItem;
	private Label _producedProductValue;
	private Label _produceTime;
	
	
	public override void _Ready()
	{
		_ingredientList = GetNode<Label>("VBoxContainer/IngredientListLabel");
		_produceItem = GetNode<Label>("VBoxContainer/ProduceItemLabel");
		_producedProductValue = GetNode<Label>("VBoxContainer/ProducedProductValueLabel");
		_produceTime = GetNode<Label>("VBoxContainer/ProduceTimeLabel");
	}
	
	
}

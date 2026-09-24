using Godot;
using System;
using System.Collections.Generic;
using IdleSim.scenes.auto_cooks.components;

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
	
	public void SetAutoCookItem(Dishes dish) 
	{
			_ingredientList.Text = dish.IngredientList[0].ItemName
			                       + " x" + dish.IngredientList[0].ItemAmount;

			_produceItem.Text = dish.ProduceItem;
			_producedProductValue.Text = dish.ProducedProductValue.ToString();
			_produceTime.Text = dish.ProduceTime.ToString();
	}
}

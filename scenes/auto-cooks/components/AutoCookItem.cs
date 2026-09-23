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

	private List<Dishes> Dishes;
	
	
	public override void _Ready()
	{
		_ingredientList = GetNode<Label>("VBoxContainer/IngredientListLabel");
		_produceItem = GetNode<Label>("VBoxContainer/ProduceItemLabel");
		_producedProductValue = GetNode<Label>("VBoxContainer/ProducedProductValueLabel");
		_produceTime = GetNode<Label>("VBoxContainer/ProduceTimeLabel");
		Dishes = GetNode<List<Dishes>>("Dishes");
		 
		_ingredientList.Text = "";
		_produceItem.Text = "";
		_producedProductValue.Text = "";
		_produceTime.Text = "";
		
		CreateAutoCookItem();
	}

	public void CreateAutoCookItem()
	{
		
		foreach (var item in Dishes)
		{
			
		}
		
	}
	
}

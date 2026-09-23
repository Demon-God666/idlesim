using Godot;
using System;
using System.Collections.Generic;
using IdleSim.scenes.auto_cooks.components;

public partial class AutoCooks : Control
{
	private List<Dishes> _autoCookDishes = new();
	
	private GridContainer _autoCookItemContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_autoCookItemContainer = GetNode<GridContainer>("ColorRect/AutoCookItemContainer");
		
		AddAutoCookItem("Milk", 1, "Milk Rice", 20, 10);
		AddAutoCookItem("Water", 1, "Water Rice", 20, 10);
		AddAutoCookItem("Sugar", 1, "Sugar Rice", 20, 10);
		AddAutoCookItem("Salt", 1, "Salt Rice", 20, 10);
		AddAutoCookItem("Flour", 1, "Flour Rice", 20, 10);

		LoadAutoCooks();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void AddAutoCookItem(string ingredientName, int ingredientAmount, string produceItem, int producedProductValue, int produceTime)
	{
		_autoCookDishes.Add(new Dishes((ingredientName, ingredientAmount), produceItem, producedProductValue, produceTime ));
	}
	
	private void LoadAutoCooks()
	{
		_autoCookItemContainer.QueueFree();
		
		
	}
}

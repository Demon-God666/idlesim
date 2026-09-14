using Godot;
using System;

public partial class CurrencySystem : Control
{
	
	private int _value = 0;
	private Label _valueLabel;
	private Label _label;
	private Button _minusButton;
	private Button _plusButton;
	private Button _buyButton;
	
	public override void _Ready()
	{
		_valueLabel = GetNode<Label>("Value");
		_label = GetNode<Label>("Label");
		_minusButton = GetNode<Button>("MinusButton");
		_plusButton = GetNode<Button>("PlusButton");
		_buyButton = GetNode<Button>("BuyButton");

		_value= _valueLabel.Text.ToInt();
		_label.Text = "Money";
		_plusButton.Pressed += () => Add(1);
		_minusButton.Pressed += () => Remove(1);
		_buyButton.Pressed += () => Remove(10);
	}

	private void Add(int amount)
	{
		_value += amount;
		UpdateValue();
	}
	private void Remove(int amount)
	{
		_value -= amount;
		UpdateValue();
	}
	private void UpdateValue()
	{
		_valueLabel.Text = _value.ToString();
	}
}

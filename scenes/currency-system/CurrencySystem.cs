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
	
	[Signal]
	public delegate void MoneyUpdatedEventHandler(int value);
	
	public override void _Ready()
	{
		_valueLabel = GetNode<Label>("Container/Value");
		_label = GetNode<Label>("Container/Label");
		_minusButton = GetNode<Button>("Container/MinusButton");
		_plusButton = GetNode<Button>("Container/PlusButton");

		_value= _valueLabel.Text.ToInt();
		_label.Text = "Money:";
		
		_plusButton.Pressed += () => Add(100);
		_minusButton.Pressed += () => Remove(1);
		
		EmitSignal(SignalName.MoneyUpdated, _value);
	}

	public int GetMoney()
	{
		return _value;
	}

	public void Add(int amount)
	{
		_value += amount;
		UpdateValue();
	}

	public bool Remove(int amount)
	{
		if (_value < amount)
		{
			return false;
		}

		_value -= amount;
		UpdateValue();

		return true;
	}
	
	private void UpdateValue()
	{
		EmitSignal(SignalName.MoneyUpdated, _value);
		_valueLabel.Text = _value.ToString();
	}
	

	
	
}

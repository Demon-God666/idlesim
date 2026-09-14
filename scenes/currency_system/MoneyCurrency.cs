using Godot;
using System;

public partial class MoneyCurrency : Control
{
	
	private int _value = 0;
	private Label _valueLabel;
	private Label _label;
	private Button _minusButton;
	private Button _plusButton;
	
	public override void _Ready()
	{
		_valueLabel = GetNode<Label>("Value");
		_label = GetNode<Label>("Label");
		_minusButton = GetNode<Button>("MinusButton");
		_plusButton = GetNode<Button>("PlusButton");

		_value= _valueLabel.Text.ToInt();
		_label.Text = "Money";
		_plusButton.Pressed += Add;
		_minusButton.Pressed += Remove;
	}

	private void Add()
	{
		_value++;
		UpdateValue();
	}
	private void Remove()
	{
		_value--;
		UpdateValue();
	}
	private void UpdateValue()
	{
		_valueLabel.Text = _value.ToString();
	}
}

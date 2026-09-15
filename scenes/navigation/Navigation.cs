using Godot;
using System;

public partial class Navigation : Control
{
	private Main _main;
	private Button _currencySystemButton;
	private Button _skillTreeButton;
	private Button _shopButton;
	private Label _moneyLabel;
	
	public override void _Ready()
	{
		_main = GetParent<Main>();
		
		_currencySystemButton = GetNode<Button>("CurrencySystemButton");
		_skillTreeButton = GetNode<Button>("SkillTreeButton");
		_shopButton = GetNode<Button>("ShopButton");
		_moneyLabel = GetNode<Label>("MoneyLabel");
		
		
		_currencySystemButton.Pressed += _main.ShowCurrencySystem;
		_skillTreeButton.Pressed += _main.ShowSkillTree;
		_shopButton.Pressed += _main.ShowShop;
		
		_main.CurrencySystem.MoneyUpdated += UpdateMoneyText;
	}

	private void UpdateMoneyText(int value)
	{
		_moneyLabel.Text = value.ToString();
		GD.Print($"Money updated: {value}");
	}
	
}

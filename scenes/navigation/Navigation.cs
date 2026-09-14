using Godot;
using System;

public partial class Navigation : Control
{
	private Main _main;
	private Button _currencySystemButton;
	private Button _skillTreeButton;
	private Button _shopButton;
	
	public override void _Ready()
	{
		_main = GetParent<Main>();
		
		_currencySystemButton = GetNode<Button>("CurrencySystemButton");
		_skillTreeButton = GetNode<Button>("SkillTreeButton");
		_shopButton = GetNode<Button>("ShopButton");
		
		
		_currencySystemButton.Pressed += _main.ShowCurrencySystem;
		_skillTreeButton.Pressed += _main.ShowSkillTree;
		_shopButton.Pressed += _main.ShowShop;
	}
	
}

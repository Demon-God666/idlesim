using Godot;
using System;

public partial class Main : Control
{	
	private Control _currencySystem;
	private Control _skillTree;
	private Control _shop;
	public override void _Ready()
	{
		_currencySystem = GetNode<Control>("CurrencySystem");
		_skillTree = GetNode<Control>("SkillTree"); 
		_shop = GetNode<Control>("Shop");

		ShowCurrencySystem();
	}

	public void ShowCurrencySystem()
	{
		HideAll();	
		_currencySystem.Show();
		
	}
	
	public void ShowSkillTree()
	{
		HideAll();	
		_skillTree.Show();
	}

	public void ShowShop()
	{
		HideAll();	
		_shop.Show();
	}
	
	private void HideAll()
	{
		_currencySystem.Hide();
		_skillTree.Hide();
		_shop.Hide();
	}
}

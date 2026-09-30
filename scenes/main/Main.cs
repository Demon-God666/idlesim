using Godot;
using IdleSim.scenes.auto_cooks;
using IdleSim.scenes.currency_system;
using IdleSim.scenes.inventory;

namespace IdleSim.scenes.main;

public partial class Main : Control
{
	private Control _skillTree;
	private Control _shop;

	private Inventory Inventory { get; set; }
	private CurrencySystem CurrencySystem { get; set; }

	private AutoCooks AutoCooks { get; set; }

	public override void _Ready()
	{
		CurrencySystem = GetNode<CurrencySystem>("CurrencySystem");
		_skillTree = GetNode<Control>("SkillTree");
		_shop = GetNode<Control>("Shop");
		Inventory = GetNode<Inventory>("Inventory");
		AutoCooks = GetNode<AutoCooks>("AutoCooks");

		ShowCurrencySystem();
	}

	public void ShowCurrencySystem()
	{
		HideAll();
		CurrencySystem.Show();
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

	public void ShowInventory()
	{
		HideAll();
		Inventory.Show();
	}

	public void ShowAutoCooks()
	{
		HideAll();
		AutoCooks.Show();
	}

	private void HideAll()
	{
		CurrencySystem.Hide();
		_skillTree.Hide();
		_shop.Hide();
		Inventory.Hide();
		AutoCooks.Hide();
	}
}

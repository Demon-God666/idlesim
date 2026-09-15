using Godot;

public partial class Main : Control
{	

	private Control _skillTree;
	private Control _shop;

	public CurrencySystem CurrencySystem { get; private set; }

	public override void _Ready()
	{
		CurrencySystem = GetNode<CurrencySystem>("CurrencySystem");

	
		_skillTree = GetNode<Control>("SkillTree"); 
		_shop = GetNode<Control>("Shop");

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
	
	private void HideAll()
	{
		CurrencySystem.Hide();
		_skillTree.Hide();
		_shop.Hide();
	}
}

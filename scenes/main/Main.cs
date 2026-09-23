using Godot;

public partial class Main : Control
{
	private Control _shop;
	private CurrencySystem _currencySystem;

	public override void _Ready()
	{
		_currencySystem = GetNode<CurrencySystem>("CurrencySystem");
		_shop = GetNode<Control>("Shop");

		ShowCurrencySystem();
	}

	public void ShowCurrencySystem()
	{
		HideAll();
		_currencySystem.Show();
	}

	public void ShowShop()
	{
		HideAll();
		_shop.Show();
	}

	private void HideAll()
	{
		_currencySystem.Hide();
		_shop.Hide();
	}
}

using Godot;

public partial class Shop : Control
{
	private CurrencySystem _currencySystem;
	private int _money;
	public override void _Ready()
	{
		_currencySystem = GetNode<CurrencySystem>("../CurrencySystem");
		_currencySystem.MoneyUpdated += UpdateMoney;
		
		_money = _currencySystem.GetMoney();
	
	}
	
	private void UpdateMoney(int value)
	{
		_money = value;
	}

	public int GetMoney()
	{
		return _money;
	}
}

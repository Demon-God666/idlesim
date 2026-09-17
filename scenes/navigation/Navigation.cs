using Godot;

public partial class Navigation : Control
{
	private Main _main;
	private CurrencySystem _currencySystem;
	
	private Button _currencySystemButton;
	private Button _skillTreeButton;
	private Button _shopButton;
	private Label _moneyLabel;
	
	public override void _Ready()
	{
		_main = GetParent<Main>();
		_currencySystem = GetNode<CurrencySystem>("../CurrencySystem");
		
		_currencySystemButton = GetNode<Button>("CurrencySystemButton");
		_skillTreeButton = GetNode<Button>("SkillTreeButton");
		_shopButton = GetNode<Button>("ShopButton");
		_moneyLabel = GetNode<Label>("MoneyLabel");
		
		_moneyLabel.Text = $"Money: {_currencySystem.GetMoney()}$";
		
		_currencySystemButton.Pressed += _main.ShowCurrencySystem;
		_skillTreeButton.Pressed += _main.ShowSkillTree;
		_shopButton.Pressed += _main.ShowShop;
		
		_currencySystem.MoneyUpdated += UpdateMoneyText;
	}

	private void UpdateMoneyText(int value)
	{
		_moneyLabel.Text = $"Money: {value}$";
		GD.Print($"Money updated: {value}$");
	}

}

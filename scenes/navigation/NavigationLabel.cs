using Godot;
using IdleSim.scenes.currency_system;

namespace IdleSim.scenes.navigation;

public partial class NavigationLabel : Control
{
	private Button _autoCookButton;
	private CurrencySystem _currencySystem;

	private Button _currencySystemButton;
	private Button _inventoryButton;
	private Label _moneyLabel;
	private Button _shopButton;
	private Button _skillTreeButton;
	private Button _settingsButton;

	public override void _Ready()
	{
		_currencySystem = GetTree().Root.GetNode<CurrencySystem>("Main/TabContainer/CurrencySystem");

		_moneyLabel = GetNode<Label>("HBoxContainer/MoneyLabel");

		_moneyLabel.Text = $"Money: {_currencySystem.GetMoney()}$";


		_currencySystem.MoneyUpdated += UpdateMoneyText;
	}

	private void UpdateMoneyText(int value)
	{
		_moneyLabel.Text = $"Money: {value}$";
		GD.Print($"Money updated: {value}$");
	}
}

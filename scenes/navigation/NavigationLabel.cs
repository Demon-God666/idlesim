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
	private Label _skillPointsLabel;
	
	public override void _Ready()
	{
		_currencySystem = GetTree().Root.GetNode<CurrencySystem>("Main/TabContainer/CurrencySystem");

		_moneyLabel = GetNode<Label>("HBoxContainer/MoneyLabel");
		_skillPointsLabel = GetNode<Label>("HBoxContainer/SkillPointsLabel");

		_moneyLabel.Text = $"Money: {_currencySystem.Value}$";
		_skillPointsLabel.Text = $"SP: {_currencySystem.SkillPoints}$";
		
		_currencySystem.MoneyUpdated += UpdateMoneyText;

		_currencySystem.SkillPointsUpdated += UpdateSkillPoints;
	}

	private void UpdateMoneyText(int value)
	{
		_moneyLabel.Text = $"Money: {value}$";
		GD.Print($"Money updated: {value}$");
	}
	
	private void UpdateSkillPoints(int skillPoints)
	{
		_skillPointsLabel.Text = $"SP: {skillPoints}";
		GD.Print($"Skill points updated: {skillPoints}");
	}
}

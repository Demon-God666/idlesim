using Godot;
using IdleSim.scenes.currency_system;
using IdleSim.scenes.main;

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

	private Main _main;
	
	public override void _Ready()
	{
		_main = GetTree().Root.GetNode<Main>("Main");
		_moneyLabel = GetNode<Label>("HBoxContainer/MoneyLabel");
		_skillPointsLabel = GetNode<Label>("HBoxContainer/SkillPointsLabel");

		_moneyLabel.Text = $"Money: {_main.Money}$";
		_skillPointsLabel.Text = $"SP: {_main.SkillPoints}$";
		
		_main.MoneyUpdated += UpdateMoneyText;

		_main.SkillPointsUpdated += UpdateSkillPoints;
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

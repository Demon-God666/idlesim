using Godot;
using IdleSim.scenes.currency_system;

namespace IdleSim.scenes.skill_tree.components;

public partial class SkillButtonItem : VBoxContainer
{
	[Export] public string SkillName { get; set; }
	[Export] public int Cost { get; set; } = 1;
	private bool Unlocked { get; set; }

	private Button _skillButton;
	private Label _skillButtonCostLabel;

	private SkillBuyMenu _skillBuyMenu;

	private SkillData _skillData;

	private CurrencySystem _currencySystem;

	public override void _Ready()
	{
		_currencySystem = GetTree().Root.GetNode<CurrencySystem>("Main/TabContainer/CurrencySystem");

		_skillButton = GetNode<Button>("SkillButton");
		_skillButtonCostLabel = GetNode<Label>("SkillPointsLabel");
		_skillBuyMenu = GetParent().GetParent().GetNode<SkillBuyMenu>("SkillBuyMenu");
	}
	
	private void OnSkillMouseEntered(Button skillButton, SkillData skill)
	{
		skillButton.Modulate = new Color(0, 1, 0, 0.5f);
		skillButton.Text = "Buy?";
		

		_skillBuyMenu.ShowSkillBuyMenu(skill);
	}

	private void OnSkillMouseExited(Button skillButton)
	{
		skillButton.Modulate = Colors.White;
		skillButton.Text = "";

		_skillBuyMenu.Hide();
	}
	
	
	private void BuySkill(SkillData skill)
	{
		_currencySystem.RemoveSkillPoints(1);

		/*
		if (Unlocked) return;
		if (GetTree().CurrentScene is not SkillTreeScroll skillTree) return;
		if (skillTree.SkillPoints < Cost) return;

		skillTree.SpendSkillPoints(Cost);

		Unlocked = true;*/
	}

	public SkillButtonItem CreateSkill(SkillData skill)
	{
		var skillButtonItem = (SkillButtonItem)Duplicate();

		var skillButton = skillButtonItem.GetNode<Button>("SkillButton");
		var skillCostLabel = skillButtonItem.GetNode<Label>("SkillPointsLabel");

		var skillIcon = GD.Load<Texture2D>($"res://assets/images/{skill.Image}");
		;
	
		skillButton.Icon = skillIcon;
		skillCostLabel.Text = "Skillpoints: " + skill.Cost;
		skillButton.Pressed += () => BuySkill(skill);
		skillButton.MouseEntered += () => OnSkillMouseEntered(skillButton, skill);
		skillButton.MouseExited += () => OnSkillMouseExited(skillButton);

		/* TODO Maybe extract the positioning func*/
		var skillButtonItemWidth = skillButtonItem.GetSize().X / 2;
		var skillButtonItemHeight = skillButtonItem.GetSize().Y / 2;
		var zeroPoint = new Vector2(
			2000 - skillButtonItemWidth,
			2000 - skillButtonItemHeight
		);

		skillButtonItem.Position = zeroPoint;
		/**/

		return skillButtonItem;
	}
}

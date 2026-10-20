using Godot;

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

	public override void _Ready()
	{
		_skillButton = GetNode<Button>("SkillButton");
		_skillButtonCostLabel = GetNode<Label>("SkillPointsLabel");
		_skillBuyMenu = GetParent().GetParent().GetNode<SkillBuyMenu>("SkillBuyMenu");
		
		_skillButton.Pressed += BuySkill;
	}

	private void BuySkill()
	{/*
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
		
		skillButton.Icon = GD.Load<Texture2D>($"res://assets/images/{skill.Image}");
		skillCostLabel.Text = "Skillpoints: " + skill.Cost;
		_skillButton.MouseEntered += () =>
			_skillBuyMenu.ShowSkillBuyMenu(skill);

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

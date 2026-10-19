using Godot;

namespace IdleSim.scenes.skill_tree.components;

public partial class SkillButtonItem : VBoxContainer
{
	[Export] public string SkillName { get; set; } 
	[Export] public int Cost { get; set; } = 1;

	private bool Unlocked { get; set; }

	private Button _skillButton;
	private Label _skillCost;

	public override void _Ready()
	{
		_skillButton = GetNode<Button>("SkillButton");
		_skillCost = GetNode<Label>("SkillPointsLabel");
		
		_skillButton.Pressed += OnPressed;
	}

	private void OnPressed()
	{
		if (Unlocked) return;
		if (GetTree().CurrentScene is not SkillTreeScroll skillTree) return;
		if (skillTree.SkillPoints < Cost) return;

		skillTree.SpendSkillPoints(Cost);

		Unlocked = true;

	}
}

using Godot;

public partial class Skill : Button
{
	[Export]
	public string SkillName { get; set; } = "Skill";

	[Export]
	public int Cost { get; set; } = 1;

	public bool Unlocked { get; private set; }

	public override void _Ready()
	{
		Text = SkillName;

		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		if (Unlocked)
			return;

		SkillTree skillTree = GetTree()
			.CurrentScene as SkillTree;

		if (skillTree == null)
			return;

		if (skillTree.SkillPoints < Cost)
			return;

		skillTree.SpendSkillPoints(Cost);

		Unlocked = true;

		Text = $"✓ {SkillName}";
	}
}

using Godot;

public partial class SkillTree : Control
{
	public int SkillPoints { get; private set; } = 5;

	private Label _skillPointsLabel;

	public override void _Ready()
	{
		_skillPointsLabel = GetNode<Label>(
            "SkillPointsLabel"
		);

		UpdateSkillPoints();
	}

	public void SpendSkillPoints(int amount)
	{
		if (SkillPoints < amount)
			return;

		SkillPoints -= amount;

		UpdateSkillPoints();
	}

	public void UpdateSkillPoints()
	{
		_skillPointsLabel.Text =
			$"Skillpunkte: {SkillPoints}";
	}
}

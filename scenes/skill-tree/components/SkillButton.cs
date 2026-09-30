using Godot;

namespace IdleSim.scenes.skill_tree.components;

public partial class SkillButton : Button
{
    [Export] public string SkillName { get; set; } = "Skill";

    [Export] public int Cost { get; set; } = 1;

    private bool Unlocked { get; set; }

    public override void _Ready()
    {
        Text = SkillName;
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        if (Unlocked)
            return;

        var skillTree = GetTree().CurrentScene as SkillTree;

        if (skillTree == null)
            return;

        if (skillTree.SkillPoints < Cost)
            return;

        skillTree.SpendSkillPoints(Cost);

        Unlocked = true;
        Text = $"✓ {SkillName}";
    }
}
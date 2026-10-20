using Godot;

namespace IdleSim.scenes.skill_tree.components;

public partial class SkillBuyMenu : Control
{
	private TextureRect _skillImage;
	private Label _descriptionTitleLabel;
	private Label _descriptionLabel;
	private Label _priceLabel;
	private Label _levelLabel;
	
	public override void _Ready()
	{
		_skillImage = GetNode<TextureRect>("HBoxContainer/SkillImage");
		_descriptionTitleLabel = GetNode<Label>("HBoxContainer/VBoxContainer/TitleLabel");
		_descriptionLabel = GetNode<Label>("HBoxContainer/VBoxContainer/DescriptionLabel");
		_priceLabel = GetNode<Label>("HBoxContainer/PriceContainer/PriceLabel");
		_levelLabel = GetNode<Label>("HBoxContainer/PriceContainer/LevelLabel");
	}

	public void ShowSkillBuyMenu(SkillData skill)
	{
		Show();
		
		_skillImage.Texture = GD.Load<Texture2D>($"res://assets/images/{skill.Image}");
		_descriptionTitleLabel.Text = skill.SkillName;
		_descriptionLabel.Text = skill.SkillDescription;
		_priceLabel.Text = skill.Cost.ToString();
		_levelLabel.Text = skill.Levels.ToString();
		
	}
}

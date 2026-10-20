using System;
using System.Collections.Generic;
using Godot;
using IdleSim.scenes.skill_tree.components;

namespace IdleSim.scenes.skill_tree;

public partial class SkillTree : Control
{
	private Control _skillTreeContainer;
	private VBoxContainer _skillButton;
	private Label _skillButtonLabel;
	
	private List<SkillData> _testButton =
	[
		new()
		{
			Image = "Skill_Money.svg",
			SkillName = "CoolSkilllll",
			SkillDescription = "Miauwww",
			Cost = 2,
			Levels = 3,
			Value = 10
		}
	];
	public override void _Ready()
	{
		_skillTreeContainer = GetNode<Control>("SkillTreeScroll/SkillTreeContainer");
		_skillButton = GetNode<VBoxContainer>("SkillTreeScroll/Templates/SkillButtonItemTemplate");
		
		foreach (var button in _skillTreeContainer.GetChildren())
		{
			button.QueueFree();
		}
		
		LoadSkills();
	}

	private void LoadSkills()
	{
		var skillButtonTest = _testButton[0];

		var skillButtonItem = (VBoxContainer)_skillButton.Duplicate();
		var skillButton = skillButtonItem.GetNode<Button>("SkillButton");
		var skillCost = skillButtonItem.GetNode<Label>("SkillPointsLabel"); 

		skillButton.Icon = GD.Load<Texture2D>($"res://assets/images/{skillButtonTest.Image}");
		skillCost.Text = "Skillpoints: " + skillButtonTest.Cost;

		var skillButtonItemWidth = skillButtonItem.GetSize().X / 2;
		var skillButtonItemHeight = skillButtonItem.GetSize().Y / 2;
		var zeroPoint = new Vector2(
			2000 - skillButtonItemWidth,
			2000 - skillButtonItemHeight
		);

		skillButtonItem.Position = zeroPoint;
		
		_skillTreeContainer.AddChild(skillButtonItem);
	}
}

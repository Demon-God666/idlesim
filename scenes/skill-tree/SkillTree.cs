using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using IdleSim.scenes.skill_tree.components;

namespace IdleSim.scenes.skill_tree;

public partial class SkillTree : Control
{
	private Control _skillTreeContainer;
	private SkillButtonItem _skillButtonItem;
	
	private List<SkillData> _testSkills =
	[
		new()
		{
			Image = "Skill_Money.svg",
			SkillName = "MoneyMaster",
			SkillDescription = "Verdiene mehr Geld pro Verkauf",
			Cost = 3,
			Levels = 5,
			Value = 15
		}/*,
		new()
		{
			Image = "Skill_Money.svg",
			SkillName = "FastService",
			SkillDescription = "Erhöhe die Geschwindigkeit deiner Köche",
			Cost = 2,
			Levels = 3,
			Value = 10
		},
		new()
		{
			Image = "Skill_Money.svg",
			SkillName = "BigTips",
			SkillDescription = "Erhöhe die Trinkgelder deiner Kunden",
			Cost = 4,
			Levels = 4,
			Value = 20
		},
		new()
		{
			Image = "Skill_Money.svg",
			SkillName = "CustomerMagnet",
			SkillDescription = "Erhöhe die Anzahl deiner Kunden",
			Cost = 5,
			Levels = 5,
			Value = 25
		}*/
	];
	public override void _Ready()
	{
		_skillTreeContainer = GetNode<Control>("SkillTreeScroll/SkillTreeContainer");
		_skillButtonItem = GetNode<SkillButtonItem>("SkillTreeScroll/Templates/SkillButtonItemTemplate"); 
		
		foreach (var button in _skillTreeContainer.GetChildren())
		{
			button.QueueFree();
		}
		
		LoadSkills();
	}

	private void LoadSkills()
	{
		foreach (var skillItem in _testSkills.Select(skill => _skillButtonItem.CreateSkill(skill)))
		{
			_skillTreeContainer.AddChild(skillItem);
		}
	}
}

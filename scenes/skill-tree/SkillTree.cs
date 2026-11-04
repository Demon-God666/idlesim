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

	public List<SkillData> _testSkills =
	[
		new()
		{
			Id = 1,
			Child = [2, 3],
			Image = "Skill_BuyConfirm.svg",
			SkillName = "MoneyMaster",
			SkillDescription = "Verdiene mehr Geld pro Verkauf",
			Cost = 3,
			BoughtLevels = 0,
			Levels = 5,
			Value = 15
		},
		new()
		{
			Id = 2,
			Image = "Skill_Money.svg",
			SkillName = "FastService",
			SkillDescription = "Erhöhe die Geschwindigkeit deiner Köche",
			Cost = 2,
			BoughtLevels = 0,
			Levels = 3,
			Value = 10
		},
		new()
		{
			Id = 3,
			Image = "Skill_Money.svg",
			SkillName = "BigTips",
			SkillDescription = "Erhöhe die Trinkgelder deiner Kunden",
			Cost = 4,
			BoughtLevels = 0,
			Levels = 4,
			Value = 20
		},
		new()
		{
			Id = 4,
			Image = "Skill_Money.svg",
			SkillName = "CustomerMagnet",
			SkillDescription = "Erhöhe die Anzahl deiner Kunden",
			Cost = 5,
			BoughtLevels = 0,
			Levels = 5,
			Value = 25
		}
	];

	public override void _Ready()
	{
		_skillTreeContainer = GetNode<Control>("SkillTreeScroll/SkillTreeContainer");
		_skillButtonItem = GetNode<SkillButtonItem>("SkillTreeScroll/Templates/SkillButtonItemTemplate");


		foreach (var button in _skillTreeContainer.GetChildren())
		{
			button.Free();
		}

		LoadSkills();
	}

	private void LoadSkills()
	{
		foreach (var skill in _testSkills)
		{
			var skillItem = _skillButtonItem.CreateSkill(skill);
			SetSkillButtonPosition(skillItem, skill);
			_skillTreeContainer.AddChild(skillItem);
		}
	}

	private void SetSkillButtonPosition(SkillButtonItem skillButton, SkillData skill)
	{
		var skillButtonItemAmount = _skillTreeContainer.GetChildCount();
		var positionX = 2000 + (skillButtonItemAmount * 200);
		var positionY = positionX;
		
		GD.Print("amount", skillButtonItemAmount);

		var skillButtonItemWidth = skillButton.GetSize().X / 2;
		var skillButtonItemHeight = skillButton.GetSize().Y / 2;
  
		// TODO set other position for child

		foreach (var child in _skillTreeContainer.GetChildren())
		{
			
		}
		
		var newPosition = new Vector2(
			positionX - skillButtonItemWidth,
			positionY - skillButtonItemHeight
		);
	  

		skillButton.Position = newPosition;
	}
}

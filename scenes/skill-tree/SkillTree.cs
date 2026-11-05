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
	
	private Dictionary<int, SkillButtonItem> _skillItems = new();

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

			_skillItems[skill.Id] = skillItem;
			_skillTreeContainer.AddChild(skillItem);
		}

		foreach (var skill in _testSkills)
		{
			SetSkillButtonPosition(_skillItems[skill.Id], skill);
		}
	}

	private void SetSkillButtonPosition(SkillButtonItem skillItem, SkillData skill)
	{
		var skillButtonItemAmount = _skillTreeContainer.GetChildCount();
		var positionY = 2000;
		var positionX = 2000 + (skill.Id * 200);
		
		GD.Print("amount", skill.Id);

		var skillButtonItemWidth = skillItem.GetSize().X / 2;
		var skillButtonItemHeight = skillItem.GetSize().Y / 2;
		
		if (skill.Child == null || skill.Child.Count <= 0) return;

		var newPosition = new Vector2(
			positionX - skillButtonItemWidth,
			positionY - skillButtonItemHeight
		);
		
		skillItem.Position = newPosition;
		
		var childCount = 0;

		foreach (var child in skill.Child)
		{
			childCount++;

			var childPosition = new Vector2(
				skillItem.Position.X,
				skillItem.Position.Y + (childCount * 200)
			);

			if (!_skillItems.TryGetValue(child, out var childItem))
				continue;

			childItem.Position = childPosition;
		}
		
	}
}

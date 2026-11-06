using System;
using System.Collections.Generic;
using Godot;
using IdleSim.scenes.currency_system;

namespace IdleSim.scenes.skill_tree.components;

public partial class SkillButtonItem : VBoxContainer
{
	private bool Unlocked { get; set; }

	private Button _skillButton;
	private Label _skillButtonCostLabel;

	private SkillBuyMenu _skillBuyMenu;

	private SkillData _skillData;

	private CurrencySystem _currencySystem;

	private void EnteredHandler()
	{
		OnSkillMouseEntered(_skillButton, _skillData);
	}

	private void ExitedHandler()
	{
		OnSkillMouseExited(_skillButton);
	}

	public override void _Ready()
	{
		_currencySystem = GetTree().Root.GetNode<CurrencySystem>("Main/TabContainer/CurrencySystem");

		_skillButton = GetNode<Button>("SkillButton");
		_skillButtonCostLabel = GetNode<Label>("SkillPointsLabel");
		_skillBuyMenu = GetTree().Root.GetNode<SkillBuyMenu>("Main/TabContainer/SkillTree/SkillTreeScroll/SkillBuyMenu");
	}

	private void OnSkillMouseEntered(Button skillButton, SkillData skill)
	{
		if (skillButton.Disabled)
		{
			UpdateSkillData(skill);
			return;
		}

		skillButton.Modulate = new Color(0, 1, 0, 0.5f);
		skillButton.Text = "Buy?";

		UpdateSkillData(skill);
	}

	private void OnSkillMouseExited(Button skillButton)
	{
		if (skillButton.Disabled)
		{
			_skillBuyMenu.Hide();
			return;
		}

		skillButton.Modulate = Colors.White;
		skillButton.Text = "";
		_skillBuyMenu.Hide();
	}


	private void BuySkill(Button skillButton, SkillData skill)
	{
		if (CheckMaxLevelReached(skill))
		{
			skillButton.Disabled = true;
			GD.Print("Max lvl reached");
			return;
		}

		_currencySystem.RemoveSkillPoints(skill.Cost);

		skill.Cost *= 2;
		skill.BoughtLevels++;
		skill.Value *= 2;

		UpdateSkillData(skill);
		/*
		if (Unlocked) return;
		if (GetTree().CurrentScene is not SkillTreeScroll skillTree) return;
		if (skillTree.SkillPoints < Cost) return;

		skillTree.SpendSkillPoints(Cost);

		Unlocked = true;*/
	}

	private bool CheckMaxLevelReached(SkillData skill)
	{
		return skill.BoughtLevels + 1 > skill.Levels;
	}

	private void UpdateSkillData(SkillData skill)
	{
		_skillBuyMenu.ShowSkillBuyMenu(skill);
	}

	public SkillButtonItem CreateSkill(SkillData skill)
	{
		var skillButtonItem = (SkillButtonItem)Duplicate();

		skillButtonItem._skillData = skill;

		var skillButton = skillButtonItem.GetNode<Button>("SkillButton");
		var skillCostLabel = skillButtonItem.GetNode<Label>("SkillPointsLabel");

		skillButton.Icon = GD.Load<Texture2D>(
			$"res://assets/images/{skill.Image}"
		);

		skillCostLabel.Text = "Skillpoints: " + skill.Cost;

		skillButton.Pressed += () =>
			skillButtonItem.BuySkill(skillButton, skill);

		skillButton.MouseEntered += skillButtonItem.EnteredHandler;
		skillButton.MouseExited += skillButtonItem.ExitedHandler;

		return skillButtonItem;
	}
}

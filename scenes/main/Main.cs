using System.Collections.Generic;
using System.Text.Json;
using Godot;
using IdleSim.scenes.main.components;
using IdleSim.scenes.settings;
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.main;

public partial class Main : Control
{
	[Signal]
	public delegate void MoneyUpdatedEventHandler(int value);
    
	[Signal]
	public delegate void SkillPointsUpdatedEventHandler(int skillPoints);
	
	private Settings Settings { get; set; }
	public int Money { get; set; }
	public int SkillPoints { get; set; }


	private TabContainer _tabContainer;

	private Theme _darkTheme;
	private Theme _lightTheme;

	public override void _Ready()
	{
		Settings = GetNode<Settings>("TabContainer/Settings");

		_tabContainer = GetNode<TabContainer>("TabContainer");
		_tabContainer.CurrentTab = 0;

		_darkTheme = GD.Load<Theme>("res://scenes/main/DarkTheme.tres");
		_lightTheme = GD.Load<Theme>("res://scenes/main/LightTheme.tres");

		Settings.InitializeTheme();

		EmitSignal(SignalName.MoneyUpdated, Money);
		EmitSignal(SignalName.SkillPointsUpdated, SkillPoints);
		LoadPlayerData();
	}

	public void ToggleDarkMode(bool isDarkMode)
	{
		Theme = isDarkMode ? _darkTheme : _lightTheme;
	}
	
	
	private void LoadPlayerData()
	{
		var json = FileAccess.GetFileAsString("res://data/PlayerData.json");
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};
		var items = JsonSerializer.Deserialize<List<PlayerDataJson>>(json, options);

		foreach (var item in items)
		{
			Money = item.Money;
			SkillPoints = item.SkillPoints;
		}
	}
	
	public void UpdateMoneyValue()
	{
		EmitSignal(SignalName.MoneyUpdated, Money);
	}
	
	public void UpdateSkillPoints()
	{
		EmitSignal(SignalName.SkillPointsUpdated, SkillPoints);
	}
}

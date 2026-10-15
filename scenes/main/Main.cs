using Godot;
using IdleSim.scenes.settings;

namespace IdleSim.scenes.main;

public partial class Main : Control
{
	private Settings Settings { get; set; }

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
	}

	public void ToggleDarkMode(bool isDarkMode)
	{
		Theme = isDarkMode ? _darkTheme : _lightTheme;
	}
}

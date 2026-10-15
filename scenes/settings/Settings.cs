using Godot;
using IdleSim.scenes.main;

namespace IdleSim.scenes.settings;

public partial class Settings : Control
{
	private Main _main;
	private CheckButton _toggleDarkMode;

	public override void _Ready()
	{
		_main = GetTree().Root.GetNode<Main>("Main");
		_toggleDarkMode = GetNode<CheckButton>(
			"VBoxContainer/SettingItem/HBoxContainer/DarkModeButton");

		_toggleDarkMode.Toggled += SetThemeColor;

		_toggleDarkMode.ButtonPressed = true;
		SetThemeColor(true);
	}

	private void SetThemeColor(bool isPressed)
	{
		_main.ToggleDarkMode(isPressed);
	}
	
	public void InitializeTheme()
	{
		SetThemeColor(true);
	}
}

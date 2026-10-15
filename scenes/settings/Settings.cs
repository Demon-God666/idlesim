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
		_toggleDarkMode = GetNode<CheckButton>("VBoxContainer/SettingItem/HBoxContainer/DarkModeButton");
		_toggleDarkMode.Pressed += SetThemeColor;
	}

	private void SetThemeColor()
	{
		var backgroundColor = _toggleDarkMode.ButtonPressed
			? new Color(0.267f, 0.267f, 0.267f)
			: Colors.White;
		
		_main.ToggleDarkMode(backgroundColor);
	}
	
	public void InitializeTheme()
	{
		_toggleDarkMode.ButtonPressed = true;
		SetThemeColor();
	}
}

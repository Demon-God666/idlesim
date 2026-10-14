using Godot;
using IdleSim.scenes.main;

namespace IdleSim.scenes.settings;

public partial class Settings : Control
{
	private Main _main;
	private Button _closeButton;

	private CheckButton _toggleDarkMode;
	private ColorRect _settingsBackground;
	public override void _Ready()
	{
		_main = GetParent<Main>();
		_closeButton = GetNode<Button>("CloseButton");
		_toggleDarkMode = GetNode<CheckButton>("VBoxContainer/SettingItem/HBoxContainer/DarkModeButton");
		_settingsBackground = GetNode<ColorRect>("ColorRect");
		
		_closeButton.Pressed += _main.HideSettings;
		_toggleDarkMode.Pressed += SetThemeColor;
	}

	private void SetThemeColor()
	{
		var backgroundColor = _toggleDarkMode.ButtonPressed
			? new Color(0.267f, 0.267f, 0.267f)
			: Colors.White;

		_settingsBackground.Color = backgroundColor; 
		_main.ToggleDarkMode(backgroundColor);
	}
	
}
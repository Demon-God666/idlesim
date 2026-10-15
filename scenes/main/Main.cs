using Godot;
using IdleSim.scenes.auto_cooks;
using IdleSim.scenes.currency_system;
using IdleSim.scenes.inventory;
using IdleSim.scenes.settings;

namespace IdleSim.scenes.main;

public partial class Main : Control
{ 
	private settings.Settings Settings { get; set; }

	private ColorRect _backgroundColorRect;

	private TabContainer _tabContainer;

	public override void _Ready()
	{
		Settings = GetNode<Settings>("TabContainer/Settings");
		_tabContainer = GetNode<TabContainer>("TabContainer");
		_tabContainer.CurrentTab = 0;

		_backgroundColorRect = GetNode<ColorRect>("BackgroundColorRect");
		Settings.InitializeTheme();
	}

	public void ToggleDarkMode(Color backgroundColor)
	{
		_backgroundColorRect.Color = backgroundColor;
	}
}

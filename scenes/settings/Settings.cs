using Godot;
using IdleSim.scenes.main;

public partial class Settings : Control
{
	private Main _main;
	private Button _closeButton;
	public override void _Ready()
	{
		_main = GetParent<Main>();
		_closeButton = GetNode<Button>("CloseButton");

		_closeButton.Pressed += _main.HideSettings;

	}
	
}

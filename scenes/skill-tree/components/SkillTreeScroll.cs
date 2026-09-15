using Godot;

public partial class SkillTreeScroll : ScrollContainer
{
	private bool _dragging;
	private Vector2 _lastMousePosition;

	public override void _GuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton &&
			mouseButton.ButtonIndex == MouseButton.Middle)
		{
			_dragging = mouseButton.Pressed;
			_lastMousePosition = mouseButton.Position;
		}

		if (@event is InputEventMouseMotion motion && _dragging)
		{
			Vector2 movement = motion.Relative;

			ScrollHorizontal -= (int)movement.X;
			ScrollVertical -= (int)movement.Y;
		}
	}
}

using Godot;

public partial class SkillTree : Control
{
	public int SkillPoints { get; private set; } = 5;

	private Label _skillPointsLabel;
	private ScrollContainer _scrollContainer;

	private bool _dragging;
	private Vector2 _lastMousePosition;

	public override void _Ready()
	{
		_skillPointsLabel = GetNode<Label>(
            "SkillPointsLabel"
		);

		_scrollContainer = GetNode<ScrollContainer>(
            "ScrollContainer"
		);

		UpdateSkillPoints();
	}

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
			Vector2 movement =
				motion.Position - _lastMousePosition;

			_scrollContainer.ScrollHorizontal -=
				(int)movement.X;

			_scrollContainer.ScrollVertical -=
				(int)movement.Y;

			_lastMousePosition = motion.Position;
		}
	}

	public void SpendSkillPoints(int amount)
	{
		if (SkillPoints < amount)
			return;

		SkillPoints -= amount;

		UpdateSkillPoints();
	}

	public void UpdateSkillPoints()
	{
		_skillPointsLabel.Text =
			$"Skillpunkte: {SkillPoints}";
	}
}

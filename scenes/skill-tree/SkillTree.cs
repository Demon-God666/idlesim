using Godot;

namespace IdleSim.scenes.skill_tree;

public partial class SkillTree : Control
{
	private bool _dragging;
	private Vector2 _lastMousePosition;
	private Vector2 _panOffset;

	private Control _skillTreeContainer;

	public int SkillPoints { get; private set; } = 5;

	private const float ZoomStep = 1.1f;
	private const float MinZoom = 0.5f;
	private const float MaxZoom = 2.0f;

	private float _zoom = 1.0f;

	public override void _Ready()
	{
		_skillTreeContainer = GetNode<Control>("SkillTreeContainer");

		_panOffset = (Size / 2) - new Vector2(2000, 2000);
		UpdateCanvas();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex is MouseButton.WheelUp or MouseButton.WheelDown)
			{
				if (!GetGlobalRect().HasPoint(GetGlobalMousePosition()))
					return;

				GetViewport().SetInputAsHandled();

				if (mouseButton.Pressed)
				{
					Zoom(mouseButton.ButtonIndex == MouseButton.WheelUp
						? ZoomStep
						: 1f / ZoomStep);
				}

				return;
			}

			if (mouseButton.ButtonIndex is MouseButton.Middle or MouseButton.Right)
			{
				_dragging = mouseButton.Pressed;
				_lastMousePosition = GetGlobalMousePosition();

				GetViewport().SetInputAsHandled();
			}
		}

		if (@event is InputEventMouseMotion && _dragging)
		{
			var mousePosition = GetGlobalMousePosition();
			_panOffset += mousePosition - _lastMousePosition;
			_lastMousePosition = mousePosition;

			UpdateCanvas();
			GetViewport().SetInputAsHandled();
		}
	}

	private void Zoom(float factor)
	{
		var mousePosition = GetGlobalMousePosition();
		var oldZoom = _zoom;

		_zoom = Mathf.Clamp(_zoom * factor, MinZoom, MaxZoom);

		if (Mathf.IsEqualApprox(oldZoom, _zoom))
			return;

		var mouseInCanvas = (mousePosition - _panOffset) / oldZoom;
		_panOffset = mousePosition - mouseInCanvas * _zoom;

		UpdateCanvas();
	}

	private void UpdateCanvas()
	{
		_skillTreeContainer.Position = _panOffset;
		_skillTreeContainer.Scale = Vector2.One * _zoom;
	}

	public void SpendSkillPoints(int amount)
	{
		if (SkillPoints < amount)
			return;

		SkillPoints -= amount;
	}
}

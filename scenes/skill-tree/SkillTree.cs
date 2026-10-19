using Godot;

namespace IdleSim.scenes.skill_tree;

public partial class SkillTree : Control
{
	private bool _dragging;
	private Vector2 _lastMousePosition;

	private ScrollContainer _scrollContainer;
	public int SkillPoints { get; private set; } = 5;

	public override void _Ready()
	{
		_scrollContainer = GetNode<ScrollContainer>("ScrollContainer");
		CallDeferred(MethodName.CenterSkillTree);
	}

	public override void _Input(InputEvent @event)
	{
		
		if (@event is InputEventMouseButton
				{ ButtonIndex: MouseButton.Middle or MouseButton.Right } mouseButton)
		{
			_dragging = mouseButton.Pressed;
			_lastMousePosition = GetGlobalMousePosition();
		}

		if (@event is not InputEventMouseMotion || !_dragging) return;

		var mousePosition = GetGlobalMousePosition();
		var movement = mousePosition - _lastMousePosition;

		_scrollContainer.ScrollHorizontal -= (int)movement.X;
		_scrollContainer.ScrollVertical -= (int)movement.Y;

		_lastMousePosition = mousePosition;
	}

	public void SpendSkillPoints(int amount)
	{
		if (SkillPoints < amount) return;

		SkillPoints -= amount;
	}

	private void CenterSkillTree()
	{
		_scrollContainer.ScrollHorizontal = 2000 - (int)(_scrollContainer.Size.X / 2);
		_scrollContainer.ScrollVertical = 2000 - (int)(_scrollContainer.Size.Y / 2);
	}
}

using Godot;

namespace IdleSim.scenes.skill_tree.components;

public partial class Connection : Node2D
{
	private Vector2 _parentPosition;
	private Vector2 _childPosition;

	public void SetConnection(Vector2 parentPosition, Vector2 childPosition)
	{
		_parentPosition = parentPosition;
		_childPosition = childPosition;

		QueueRedraw();
	}

	public override void _Draw()
	{
		DrawLine(
			_parentPosition,
			_childPosition,
			Colors.Black,
			5
		);
	}
}

using Godot;

public partial class Connections : Node2D
{
	public override void _Draw()
	{
		DrawLine(
			new Vector2(0, 0),
			new Vector2(576, 324),
			Colors. Black,
			5
		);

		DrawLine(
			new Vector2(1152, 0),
			new Vector2(576, 324),
			Colors.Pink,
			5
		);

		DrawLine(
			new Vector2(1152, 648),
			new Vector2(576, 324),
			Colors.Black,
			5
		);

		DrawLine(
			new Vector2(0, 648),
			new Vector2(576, 324),
			Colors.Black,
			5
		);
		
	}
}

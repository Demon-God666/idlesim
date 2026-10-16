using Godot;
using System;

public partial class Character : CharacterBody3D
{
	private const float Speed = 5.0f;
	
	public override void _PhysicsProcess(double delta)
	{
		var input = GetInputDirection();

		Velocity = new Vector3(
			input.X * Speed,
			0,
			input.Y * Speed
		);

		MoveAndSlide();
	}

	private Vector2 GetInputDirection()
	{
		return Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

	}
}

using Godot;
using System;

public partial class Character : CharacterBody3D
{
	private const float Speed = 5.0f;
	private const float SprintSpeed = 8.0f;
	private const float JumpVelocity = 4.5f;
	private const float Gravity = 9.8f;
	
	public override void _PhysicsProcess(double delta)
	{
		if (!IsOnFloor())
			Velocity = new Vector3(
				Velocity.X,
				Velocity.Y - Gravity * (float)delta,
				Velocity.Z
			);
		
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			Velocity = new Vector3(
				Velocity.X,
				JumpVelocity,
				Velocity.Z
			);
		
		var input = GetInputDirection();
		var currentSpeed = Input.IsActionPressed("sprint")
			? SprintSpeed
			: Speed;

		Velocity = new Vector3(
			input.X * currentSpeed,
			Velocity.Y,
			input.Y * currentSpeed
		);

		MoveAndSlide();
	}

	private Vector2 GetInputDirection()
	{
		return Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

	}
}

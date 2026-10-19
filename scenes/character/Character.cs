using Godot;

namespace IdleSim.scenes.character;

public partial class Character : CharacterBody3D
{
	private const float Speed = 5.0f;
	private const float SprintSpeed = 8.0f;
	private const float JumpVelocity = 4.5f;
	private const float Gravity = 9.8f;
	
	private Node3D _head;
	private Camera3D _camera;

	private float _mouseSensitivity = 0.002f;
	private float _cameraRotationX;
	
	public override void _Ready()
	{
		_head = GetNode<Node3D>("Head");
		_camera = GetNode<Camera3D>("Head/Camera3D");

		// Input.MouseMode = Input.MouseModeEnum.Captured;
	}
	
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
		var direction = (Transform.Basis * new Vector3(input.X, 0, input.Y)).Normalized();

		var currentSpeed = Input.IsActionPressed("sprint")
			? SprintSpeed
			: Speed;
			
		Velocity = new Vector3(
			direction.X * currentSpeed,
			Velocity.Y,
			direction.Z * currentSpeed
		);

		MoveAndSlide();
	}

	private Vector2 GetInputDirection()
	{
		return Input.GetVector("move_left", "move_right", "move_forward", "move_backward");

	}
	
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			RotateY(-mouseMotion.Relative.X * _mouseSensitivity);
			
			_cameraRotationX -= mouseMotion.Relative.Y * _mouseSensitivity;
			_cameraRotationX = Mathf.Clamp(
				_cameraRotationX,
				Mathf.DegToRad(-80),
				Mathf.DegToRad(80)
			);

			_head.Rotation = new Vector3(_cameraRotationX, 0, 0);
		}

		if (@event.IsActionPressed("ui_cancel"))
			Input.MouseMode = Input.MouseModeEnum.Visible;
		
		if (@event is InputEventMouseButton { Pressed: true } && Input.MouseMode == Input.MouseModeEnum.Visible)
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
	}
}
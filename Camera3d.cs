using Godot;
using System;

public partial class Camera3d : Camera3D
{
	[Export] public float Speed = 5f;

	public override void _Process(double delta)
	{
		Vector3 movement = Vector3.Zero;

		// Forward / backward
		if (Input.IsActionPressed("CamUp"))
			movement -= Transform.Basis.Z;

		if (Input.IsActionPressed("CamDown"))
			movement += Transform.Basis.Z;

		// Left / right
		if (Input.IsActionPressed("CamLeft"))
			movement -= Transform.Basis.X;

		if (Input.IsActionPressed("CamRight"))
			movement += Transform.Basis.X;

		// Left / right
		if (Input.IsActionPressed("CamIn"))
			movement -= Transform.Basis.Y;

		if (Input.IsActionPressed("CamOut"))
			movement += Transform.Basis.Y;

		// Normalize so diagonal movement isn't faster
		movement = movement.Normalized();

		Position += movement * Speed * (float)delta;
	}
}

using Godot;
using System;

public partial class Pulse : Node2D
{
	public double size = 0;
	private bool pulsing = false;

	public double growthSpeed = 0;
	//3 seconds

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("playaudio"))
		{
			pulsing = !pulsing;
		}
		if (pulsing)
		{
			size += (float)(delta * growthSpeed);
			if(size <= 0 || size >= 200){
				growthSpeed *= -1;
			}
			if(size <= 0)
			{
				GD.Print("Pulse Loop : " + Time.GetTicksMsec());
			}
			QueueRedraw();
		}
		
	}

	public override void _Draw()
	{
		DrawCircle(
			position: Vector2.Zero,
			radius: 25f,
			color: Colors.Red
		);

		DrawArc(
			center: Vector2.Zero,
			radius: (float) size,
			startAngle: 0,
			endAngle: Mathf.Tau,
			pointCount: 64,
			color: Colors.White,
			width: 4
		);
	}
}

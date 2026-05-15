using Godot;
using System;

public partial class Beat : Node3D
{
	public float trackMovementSpeed = 40;
	public double spawnTime;
	private AnimatedSprite3D fishSprite;
	private AnimatedSprite3D ringSprite;


	//The ring animation last 3.75 seconds by default
	public override void _Ready()
	{
		//Set up the fish sprite to play.
		fishSprite = GetNode<AnimatedSprite3D>("Fish");
		fishSprite.Play();
		fishSprite.RenderPriority = 0;
		fishSprite.SpeedScale = 1.3f;

		//Set up the loop sprite to play, and what to do when we timeout.
		ringSprite = GetNode<AnimatedSprite3D>("Fish/Ring");
		ringSprite.SpeedScale = 2;
		ringSprite.RenderPriority = 1;
		ringSprite.Play();
		ringSprite.AnimationFinished += RingTimeout;

		//Sizing correctly...
		Scale = new Vector3(0.3f, 0.3f, 0.3f);

		base._Ready();
	}


	public override void _Process(double delta)
	{
		base._Process(delta);

		float movement = (float) delta * trackMovementSpeed * 0.3f;

		Position = new Vector3(
			Mathf.MoveToward(Position.X, 0, movement),
			Mathf.MoveToward(Position.Y, 0, movement),
			Mathf.MoveToward(Position.Z, 0, movement)
		);	
	}

	private void RingTimeout()
	{
		this.QueueFree();
		//Probably send a signal back to let it know we failed the timing...
	}

}

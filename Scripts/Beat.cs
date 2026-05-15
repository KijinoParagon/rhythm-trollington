using Godot;
using System;

public partial class Beat : Node3D
{
	[Signal]
    public delegate void NoteHitEventHandler(int score);

    [Signal]
    public delegate void NoteMissEventHandler();

	
	public float trackMovementSpeed = 40;
	public double noteTime;
	public double hitWindow;
	public int score = 1;
	private AnimatedSprite3D fishSprite;
	private AnimatedSprite3D ringSprite;
	public NoteData noteData;
	public bool active = false;
	public bool inQueue = true;
	public double timer;
	public int number;



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

		ringSprite.SpeedScale = (float) (ringSprite.SpriteFrames.GetFrameCount("default") / ringSprite.SpriteFrames.GetAnimationSpeed("default")) / (float) noteTime;
		ringSprite.RenderPriority = 1;
		ringSprite.Play();
		ringSprite.AnimationFinished += RingTimeout;

		//Sizing correctly...
		Scale = new Vector3(0.3f, 0.3f, 0.3f);

		base._Ready();
	}


	public override void _Process(double delta)
	{
		if (!inQueue)
		{
			Free();
			return;
		}
		if (Input.IsActionJustPressed("NoteHit") && active)
		{
			if(noteTime <= hitWindow)
			{
				GD.Print("HIT!");
				EmitSignal(SignalName.NoteHit, score);
			}
			else
			{
				GD.Print("MISTIMED!");
				EmitSignal(SignalName.NoteMiss);
			}
		}
		noteTime -= delta;
		float movement = (float) delta * trackMovementSpeed;

		Position = new Vector3(
			Mathf.MoveToward(Position.X, 0, movement),
			Mathf.MoveToward(Position.Y, 0, movement),
			Mathf.MoveToward(Position.Z, 0, movement)
		);

		

	}

	private void RingTimeout()
	{
		GD.Print("TIMEOUT");
		EmitSignal(SignalName.NoteMiss);
		//Probably send a signal back to let it know we failed the timing...
	}

}

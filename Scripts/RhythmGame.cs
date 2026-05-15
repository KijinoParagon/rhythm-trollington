using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class RhythmGame : Node3D
{
	private SongController _songController;
	//private NoteSpawner _noteSpawner;
	//private InputJudge _inputJudge;
	private UIController _uiController;
	private readonly PackedScene _beatScene = GD.Load<PackedScene>("res://Beat.tscn");
	private Queue<Beat> beats = new Queue<Beat>();
	private int Score;
	private int beatCount = 1;
	private double spawnCountdown = 1;
	[Export]
	public AudioStream song;

	public override void _Ready()
	{
		_songController = new SongController();

		_songController.init(1, song);

		AddChild(_songController);
		_songController.Start();
		_songController.SongFinished += FinishSong;
		//_noteSpawner = GetNode<NoteSpawner>("NoteSpawner");
		//_inputJudge = GetNode<InputJudge>("InputJudge");
		//spawn


		_uiController = GetNode<UIController>("Camera3D/UIController");



		/*  RythmGame.cs    - Orchestrates the game scene
				SongController.cs
					NoteData.cs
					Note.cs
					NoteSpawner.cs
					InputJudge.cs
				Camera3d.cs
				UIController.cs + ScoreManager.cs
					
		*/
	}
	public override void _Process(double delta)
	{
		spawnCountdown -= delta;
		if(spawnCountdown <= 0)
		{
			spawnCountdown = 2;
			SpawnBeat(5, 1, 10);
		}
		if(beats.Count != 0)
		{
			beats.First().active = true;
		}
	}

	private void SpawnBeat(double noteTime, double hitWindow, float moveSpeed)
	{
		Beat b = _beatScene.Instantiate<Beat>();
		
		beats.Enqueue(b);

		b.NoteHit += Hit;
		b.NoteMiss += Miss;
		b.noteTime = noteTime;
		b.hitWindow = hitWindow;
		b.trackMovementSpeed = moveSpeed;
		b.Position = new Vector3(-moveSpeed * (float) noteTime, 0, 0);
		b.number = beatCount;
		beatCount++;
		AddChild(b);
	}
	private void Hit(int scorePoints)
	{
		Score += scorePoints; 
		_uiController.UpdateScore(Score); 
		GD.Print(Score);
		Beat b = beats.Dequeue();
		b.inQueue = false;			
	}
	private void Miss()
	{
		Beat b = beats.Dequeue();
		b.inQueue = false;	
	}

	private void FinishSong()
	{
		_uiController.UpdateScore(-1);
	}
}

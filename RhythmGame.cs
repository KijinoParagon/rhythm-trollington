using Godot;
using System;


public partial class RhythmGame : Node3D
{
    private const float SpeedStep = 0.5f;
    private const float MinSpeed = 0.5f;
    private const float MaxSpeed = 3.0f;

    private AudioStreamPlayer _player;
    private Camera3D _camera;
    private RandomNumberGenerator _rng = new();

    private float _audioSpeed = 1.0f;

    private readonly PackedScene _noteScene =
        GD.Load<PackedScene>("res://Beat.tscn");

    public override void _Ready()
    {
        _rng.Randomize();

        _player = GetNode<AudioStreamPlayer>("Tinktink");
        _camera = GetNode<Camera3D>("Camera3D");

        _camera.MakeCurrent();
    }

    public override void _Process(double delta)
	{
		
	}

}

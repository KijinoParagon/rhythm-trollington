using Godot;
using System;
using System.IO;

public partial class SongController : Node
{

    [Signal]
    public delegate void SongFinishedEventHandler();
    private AudioStreamPlayer _player;

    private float _speed = 1f;

    public double SongTimeMs =>
        _player.GetPlaybackPosition() * 1000.0;

    public override void _Ready()
    {
        _player.Finished += ()=>{EmitSignal(SignalName.SongFinished);};
        AddChild(_player);
    }

    public void Start()
    {
        _player.Play();
    }


    public void TogglePlayback()
    {
        _player.StreamPaused = !_player.StreamPaused;
    }

    /*public void ChangeSpeed(float delta)
    {
        _speed = Mathf.Clamp(_speed + delta, 0.5f, 3f);

        double pos = _player.GetPlaybackPosition();

        _player.Stop();
        _player.PitchScale = _speed;
        _player.Play((float)pos);
    }*/


    public void init(float speed, AudioStream song)
    {
        this._speed = speed;
        this._player = new AudioStreamPlayer
        {
            Stream = song,
            PitchScale = speed
        };
    }
}
using Godot;
using System;

public partial class InputJudge : Node
{
    [Signal]
    public delegate void NoteHitEventHandler(int score);

    [Signal]
    public delegate void NoteMissedEventHandler();

    private SongController _songController;

    public override void _Ready()
    {
        _songController =
            GetNode<SongController>("../SongController");
    }

    public override void _Process(double delta)
    {
        CheckLane("lane_1", 0);
        CheckLane("lane_2", 1);
        CheckLane("lane_3", 2);
        CheckLane("lane_4", 3);
    }

    private void CheckLane(string action, int lane)
    {
        if (!Input.IsActionJustPressed(action))
            return;

        double currentTime = _songController.SongTimeMs;

        GD.Print($"Lane {lane} pressed at {currentTime}");

        EmitSignal(SignalName.NoteHit, 100);
    }
}
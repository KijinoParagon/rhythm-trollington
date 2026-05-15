using Godot;
using System.Collections.Generic;

public partial class NoteSpawner : Node3D
{
    [Export]
    public PackedScene NoteScene;

    [Export]
    public float SpawnAheadMs = 2000;

    private SongController _songController;

    private readonly List<NoteData> _notes = new();

    private int _nextIndex = 0;

    public override void _Ready()
    {
        _songController =
            GetNode<SongController>("../SongController");

        LoadTestChart();
    }

    public override void _Process(double delta)
    {
        double songTime = _songController.SongTimeMs;

        while (_nextIndex < _notes.Count &&
               _notes[_nextIndex].TimeMs <= songTime + SpawnAheadMs)
        {
            SpawnNote(_notes[_nextIndex]);

            _nextIndex++;
        }
    }

    private void SpawnNote(NoteData data)
    {
        var note = NoteScene.Instantiate<Beat>();

        AddChild(note);
    }

    private void LoadTestChart()
    {
        _notes.Add(new NoteData(1000, 0));
        _notes.Add(new NoteData(1500, 1));
        _notes.Add(new NoteData(2000, 2));
        _notes.Add(new NoteData(2500, 3));
    }
}
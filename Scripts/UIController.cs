using Godot;

public partial class UIController : Node
{
	private Label _scoreLabel;

	public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("ScoreLabel");
		_scoreLabel.Text = $"Score: 0";
	}

	public void UpdateScore(int score)
	{
		_scoreLabel.Text = $"Score: {score}";
	}
}

using Godot;

public partial class LoadingBar : Control
{
    private ProgressBar _loadingBar;
    private float progress = 0.0f;
    private int progressTime = 0;

    public override void _Ready()
    {
        _loadingBar = GetNode<ProgressBar>("ProgressBar");
    }

    public void SetProgress(double value)
    {
        _loadingBar.Value = value;
    }
}
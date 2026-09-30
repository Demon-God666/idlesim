using Godot;

namespace IdleSim.components;

public partial class LoadingBar : Control
{
    private ProgressBar _loadingBar;
    private float _progress;
    private int _progressTime;

    public override void _Ready()
    {
        _loadingBar = GetNode<ProgressBar>("ProgressBar");
    }

    public void SetProgress(double value)
    {
        _loadingBar.Value = value;
    }
}
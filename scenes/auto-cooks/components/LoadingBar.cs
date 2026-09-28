using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class LoadingBar : Control
{
	private ProgressBar _loadingBar;
	private float progress = 0.0f;
	private int progressTime = 0;
	
	public override void _Ready()
	{
		_loadingBar = GetNode<ProgressBar>("ProgressBar");
	}
	public override void _Process(double delta)
	{
	}

	public async Task SetLoadingBar(int produceTime)
	{
		var progressPerSecond = 100.0f / produceTime;
		
			for (int i = 0; i < produceTime; i++)
			{
				_loadingBar.Value += progressPerSecond;
				GD.Print(_loadingBar.Value);
				await Task.Delay(1000);
			}

			_loadingBar.Value = 0;
		
	}
}

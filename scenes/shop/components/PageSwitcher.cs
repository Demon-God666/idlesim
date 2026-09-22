using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;

public partial class PageSwitcher : Control
{
	private Shop _shop;
	public int CurrentPage { get; private set; } = 1;
	private Button _backButton;
	private Button _forwardButton;

	private Panel _activeDot;
	private Panel _inactiveDot;
	
	private int _maxPage;
	
	public override void _Ready()
	{
		_shop = GetNode<Shop>("../..");
		_backButton = GetNode<Button>("BackButton");
		_forwardButton = GetNode<Button>("ForwardButton");
		
		_backButton.Disabled = true;
		
		_backButton.Pressed += BackButtonPressed;
		_forwardButton.Pressed += ForwardButtonPressed;
		
	}
	
	public override void _Process(double delta)
	{
	}
	
	private void BackButtonPressed()
	{
		CurrentPage--;
		_shop.DisplayShopItems();
		_forwardButton.Disabled = false;
		if (CurrentPage <= 1)
		{
			_backButton.Disabled = true;
		}
	}

	private void ForwardButtonPressed()
	{
		CurrentPage++;
		_shop.DisplayShopItems();
		_backButton.Disabled = false;
		if (CurrentPage >= _maxPage)
		{
			_forwardButton.Disabled = true;
		}
	}
	
	public void GetMaxPage(int itemCount)
	{
		_maxPage = Mathf.CeilToInt(itemCount / 8.0f);
	}
}

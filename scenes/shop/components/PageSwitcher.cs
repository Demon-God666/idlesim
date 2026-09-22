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
	private HBoxContainer _dotsContainer;
	
	private Panel _currentActiveDot;
	
	private int _maxPage;
	
	public override void _Ready()
	{
		_shop = GetNode<Shop>("../..");
		_backButton = GetNode<Button>("BackButton");
		_forwardButton = GetNode<Button>("ForwardButton");
		
		_activeDot = GetNode<Panel>("DotTemplates/ActiveDot");
		_inactiveDot = GetNode<Panel>("DotTemplates/InactiveDot");
		_dotsContainer = GetNode<HBoxContainer>("HBoxContainer");
		
		
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
		MoveActiveDot();
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
		MoveActiveDot();
	}
	
	public void GetMaxPage(int itemCount)
	{
		_maxPage = Mathf.CeilToInt(itemCount / 8.0f);
		GetDots();
	}

	private void GetDots()
	{
		foreach (Node child in _dotsContainer.GetChildren())
		{
			child.Free();
		}

		for (int i = 0; i < _maxPage; i++)
		{
			Panel dot;

			if (i == 0)
			{
				dot = _activeDot.Duplicate() as Panel;
				_currentActiveDot = dot;
			}
			else
			{
				dot = _inactiveDot.Duplicate() as Panel;
			}

			_dotsContainer.AddChild(dot);
		}
	}
	
	private void MoveActiveDot()
	{
		_dotsContainer.MoveChild(_currentActiveDot, CurrentPage - 1 );
	}
}

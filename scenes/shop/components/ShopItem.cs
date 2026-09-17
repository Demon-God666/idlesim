using Godot;

public partial class ShopItem : Control
{
	private CurrencySystem _currencySystem;
	
	private Button _addButton;
	private Button _removeButton;
	private Label _productAmountLabel;
	private int _productAmountValue;
	
	private Label _productPriceLabel;
	private int _productPriceValue;
	
	private Button _buttonBuy;

	public override void _Ready()
	{
		_currencySystem = GetTree().Root.GetNode<CurrencySystem>("Main/CurrencySystem");
		
		_addButton = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ButtonAdd");
		_removeButton = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ButtonRemove");
		_productAmountLabel = GetNode<Label>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ProductAmountLabel");
		_productPriceLabel = GetNode<Label>("VBoxContainer/ButtonContainer/ButtonContainerBuy/ProductPriceLabel");
		_buttonBuy = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerBuy/ButtonBuy");
		
		_productAmountValue = 1;
		_productPriceValue = 10;

		UpdateProductAmountLabel();
		UpdateProductPriceLabel();

		_addButton.Pressed += () => Add(1);
		_removeButton.Pressed += () => Remove(1);
		_buttonBuy.Pressed += Buy;
	}

	private void Add(int amount)
	{
		_productAmountValue += amount;
		UpdateProductAmountLabel();
	}

	private void Remove(int amount)
	{
		if (_productAmountValue <= 1)
		{
			return;
		}

		_productAmountValue -= amount;
		UpdateProductAmountLabel();
	}

	
	private void Buy()
	{
		int price = _productPriceValue * _productAmountValue;

		if (_currencySystem.GetMoney() < price)
		{
			GD.Print("Nicht genug Geld.");
			return;
		}

		_currencySystem.Remove(price);

		GD.Print($"Gekauft: {_productAmountValue}x für {price}$");
	}

	private void UpdateProductAmountLabel()
	{
		_productAmountLabel.Text = _productAmountValue.ToString();
		UpdateProductPriceLabel();
	}

	private void UpdateProductPriceLabel()
	{
		_productPriceLabel.Text = (_productPriceValue * _productAmountValue) + "$";
	}

}

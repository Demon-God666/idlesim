using Godot;

public partial class ShopItem : Control
{
	private Button _addButton;
	private Button _removeButton;
	private Label _productAmountLabel;
	private int _productAmountValue;
	private Label _productPriceLabel;
	private int _productPriceValue;

	public override void _Ready()
	{
		_addButton = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ButtonAdd");
		_removeButton = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ButtonRemove");
		_productAmountLabel = GetNode<Label>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ProductAmountLabel");
		_productPriceLabel = GetNode<Label>("VBoxContainer/ButtonContainer/ButtonContainerBuy/ProductPriceLabel");
		
		_productAmountLabel.Text = "1";
		_productAmountValue = _productAmountLabel.Text.ToInt();
		
		_productPriceLabel.Text = 10 + "$";
		_productPriceValue = _productPriceLabel.Text.Replace("$", "").ToInt();

		_addButton.Pressed += () => Add(1);
		_removeButton.Pressed += () => Remove(1);
	}

	private void Add(int amount)
	{
		_productAmountValue += amount;
		UpdateProductAmountLabel();

	}

	private void Remove(int amount)
	{
		_productAmountValue -= amount;
		UpdateProductAmountLabel();
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

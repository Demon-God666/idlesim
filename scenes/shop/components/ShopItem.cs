using Godot;

public partial class ShopItem : Control
{
	private Button _addButton;
	private Button _removeButton;
	private Label _productAmountLabel;
	private int _productAmountValue;

	public override void _Ready()
	{
		_addButton = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ButtonAdd");
		_removeButton = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ButtonRemove");
		_productAmountLabel = GetNode<Label>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ProductAmountLabel");

		_productAmountValue = _productAmountLabel.Text.ToInt();

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
	}

}

using Godot;
using IdleSim.scenes.inventory;
using IdleSim.scenes.inventory.components;
using IdleSim.scenes.shop.components;

public partial class ShopItem : Control
{
	private CurrencySystem _currencySystem;
	private Inventory _inventory;
	private ItemData _item;
	
	private Button _addButton;
	private Button _removeButton;
	private Label _productAmountLabel;
	private int _productAmountValue;
	
	private Label _productNameLabel;
	private Label _productPriceLabel;
	private int _productPriceValue;
	private TextureRect _productImage;
	
	private Button _buttonBuy;

	public override void _Ready()
	{
		
		_currencySystem = GetTree().Root.GetNode<CurrencySystem>("Main/CurrencySystem");
		_inventory = GetTree().Root.GetNode<Inventory>("Main/Inventory");
		
		_addButton = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ButtonAdd");
		_removeButton = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ButtonRemove");
		_productAmountLabel = GetNode<Label>("VBoxContainer/ButtonContainer/ButtonContainerAmount/ProductAmountLabel");
		_buttonBuy = GetNode<Button>("VBoxContainer/ButtonContainer/ButtonContainerBuy/ButtonBuy");

		_productNameLabel = GetNode<Label>("VBoxContainer/ProductLabel");
		_productPriceLabel = GetNode<Label>("VBoxContainer/ButtonContainer/ButtonContainerBuy/ProductPriceLabel");
		_productImage = GetNode<TextureRect>("VBoxContainer/ImageContainer/AspectRatioContainer/ProductImage");
		
		
		_productAmountValue = 1;
		

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
		
		_inventory.Add(_item, _productAmountValue);
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

	public void SetItem(ItemData item)
	{
		_item = item;
		
		_productNameLabel.Text= item.ProductName;
		_productPriceValue = item.ProductPrice;
		_productImage.Texture = item.ProductImage;
		
		LoadImageShadow();
		UpdateProductPriceLabel();
	}

	private void LoadImageShadow()
	{
		var imageAspect = _productImage.GetParent<AspectRatioContainer>();

		var shadowAspect = (AspectRatioContainer)imageAspect.Duplicate();
		shadowAspect.Name = "ShadowAspectRatioContainer";

		var parent = imageAspect.GetParent();

		parent.AddChild(shadowAspect);
		parent.MoveChild(shadowAspect, imageAspect.GetIndex());

		var shadowTexture = shadowAspect.GetNode<TextureRect>("ProductImage");
		shadowTexture.Name = "ShadowTexture";
		shadowTexture.Modulate = new Color(0, 0, 0, 0.4f);

		shadowAspect.Position += new Vector2(10, 10);
	}

}

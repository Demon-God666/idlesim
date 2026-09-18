using Godot;

namespace IdleSim.scenes.shop.components;

public class ShopItemData
{
	public string ProductName { get; set; }
	public int ProductPrice { get; set; }
	public Texture2D ProductImage { get; set; }

	public ShopItemData(string productName, int productPrice, Texture2D productImage)
	{
		ProductName = productName;
		ProductPrice = productPrice;
		ProductImage = productImage;
	}
}

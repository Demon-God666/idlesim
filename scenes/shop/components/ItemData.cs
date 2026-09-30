using Godot;

namespace IdleSim.scenes.shop.components;

public class ItemData(string productName, int productPrice, Texture2D productImage, int category)
{
    public string ProductName { get; } = productName;
    public int ProductPrice { get; } = productPrice;
    public Texture2D ProductImage { get; } = productImage;
    public int Category { get; } = category;
}
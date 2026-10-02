using Godot;

namespace IdleSim.scenes.shop.components;

public class ItemData(string productName, int productPrice, string productImage, int category)
{
    public string ProductName { get; } = productName;
    public int ProductPrice { get; } = productPrice;
    public string ProductImage { get; } = productImage;
    public int Category { get; } = category;
}
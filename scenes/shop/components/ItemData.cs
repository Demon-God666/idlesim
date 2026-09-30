using Godot;

namespace IdleSim.scenes.shop.components;

public class ItemData(string productName, int productPrice, Texture2D productImage, Category category)
{
    public string ProductName { get; } = productName;
    public int ProductPrice { get; } = productPrice;
    public Texture2D ProductImage { get; } = productImage;
    public Category Category { get; } = category;
}
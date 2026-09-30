using Godot;

namespace IdleSim.scenes.shop.components;

public class ItemData(string productName, int productPrice, Texture2D productImage, Category category)
{
    public string ProductName { get; set; } = productName;
    public int ProductPrice { get; set; } = productPrice;
    public Texture2D ProductImage { get; set; } = productImage;

    public Category Category { get; set; } = category;
}
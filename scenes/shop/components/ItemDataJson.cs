namespace IdleSim.scenes.shop.components;

public class ItemDataJson
{
    public ItemDataJson(string name, int price, string image, int category)
    {
        Name = name;
        Price = price;
        Image = image;
        Category = category;
    }

    public string Name { get; }
    public int Price { get; }
    public string Image { get; }
    public int Category { get; }
}
namespace IdleSim.scenes.shop.components;

public class Category(int categoryId, string categoryLabel)
{
    public int CategoryId { get; } = categoryId;
    public string CategoryLabel { get; } = categoryLabel;
}
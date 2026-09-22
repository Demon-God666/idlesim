namespace IdleSim.scenes.shop.components;

public class Category
{
    public int CategoryId { get; }
    public string CategoryLabel { get; }

    public Category(int categoryId, string categoryLabel)
    {
        CategoryId = categoryId;
        CategoryLabel = categoryLabel;
    }
}
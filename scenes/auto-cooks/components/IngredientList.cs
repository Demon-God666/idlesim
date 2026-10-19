namespace IdleSim.scenes.auto_cooks.components;

public class IngredientList(string itemName, int itemAmount)
{
    public readonly int ItemAmount = itemAmount;
    public readonly string ItemName = itemName;
}
using System.Collections.Generic;

namespace IdleSim.scenes.auto_cooks.components;

public class IngredientList
{
    public string ItemName;
    public int ItemAmount;
    
    public IngredientList(string itemName, int itemAmount)
    {
        ItemName = itemName;
        ItemAmount = itemAmount;
    }
}
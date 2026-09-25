using System.Collections.Generic;
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.auto_cooks.components;

public class Dishes
{
    public List<IngredientList> IngredientList = new();
    
    public ItemData ItemData { get; }
    public int ProduceTime { get; }
    
    public Dishes(List<IngredientList> ingredientList, ItemData itemData, int produceTime)
    {
        IngredientList = ingredientList;
        ItemData = itemData;
        ProduceTime = produceTime;
    }
}
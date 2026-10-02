using System.Collections.Generic;
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.auto_cooks.components;

public class Dish(List<IngredientList> ingredientList, ItemData itemData, int produceTime)
{
    public readonly List<IngredientList> IngredientList = ingredientList;
    public ItemData ItemData { get; } = itemData;
    public int ProduceTime { get; } = produceTime;
}
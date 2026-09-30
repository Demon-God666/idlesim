using System.Collections.Generic;

namespace IdleSim.scenes.auto_cooks.components;

public class DishesJson(string itemData, List<IngredientListJson> ingredients, int produceTime)
{
    public string ItemData { get; } = itemData;
    public List<IngredientListJson> Ingredients { get; } = ingredients;
    public int ProduceTime { get; } = produceTime;
}
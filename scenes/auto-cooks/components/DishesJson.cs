using System.Collections.Generic;

namespace IdleSim.scenes.auto_cooks.components;

public class DishesJson
{
    public string ItemData { get; set; }
    public List<IngredientListJson> Ingredients { get; set; }
    public int ProduceTime { get; set; }
}
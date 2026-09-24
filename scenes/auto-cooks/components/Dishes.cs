using System.Collections.Generic;

namespace IdleSim.scenes.auto_cooks.components;

public class Dishes
{
    public List<IngredientList> IngredientList = new();
    public string ProduceItem { get; }
    public int ProducedProductValue {get;}
    public int ProduceTime { get; }
    
    public Dishes(List<IngredientList> ingredientList, string produceItem, int producedProductValue, int produceTime)
    {
        IngredientList = ingredientList;
        ProduceItem = produceItem;
        ProducedProductValue = producedProductValue;
        ProduceTime = produceTime;
    }
}
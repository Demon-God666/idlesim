using System.Collections.Generic;

namespace IdleSim.scenes.auto_cooks.components;

public class Dishes
{
    public List<IngredientList> IngredientList;
    private string _produceItem;
    private int _producedProductValue;
    private int _produceTime;
    
    public Dishes((string, int) ingredientList, string produceItem, int producedProductValue, int produceTime)
    {
        IngredientList = new List<IngredientList>();
        _produceItem = produceItem;
        _producedProductValue = producedProductValue;
        _produceTime = produceTime;
    }
}
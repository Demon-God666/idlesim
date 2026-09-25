using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.inventory.components;

public class BoughtItem
{
    public ItemData Item;
    public int Amount;

    public BoughtItem(ItemData item, int amount)
    {
        Item = item;
        Amount = amount;
    }
    
}
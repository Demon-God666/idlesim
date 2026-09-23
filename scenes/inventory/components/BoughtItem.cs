using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.inventory.components;

public class BoughtItem
{
    public ShopItemData Item;
    public int Amount;

    public BoughtItem(ShopItemData item, int amount)
    {
        Item = item;
        Amount = amount;
    }
}
using IdleSim.scenes.shop.components;

namespace IdleSim.scenes.inventory.components;

public class BoughtItem(ItemData item, int amount)
{
    public readonly ItemData Item = item;
    public int Amount = amount;
}
using Godot;
using IdleSim.scenes.main;

namespace IdleSim.scenes.currency_system;

public partial class CurrencySystem : Control
{
    private Main _main;
    private Button _buyButton;
    private Label _label;
    private Button _minusButton;
    private Button _plusButton;
    
    private Label _valueLabel;

    public int Money { get; set; }
    public int SkillPoints { get; set; }
    
    public override void _Ready()

    {
        _main = GetTree().Root.GetNode<Main>("Main");
        _valueLabel = GetNode<Label>("Container/Value");
        _label = GetNode<Label>("Container/Label");
        _minusButton = GetNode<Button>("Container/MinusButton");
        _plusButton = GetNode<Button>("Container/PlusButton");
        
        _label.Text = "Money:";

        Money = _main.Money;
        SkillPoints = _main.SkillPoints;
        
        AddMoney(Money);
        AddSkillPoints(SkillPoints);

        _plusButton.Pressed += () => AddMoney(1);
        _minusButton.Pressed += () => RemoveMoney(1);

    }

   

    public void AddMoney(int amount)
    {
        Money += amount;
        _main.UpdateMoneyValue();
    }

    public bool RemoveMoney(int amount)
    {
        if (Money < amount) return false;

        Money -= amount;
        _main.UpdateMoneyValue();

        return true;
    }
    
    public void AddSkillPoints(int amount)
    {
        SkillPoints += amount;
        _main.UpdateSkillPoints();
    }

    public bool RemoveSkillPoints(int amount)
    {
        if (SkillPoints < amount) return false;

        SkillPoints -= amount;
        _main.UpdateSkillPoints();

        return true;
    }
    
}
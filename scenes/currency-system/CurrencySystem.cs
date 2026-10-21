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
    
    public override void _Ready()

    {
        _main = GetTree().Root.GetNode<Main>("Main");
        _valueLabel = GetNode<Label>("Container/Value");
        _label = GetNode<Label>("Container/Label");
        _minusButton = GetNode<Button>("Container/MinusButton");
        _plusButton = GetNode<Button>("Container/PlusButton");
   
        UpdateValueLabel();

        _plusButton.Pressed += () => AddMoney(1);
        _minusButton.Pressed += () => RemoveMoney(1);

    }

    private void UpdateValueLabel()
    {
        _valueLabel.Text = _main.Money.ToString();
    }

    public void AddMoney(int amount)
    {
        _main.Money += amount;
        _main.UpdateMoneyValue();
        UpdateValueLabel();
    }

    public bool RemoveMoney(int amount)
    {
        if (_main.Money < amount) return false;

        _main.Money -= amount;
        _main.UpdateMoneyValue();
        UpdateValueLabel();
        
        return true;
    }

    public bool RemoveSkillPoints(int amount)
    {
        if (_main.SkillPoints < amount) return false;

        _main.SkillPoints -= amount;
        _main.UpdateSkillPoints();
        
        return true;
    }
    
}
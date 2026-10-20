using Godot;

namespace IdleSim.scenes.currency_system;

public partial class CurrencySystem : Control
{
    [Signal]
    public delegate void MoneyUpdatedEventHandler(int value);
    
    [Signal]
    public delegate void SkillPointsUpdatedEventHandler(int skillPoints);

    private Button _buyButton;
    private Label _label;
    private Button _minusButton;
    private Button _plusButton;
    public int Value { get; set; }

    public int SkillPoints { get; set; }
    
    private Label _valueLabel;

    public override void _Ready()
    {
        _valueLabel = GetNode<Label>("Container/Value");
        _label = GetNode<Label>("Container/Label");
        _minusButton = GetNode<Button>("Container/MinusButton");
        _plusButton = GetNode<Button>("Container/PlusButton");

        Value = _valueLabel.Text.ToInt();
        _label.Text = "Money:";
        
        AddMoney(100);
        AddSkillPoints(200);

        _plusButton.Pressed += () => AddMoney(1);
        _minusButton.Pressed += () => RemoveMoney(1);

        EmitSignal(SignalName.MoneyUpdated, Value);
        EmitSignal(SignalName.SkillPointsUpdated, SkillPoints);

    }

    public void AddMoney(int amount)
    {
        Value += amount;
        UpdateMoneyValue();
    }

    public bool RemoveMoney(int amount)
    {
        if (Value < amount) return false;

        Value -= amount;
        UpdateMoneyValue();

        return true;
    }

    private void UpdateMoneyValue()
    {
        EmitSignal(SignalName.MoneyUpdated, Value);
        _valueLabel.Text = Value.ToString();
    }
    
    public void AddSkillPoints(int amount)
    {
        SkillPoints += amount;
        UpdateSkillPoints();
    }

    public bool RemoveSkillPoints(int amount)
    {
        if (SkillPoints < amount) return false;

        SkillPoints -= amount;
        UpdateSkillPoints();

        return true;
    }
    
    private void UpdateSkillPoints()
    {
        EmitSignal(SignalName.SkillPointsUpdated, SkillPoints);
    }
}
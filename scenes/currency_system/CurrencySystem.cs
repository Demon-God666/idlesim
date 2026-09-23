using Godot;

public partial class CurrencySystem : Control
{
    public int Money { get;  private set; }
    
    private Label _valueLabel;
    private Label _label;
    private Button _minusButton;
    private Button _plusButton;
    private Button _buyButton;

    [Signal]
    public delegate void MoneyUpdatedEventHandler(int value);

    public override void _Ready()
    {
        _valueLabel = GetNode<Label>("Container/Value");
        _label = GetNode<Label>("Container/Label");
        _minusButton = GetNode<Button>("Container/MinusButton");
        _plusButton = GetNode<Button>("Container/PlusButton");
        _buyButton = GetNode<Button>("Container/BuyButton");
        
        _plusButton.Pressed += () => Add(1);
        _minusButton.Pressed += () => Remove(1);
        _buyButton.Pressed += () => Add(10);

        UpdateUI();
    }

    private void Add(int amount)
    {
        Money += amount;
        UpdateUI();
        EmitSignal(SignalName.MoneyUpdated, Money);

    }

    private void Remove(int amount)
    {
        Money = Mathf.Max(0, Money - amount);
        UpdateUI();
        EmitSignal(SignalName.MoneyUpdated, Money);
    }

    private void UpdateUI()
    {
        _valueLabel.Text = Money.ToString();
    }
}
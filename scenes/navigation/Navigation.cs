using Godot;

public partial class Navigation : Control
{
    private Main _main;
    private CurrencySystem _currencySystem;

    private Button _currencySystemButton;
    private Button _shopButton;
    private Label _moneyLabel;

    public override void _Ready()
    {
        // ===== Get access to currency system and listen to any updates =====
        _main = GetParent<Main>();
        _currencySystem = _main.GetNode<CurrencySystem>("CurrencySystem");
        _currencySystem.MoneyUpdated += UpdateMoneyText;
        // ===================================================================

        _currencySystemButton = GetNode<Button>("CurrencySystemButton");
        _shopButton = GetNode<Button>("ShopButton");
        _moneyLabel = GetNode<Label>("MoneyLabel");

        _currencySystemButton.Pressed += _main.ShowCurrencySystem;
        _shopButton.Pressed += _main.ShowShop;
        
        UpdateMoneyText(_currencySystem.Money);
    }

    private void UpdateMoneyText(int value)
    {
        _moneyLabel.Text = value.ToString();
    }
}
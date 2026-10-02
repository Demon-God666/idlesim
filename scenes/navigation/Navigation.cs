using Godot;
using IdleSim.scenes.currency_system;
using Main = IdleSim.scenes.main.Main;

namespace IdleSim.scenes.navigation;

public partial class Navigation : Control
{
    private Button _autoCookButton;
    private CurrencySystem _currencySystem;

    private Button _currencySystemButton;
    private Button _inventoryButton;
    private Main _main;
    private Label _moneyLabel;
    private Button _shopButton;
    private Button _skillTreeButton;

    public override void _Ready()
    {
        _main = GetParent<Main>();
        _currencySystem = GetTree().Root.GetNode<CurrencySystem>("Main/CurrencySystem");

        _currencySystemButton = GetNode<Button>("HBoxContainer/CurrencySystemButton");
        _skillTreeButton = GetNode<Button>("HBoxContainer/SkillTreeButton");
        _shopButton = GetNode<Button>("HBoxContainer/ShopButton");
        _inventoryButton = GetNode<Button>("HBoxContainer/InventoryButton");
        _autoCookButton = GetNode<Button>("HBoxContainer/AutoCookButton");
        _moneyLabel = GetNode<Label>("HBoxContainer/MoneyLabel");

        _moneyLabel.Text = $"Money: {_currencySystem.GetMoney()}$";

        _currencySystemButton.Pressed += _main.ShowCurrencySystem;
        _skillTreeButton.Pressed += _main.ShowSkillTree;
        _shopButton.Pressed += _main.ShowShop;
        _inventoryButton.Pressed += _main.ShowInventory;
        _autoCookButton.Pressed += _main.ShowAutoCooks;


        _currencySystem.MoneyUpdated += UpdateMoneyText;
    }

    private void UpdateMoneyText(int value)
    {
        _moneyLabel.Text = $"Money: {value}$";
        GD.Print($"Money updated: {value}$");
    }
}
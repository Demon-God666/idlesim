using Godot;
using IdleSim.scenes.auto_cooks.components;
using IdleSim.scenes.inventory;

public partial class AutoCookItem : Control
{
	private AutoCooks _autoCooks;
	private Button _cookingButton;
	private Dishes _dish;
	private double _elapsedTime;
	private bool _hasStartedCooking;
	private Label _ingredientList;
	private Inventory _inventory;
	private bool _isCooking;

	private LoadingBar _loadingBar;
	private Label _produceItem;
	private Label _produceTime;
	private Label _producedProductValue;
	private double _producingTime;

	public override void _Ready()
	{
		_inventory = GetNode<Inventory>("../../../Inventory");

		_ingredientList = GetNode<Label>("VBoxContainer/IngredientListLabel");
		_produceItem = GetNode<Label>("VBoxContainer/ProduceItemLabel");
		_producedProductValue = GetNode<Label>("VBoxContainer/ProducedProductValueLabel");
		_produceTime = GetNode<Label>("VBoxContainer/ProduceTimeLabel");

		_loadingBar = GetNode<LoadingBar>("HBoxContainer/LoadingBar");
		_cookingButton = GetNode<Button>("HBoxContainer/StartAutoCookButton");
		_autoCooks = GetTree().Root.GetNode<AutoCooks>("Main/AutoCooks");

		_ingredientList.Text = "Ingredients: \n";
		_produceItem.Text = "Produce: \n";
		_producedProductValue.Text = "Value: \n";
		_produceTime.Text = "Time: \n";

		_cookingButton.Pressed += CookingButtonPressed;

		_inventory.InventoryUpdated += UpdateInventory;
	}

	public void SetAutoCookItem(Dishes dish)
	{
		_dish = dish;

		_produceItem.Text += dish.ItemData.ProductName;
		_producedProductValue.Text += dish.ItemData.ProductPrice + "$";
		_produceTime.Text += dish.ProduceTime + "s";

		UpdateInventory();
	}

	private int GetInventoryItemCount(string itemName)
	{
		var itemIndex = _inventory.InventoryItems.FindIndex(i => i.Item.ProductName == itemName);
		if (itemIndex < 0)
			return 0;

		var itemAmount = _inventory.InventoryItems[itemIndex];
		GD.Print(itemAmount);
		return itemAmount?.Amount ?? 0;
	}

	public void UpdateInventory()
	{
		if (_dish == null)
			return;

		_ingredientList.Text = "Ingredients: \n";

		foreach (var ingredient in _dish.IngredientList)
		{
			_ingredientList.Text +=
				$"{ingredient.ItemName} {GetInventoryItemCount(ingredient.ItemName)} / {ingredient.ItemAmount}";
			if (_dish.IngredientList.Count > 1 &&
				_dish.IngredientList.IndexOf(ingredient) < _dish.IngredientList.Count - 1) _ingredientList.Text += "\n";
		}
	}

	private void CookingButtonPressed()
	{
		if (_isCooking)
		{
			_isCooking = !_isCooking;
			return;
		}

		if (_hasStartedCooking)
		{
			_isCooking = !_isCooking;
			return;
		}

		var checkItem = _inventory.CheckIngredientAvailable(_dish.IngredientList);

		if (!checkItem)
		{
			GD.Print("Not enough ingredients");
			return;
		}

		foreach (var ingredient in _dish.IngredientList)
		{
			var item = _inventory.InventoryItems.Find(x => x.Item.ProductName == ingredient.ItemName
			);

			_inventory.Remove(item.Item, ingredient.ItemAmount);
		}

		_autoCooks.UpdateAllAutoCookItems();

		_producingTime = _dish.ProduceTime;
		_elapsedTime = 0;
		_hasStartedCooking = true;

		_loadingBar.SetProgress(0);

		_isCooking = !_isCooking;
	}

	public override void _Process(double delta)
	{
		if (!_isCooking)
			return;

		_elapsedTime += delta;

		var progress = _elapsedTime / _producingTime * 100.0;

		_loadingBar.SetProgress(progress);

		if (_elapsedTime >= _producingTime)
		{
			_isCooking = false;
			_hasStartedCooking = false;
			_elapsedTime = 0;

			_loadingBar.SetProgress(0);

			GD.Print("Cooking finished");
		}
	}
}

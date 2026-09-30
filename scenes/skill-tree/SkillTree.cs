using Godot;

namespace IdleSim.scenes.skill_tree;

public partial class SkillTree : Control
{
    public int SkillPoints { get; private set; } = 5;

    private ScrollContainer _scrollContainer;

    private bool _dragging;
    private Vector2 _lastMousePosition;

    public override void _Ready()
    {
        _scrollContainer = GetNode<ScrollContainer>("ScrollContainer");
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Middle } mouseButton)
        {
            _dragging = mouseButton.Pressed;
            _lastMousePosition = mouseButton.Position;
        }

        if (@event is not InputEventMouseMotion motion || !_dragging) return;
        var movement = motion.Position - _lastMousePosition;

        _scrollContainer.ScrollHorizontal -= (int)movement.X;
        _scrollContainer.ScrollVertical -= (int)movement.Y;

        _lastMousePosition = motion.Position;
    }

    public void SpendSkillPoints(int amount)
    {
        if (SkillPoints < amount) return;

        SkillPoints -= amount;
    }
}
using Godot;

public partial class Card : Control
{
	// Signals are used to communicate between nodes
	// We need this one because when a card is dragged, it needs to be reparented to the root node,
	// otherwise it will be restricted to the Hand's container.
	[Signal]
	public delegate void ReparentRequestedEventHandler(Card card);

	// Card properties
	public ColorRect ColorRect;
	public Label Label;
	public Area2D DropPointDetector;
	private CardStateMachine cardStateMachine;

	// This is called when the node enters the scene tree for the first time
	public override void _Ready()
	{
		// We can use GetNode<T> to get a reference to a child node
		ColorRect = GetNode<ColorRect>("Color");
		Label = GetNode<Label>("Label");
		DropPointDetector = GetNode<Area2D>("DropPointDetector");

		cardStateMachine = GetNode<CardStateMachine>("CardStateMachine");
		cardStateMachine.Init(this);

		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
	}

	public override void _Input(InputEvent e)
	{
		cardStateMachine.OnInput(e);
	}

	public override void _GuiInput(InputEvent e)
	{
		cardStateMachine.OnGuiInput(e);
	}

	public void OnMouseEntered()
	{
		cardStateMachine.OnMouseEnter();
	}

	public void OnMouseExited()
	{
		cardStateMachine.OnMouseExit();
	}
}

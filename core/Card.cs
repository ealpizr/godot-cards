using Godot;
using Godot.Collections;

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
	public Array<Node> Targets = new Array<Node>();
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

		DropPointDetector.AreaEntered += DropPointAreaEntered;
		DropPointDetector.AreaExited += DropPointAreaExited;
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

	public void DropPointAreaEntered(Area2D area)
	{
		if (!Targets.Contains(area))
		{
			Targets.Add(area);
		}
	}

	public void DropPointAreaExited(Area2D area)
	{
		Targets.Remove(area);
	}
}

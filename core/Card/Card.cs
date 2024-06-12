using Godot;
using System.Collections.Generic;

public partial class Card : Control
{
	// Signals are used to communicate between nodes
	// We need this one because when a card is dragged, it needs to be reparented to the root node,
	// otherwise it will be restricted to the Hand's container.
	[Signal]
	public delegate void ReparentRequestedEventHandler(Card card);

	// Card properties
	public ColorRect ColorRect { get; private set; }
	public Label Label { get; private set; }
	public Area2D DropPointDetector { get; private set; }
	public List<Node> Targets { get; private set; } = new List<Node>();
	private IStateMachine cardStateMachine;

	// New properties for the card
	public new string Name { get; set; }
	public string Description { get; set; }
	public Texture Icon { get; set; }
	public int EnergyCost { get; set; }
	public int AttackPoints { get; set; }
	public int DefensePoints { get; set; }
	public int HealthPoints { get; set; }
	public CardRarity Rarity { get; set; }

	// Constructor
	public Card()
	{
		// Initialize properties
		Name = "";
		Description = "";
		Icon = null;
		EnergyCost = 0;
		AttackPoints = 0;
		DefensePoints = 0;
		HealthPoints = 0;
		Rarity = CardRarity.Common; // Default value
	}

	// This is called when the node enters the scene tree for the first time
	public override void _Ready()
	{
		// Get references to child nodes
		ColorRect = GetNode<ColorRect>("Color");
		Label = GetNode<Label>("Label");
		DropPointDetector = GetNode<Area2D>("DropPointDetector");

		// Initialize state machine
		cardStateMachine = GetNode<CardStateMachine>("CardStateMachine");
		cardStateMachine.Init(this);

		// Connect mouse enter/exit signals
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;

		// Connect area detection signals
		DropPointDetector.AreaEntered += DropPointAreaEntered;
		DropPointDetector.AreaExited += DropPointAreaExited;
	}

	// Input handling methods
	public override void _Input(InputEvent e)
	{
		cardStateMachine.OnInput(e);
	}

	public override void _GuiInput(InputEvent e)
	{
		cardStateMachine.OnGuiInput(e);
	}

	// Mouse enter/exit event handlers
	private void OnMouseEntered()
	{
		cardStateMachine.OnMouseEnter();
	}

	private void OnMouseExited()
	{
		cardStateMachine.OnMouseExit();
	}

	// Area detection event handlers
	private void DropPointAreaEntered(Area2D area)
	{
		if (!Targets.Contains(area))
		{
			Targets.Add(area);
		}
	}

	private void DropPointAreaExited(Area2D area)
	{
		Targets.Remove(area);
	}
}

// Enum to define the rarity of the card
public enum CardRarity
{
	Common,
	Normal,
	Elite,
	Legendary
}


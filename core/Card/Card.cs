using Godot;
using System;
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

	// added a control to seperate the card shape from the card itself because the hboxcontainer always resets the position of the card and for some
	// reason the rotation! therefore it was better to handle the rotation inside the CardUI and leave the control CardUI for reparenting/state logic purposes.
	public Control CardShape { get; private set; }

	public bool isAttackPosition = true;

	// New properties for the card
	public new string Name { get; set; }
	public string Description { get; set; }
	public Texture Icon { get; set; }
	public int EnergyCost { get; set; }
	public int AttackPoints { get; set; }
	public int DefensePoints { get; set; }
	public int HealthPoints { get; set; }
	public CardRarity Rarity { get; set; }

	public bool IsSelected { get; set; }

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
		ColorRect = GetNode<ColorRect>("CardShape/Color");
		Label = GetNode<Label>("CardShape/Label");
		DropPointDetector = GetNode<Area2D>("DropPointDetector");
		CardShape = GetNode<Control>("CardShape");

		// Learning point: I was debugging why the card shape was on the way of the mouse event detection from the droppointdector.
		// this control had a handler for the mouse events, so it was handling first the mouse events and the other didn't get the chance to handle it.
		CardShape.MouseFilter = MouseFilterEnum.Pass;

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


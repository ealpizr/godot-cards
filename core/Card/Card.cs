using Godot;
using godotcards.core.Api;
using System;
using System.Collections.Generic;

public partial class Card : aCard
{
	// Signals are used to communicate between nodes
	// We need this one because when a card is dragged, it needs to be reparented to the root node,
	// otherwise it will be restricted to the Hand's container.

	// Card properties
	public ColorRect ColorRect { get; private set; }
	public Area2D DropPointDetector { get; private set; }
	public List<Node> Targets { get; private set; } = new List<Node>();
	public IStateMachine cardStateMachine;

	// added a control to seperate the card shape from the card itself because the hboxcontainer always resets the position of the card and for some
	// reason the rotation! therefore it was better to handle the rotation inside the CardUI and leave the control CardUI for reparenting/state logic purposes.
	public Control CardShape { get; private set; }

	// New properties for the card
	public new string Name { get; set; }

	public Card()
	{
		// Initialize properties
		Name = "";
        Description = "";
		Icon = GD.Load<Texture2D>("res://assets/card_characters/image_0.png");
		Name = "Card";
		Description = "Description";
		Icon = null;
		EnergyCost = 0;
		AttackPoints = 0;
		DefensePoints = 0;
		HealthPoints = 0;
		IsAttacking = false;
	}
	// Constructor
	public Card(String name, String description, int pEnergyCost, int pAttackPoints, int pDefensePoints, int pHealthPoints)
	{
		// Initialize properties
		Name = name;
		Description = description;
		Icon = null;
		EnergyCost = pEnergyCost;
		AttackPoints = pAttackPoints;
		DefensePoints = pDefensePoints;
		HealthPoints = pHealthPoints;
	}

	public override void _Ready()
	{
		// Get references to child nodes
		ColorRect = GetNode<ColorRect>("CardShape/Color");
        GetNode<TextureRect>("CardShape/Color/Image").Texture = Icon;
        GetNode<Label>("CardShape/Color/Name").Text = Name;
        GetNode<Label>("CardShape/Color/Attack").Text = $"⚔ {AttackPoints.ToString()}";
        GetNode<Label>("CardShape/Color/Health").Text = $"❤ {HealthPoints.ToString()}";
        GetNode<Label>("CardShape/Color/ManaCost").Text = EnergyCost.ToString();


        DropPointDetector = GetNode<Area2D>("DropPointDetector");
		CardShape = GetNode<Control>("CardShape");

		Rarity = CardRarity.Common;

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

	public override void OnCardEliminated(PlayerBase player)
	{
		player.Points -= eliminationPoints;
	}

    public override void Init(aCard c, godotcards.core.Api.Card card)
    {
		c.Name = card.Name;
		c.Description = card.Description;
		c.Icon = GD.Load<Texture2D>(card.Image);
		c.EnergyCost = card.ManaCost;
		c.AttackPoints = card.Attack;
		c.DefensePoints = 0;
		c.HealthPoints = card.Health;

		switch (card.Rarity)
		{
			case "Común":
				c.Rarity = CardRarity.Common;
				break;
			case "Rara":
				c.Rarity = CardRarity.Normal;
				break;
			case "Élite":
				c.Rarity = CardRarity.Elite;
				break;
			case "Legendaria":
				c.Rarity = CardRarity.Legendary;
				break;
		}
    }

}


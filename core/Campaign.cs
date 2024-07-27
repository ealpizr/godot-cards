using System;
using Godot;
using Godot.Collections;

public partial class Campaign : Node
{
	public IInteractable player;

	private IInteractable otherPlayerBase;
	CustomCPUPlayer otherPlayer;

	GameField gameField;

	Difficulty level;

	public int CurrentLevel = 1;

	public Campaign(Difficulty level, IInteractable player, Hand otherHand, HBoxContainer otherPlayingField, GameField gameField, Deck deck, Array<Card> cards)
	{
		this.level = level;
		this.player = player;
		this.otherPlayerBase = new CPUPlayer();
		this.otherPlayerBase.Init(otherPlayingField, otherHand, deck, cards);

		// if level is easy, then the other player is easy.
		this.otherPlayer = new DifficultyCPUPlayer(this.otherPlayerBase);
		this.otherPlayer.ChangeLevel(level);

		// configuration of the other player. Maybe there is a way to implement this more generically.
		this.gameField = gameField;
	}

	public override void _Ready()
	{
		// Nothing so far.
	}

	public override void _Process(double delta)
	{
		// Nothing so far.
	}

	public void AdvanceLevel()
	{
		this.CurrentLevel++;

		if (this.CurrentLevel == 2)
		{
			this.otherPlayer.ChangeLevel(Difficulty.Medium);
		}
		else
		{
		   // this.otherPlayer = new HardCPUPlayer();
		}

	}

	public void CPUPlayerPlay()
	{
		this.otherPlayer.SendInteraction(this.gameField, (PlayerBase)this.player);
	}


	public void PlayerPlay()
	{
		this.player.SendInteraction(gameField, (PlayerBase)this.otherPlayer);
	}

}

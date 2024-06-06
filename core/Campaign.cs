using System;
using Godot;

public partial class Campaign : Node
{
    public IInteractable player;
    IInteractable otherPlayer;

    GameField gameField;

    Difficulty level;



    public Campaign(Difficulty level, PlayerBase player, Hand otherHand, HBoxContainer otherPlayingField, GameField gameField)
    {
        this.level = level;
        this.player = (IInteractable)player;
        this.otherPlayer = new CPUPlayer();

        // if level is easy, then the other player is easy.
        this.otherPlayer = new EasyCPUPlayer(otherPlayer);

        // configuration of the other player. Maybe there is a way to 
        ((CPUPlayer)this.otherPlayer).Hand = otherHand;
        ((CPUPlayer)this.otherPlayer).PlayingFieldContainer = otherPlayingField;
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

    public void CPUPlayerPlay()
    {
        this.otherPlayer.SendInteraction((PlayerBase)this.player);
        this.gameField.EmitSignal(GameField.SignalName.ReparentToHboxContainer,((CPUPlayer)otherPlayer).PlayHand.Cards[0], ((CPUPlayer)otherPlayer).PlayingFieldContainer);
    }


    public void PlayerPlay()
    {
        this.player.SendInteraction((PlayerBase)this.otherPlayer);
    }

}
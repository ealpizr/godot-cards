using System;
using Godot;

public partial class Campaign : Node
{
    public IInteractable player;

    private IInteractable otherPlayerBase;
    IInteractable otherPlayer;

    GameField gameField;

    Difficulty level;

    int currentLevel = 1;



    public Campaign(Difficulty level, PlayerBase player, Hand otherHand, HBoxContainer otherPlayingField, GameField gameField)
    {
        this.level = level;
        this.player = (IInteractable)player;
        this.otherPlayerBase = new CPUPlayer();
        this.otherPlayerBase.Init(otherPlayingField, otherHand);

        // if level is easy, then the other player is easy.
        this.otherPlayer = new EasyCPUPlayer(this.otherPlayerBase);

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
        this.currentLevel++;

        if (this.currentLevel == 2)
        {
            this.otherPlayer = new EasyCPUPlayer(this.otherPlayerBase);
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
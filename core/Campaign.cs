using System;
using Godot;
using Godot.Collections;
using GodotCards.DesignPatterns.Observer;

public partial class Campaign
{
    CustomCPUPlayer cpuPlayer;
    GameField gameField;
    Difficulty level;
    public int CurrentLevel = 1;

    private Dictionary<int, Difficulty> difficultyMap = new Dictionary<int, Difficulty>
    {
        {1, Difficulty.Easy},
        {2, Difficulty.Medium},
        {3, Difficulty.Hard},
        {4, Difficulty.Hard}, // Should be sub-boss
        {5, Difficulty.Hard}, // Should be boss
    };

    public Campaign(int level, PlayerBase player, TurnManager turnManager)
    {
        cpuPlayer = new DifficultyCPUPlayer(player, turnManager);
        cpuPlayer.ChangeLevel(difficultyMap[level]);
    }

    public CustomCPUPlayer GetCPUPlayer()
    {
        return this.cpuPlayer;
    }

    public void AdvanceLevel()
    {
        this.CurrentLevel++;

        if (this.CurrentLevel == 2)
        {
            this.cpuPlayer.ChangeLevel(Difficulty.Medium);
        }
        else
        {
            // this.otherPlayer = new HardCPUPlayer();
        }

    }

    //public void CPUPlayerPlay()
    //{
    //    this.cpuPlayer.SendInteraction(this.gameField, (PlayerBase)this.player);
    //}


    //public void PlayerPlay()
    //{
    //    this.player.SendInteraction(gameField, (PlayerBase)this.cpuPlayer);
    //}

}

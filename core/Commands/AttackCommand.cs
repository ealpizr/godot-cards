using System;
using Godot;
using GodotCards.DesignPatterns.Command;

public class AttackCommand : ICommand
{
    public event EventHandler CanExecuteChanged;
    public PlayerBase _player { get; set; }
    public PlayerBase _opponent { get; set; }

    public AttackCommand(PlayerBase player, PlayerBase opponent) {
        this._player = player;
        this._opponent = opponent;
    }

    public bool CanExecute(object parameter)
    {
        throw new NotImplementedException();
    }

    public void Execute()
    {
        aCard playerCard = null;
		aCard opponentCard = null;

		for (int i = 0; i < _player.Hand.Cards.Count; i++) {
			if (_player.Hand.Cards[i].IsAttacking) {
				playerCard = _player.Hand.Cards[i];
			}
		}

		for (int i = 0; i < _opponent.Hand.Cards.Count; i++) {
			if (_opponent.Hand.Cards[i].IsAttacking) {
				opponentCard = _opponent.Hand.Cards[i];
			}
		}
		
		opponentCard.DefensePoints -= playerCard.AttackPoints;
        GD.Print("Attack success");

        if (opponentCard.DefensePoints <= 0) {
            playerCard.OnCardEliminated(_opponent);
        }

        GD.Print("Losed " + _opponent.Points + " points");
    }
}
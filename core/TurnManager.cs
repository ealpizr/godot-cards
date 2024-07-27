using Godot;
using System;

public partial class TurnManager : Node
{
	public bool IsPlayerTurn { get; set; } = true;

	public void EndPlayerTurn() {
		this.IsPlayerTurn = false;
	}

	public void StartPlayerTurn() {
		this.IsPlayerTurn = true;
	}
}

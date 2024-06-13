using Godot;
using System;

public partial class TurnManager : Node
{
	public bool IsPlayerTurn { get; set; } = true;

	public void EndTurn() {
		this.IsPlayerTurn = false;
	}
}

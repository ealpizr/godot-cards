public interface IPuntuacion
	{
		int PuntosCartasEliminadas { get; set; }
		int PuntosObjetivosEspeciales { get; set; }
		int PuntosExtras { get; set; }
		int TotalPuntos { get; }

		int GetCCoinsForWinner();
		int GetCCoinsForLoser();
		int GetCCoinsForObjectives();
	}

public class PuntuacionJugador : IPuntuacion
	{
		public int PuntosCartasEliminadas { get; set; }
		public int PuntosObjetivosEspeciales { get; set; }
		public int PuntosExtras { get; set; }

		public int TotalPuntos => PuntosCartasEliminadas + PuntosObjetivosEspeciales + PuntosExtras;

		public int GetCCoinsForWinner()
		{
			return 100;
		}

		public int GetCCoinsForLoser()
		{
			return 50;
		}

		public int GetCCoinsForObjectives()
		{
			return PuntosObjetivosEspeciales / 25 * 25;
		}
	}

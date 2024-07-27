using System.Linq;
using System.Collections.Generic;

public class PuntuacionJugadorFactory : PuntuacionFactory
	{
		public override IPuntuacion CrearPuntuacion(Player player)
		{
			PuntuacionJugador puntuacion = new PuntuacionJugador
			{
				PuntosCartasEliminadas = player.CartasEliminadas.Sum(c => c.Puntos),
				PuntosObjetivosEspeciales = player.ObjetivosEspecialesCompletados * 25,
				PuntosExtras = player.PuntosExtras
			};
			return puntuacion;
		}
	}

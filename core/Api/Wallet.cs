
using System.Text.Json.Serialization;

namespace godotcards.core.Api
{
	public class Wallet
	{
		[JsonPropertyName("c-coins")]
		public int Coins { get; set; }
	}
}

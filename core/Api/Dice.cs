using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace godotcards.core.Api
{
	public class Dice
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("image")]
		public int Cost { get; set; }

		[JsonPropertyName("rarity")]
		public string Rarity { get; set; }

		[JsonPropertyName("type")]
		public int Min { get; set; }

		[JsonPropertyName("manaCost")]
		public int Max { get; set; }
	}
}

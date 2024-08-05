using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace godotcards.core.Api
{
	public class Card
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("image")]
		public string Image { get; set; }

		[JsonPropertyName("attack")]
		public int Attack { get; set; }
		
		[JsonPropertyName("defense")]
		public int Defense { get; set; }

		[JsonPropertyName("health")]
		public int Health { get; set; }

		[JsonPropertyName("cost")]
		public int Cost { get; set; }

		[JsonPropertyName("rarity")]
		public string Rarity { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("manaCost")]
		public int ManaCost { get; set; }

	}
}

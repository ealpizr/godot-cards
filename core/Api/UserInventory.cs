using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace godotcards.core.Api
{
    public class UserInventory
    {
        [JsonPropertyName("cards")]
        public List<Card> Cards { get; set; }

        [JsonPropertyName("dice")]
        public List<Dice> Dice { get; set; }
    }
}

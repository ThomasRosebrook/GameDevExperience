using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameDevExperience
{
    public class Action
    {
        [JsonPropertyName("measure")]
        public int Measure { get; set; }

        [JsonPropertyName("beat")]
        public int Beat { get; set; }

        [JsonPropertyName("actionId")]
        public int ActionId { get; set; }
    }
}

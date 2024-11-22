using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameDevExperience
{
    public class Beatmap
    {
        [JsonPropertyName("bpm")]
        public int Bpm { get; set; }

        [JsonPropertyName("actions")]
        public List<Action> Actions { get; set; }
    }
}

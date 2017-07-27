using System.Collections.Generic;

namespace App
{
    internal class GameData
    {
        public decimal Version { get; set; }
        public Dictionary<int, Instance> Instances { get; set; }
        public Dictionary<int, Roulette> Roulettes { get; set; }
        public Dictionary<int, Area> Areas { get; set; }
    }
}

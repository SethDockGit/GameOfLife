using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Of_Life2
{
    public class Map
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string[] Seeds { get; set; }
        public bool[,] Cells { get; set; }
        public int Length = 50;
        public int Height = 50;
        public int ChanceToLive = 15; 
    }
}

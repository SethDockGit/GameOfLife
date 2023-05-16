using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Of_Life2.Data
{
    public class InMemory : IData
    {
        public List<Map> Maps { get; set; }
        public InMemory() 
        {
            Maps = new List<Map>
            {
                new Map()
                {
                    Name = "StillLife",
                    ID = "1",
                    Cells = StillLife(),
                },
                new Map()
                {
                    Name = "Gosper",
                    ID = "2",
                    Seeds = GetGosperGliderGun(),
                },
                new Map()
                {
                    Name = "Generic",
                    ID = "3",
                    Seeds = GetGeneric(),
                },
                new Map()
                {
                    Name = "Random",
                    ID = "4",
                    Cells = Random(Settings.Width, Settings.Height, Settings.PercentChanceToLive)
                }
            };
        }
        public Map GetMap(string id)
        {
            Map map = Maps.Single(m => m.ID == id);
            return map;
        }
        public List<Map> ReadMaps()
        {
            return Maps;
        }
        private bool[,] StillLife()
        {
            bool[,] cells = new bool[10, 10];

            cells[3, 7] = true;
            cells[3, 8] = true;
            cells[3, 9] = true;
            cells[6, 7] = true;
            cells[6, 8] = true;
            cells[6, 9] = true;

            return cells;
        }
        private string[] GetGosperGliderGun()
        {

            string[] gosper = new string[]
            {
                "........................O",
                "......................O.O",
                "............OO......OO............OO",
                "...........O...O....OO............OO",
                "OO........O.....O...OO",
                "OO........O...O.OO....O.O",
                "..........O.....O.......O",
                "...........O...O",
                "............OO"
            };


            return gosper;
        }
        private string[] GetGeneric()
        {
            string[] seeds =
            {
                "*******OOO",
                "*******OOO",
                "*******OOO",
                "*******OOO",
                "*******OOO",
            };

            return seeds;
        }
        private bool[,] Random(int length, int height, int percentChanceToLive)
        {
            Random rng = new Random();

            bool[,] cells = new bool[length, height];

            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    int randomInt = rng.Next(0, 100);

                    if (randomInt > percentChanceToLive - 1)
                    {
                        cells[x, y] = false;
                    }
                    else
                    {
                        cells[x, y] = true;
                    }
                }
            }
            return cells;
        }

    }
}

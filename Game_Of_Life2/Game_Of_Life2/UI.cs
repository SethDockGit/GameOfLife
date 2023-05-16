using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Game_Of_Life2.Data;

namespace Game_Of_Life2
{
    public class UI
    {

        private Manager Manager;
        private IData DataSource;

        public UI()
        {
            bool userExit = false;

            while(!userExit)
            {
                Menu();
            }
        }
        public void Menu()
        {
            GetDataSource();
            ListMaps(DataSource.Maps);
            Map map = SelectMap(DataSource.Maps);
            DisplayMap(map);
        }
        public void GetDataSource()
        {
            DataSource = new InMemory();
            Manager = new Manager(DataSource);
        }
        public void ListMaps(List<Map> maps)
        {
            Console.Clear();
            Console.WriteLine("        ***Maps***\n\n");

            maps = DataSource.ReadMaps();

            foreach (Map map in maps)
            {
                Console.WriteLine($"{map.ID}: {map.Name}");
            }
        }
        public Map SelectMap(List<Map> maps)
        {
            Console.WriteLine("\nPlease select the number of the map you'd like to run, then press enter...");

            string selection = Console.ReadLine();

            Map map = Manager.GetMap(selection);

            Console.Clear();

            return map;         
        }
        public void DisplayMap(Map map)
        {
            Initialize();
            RunMap(map.Cells);
        }
        public void Initialize()
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 100, Console.LargestWindowHeight - 3);
            Console.SetBufferSize(Console.LargestWindowWidth - 100, Console.LargestWindowHeight - 3);
            Console.Title = "Game of Life";
            Console.CursorVisible = false;
            Console.ForegroundColor = Settings.ForeGround;
            Console.BackgroundColor = Settings.BackGround;
        }
        public void RunMap(bool[,] cells)
        {

            var timer = new Stopwatch();

            while (true)
            {
                timer.Start();

                if (timer.ElapsedMilliseconds >= Settings.RunSpeed)
                {
                    PrintGrid(cells);

                    cells = GetNextState(cells);

                    timer.Reset();
                }
            }
        }
        public void PrintGrid(bool[,] cells)
        {

            StringBuilder sb = new StringBuilder();
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y <= cells.GetLength(1) + 1; y++)
            {
                sb.Append("--");
            }
            sb.Append("\n");

            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    if (cells[x, y] == true)
                    {
                        if (x == 0)
                        {
                            sb.Append($"| {Settings.LiveCellCharacter}");
                        }
                        else if (x == cells.GetLength(0) - 1)
                        {
                            sb.Append($"{Settings.LiveCellCharacter} |");
                        }
                        else
                        {
                            sb.Append($"{Settings.LiveCellCharacter}");
                        }
                    }
                    else
                    {
                        if (x == 0)
                        {
                            sb.Append($"| {Settings.DeadCellCharacter}");
                        }
                        else if (x == cells.GetLength(0) - 1)
                        {
                            sb.Append($"{Settings.DeadCellCharacter} |");
                        }
                        else
                        {
                            sb.Append($"{Settings.DeadCellCharacter}");
                        }
                    }
                }
                sb.Append("\n");
            }

            for (int y = 0; y <= cells.GetLength(1) + 1; y++)
            {
                sb.Append("--");
            }
            sb.Append("\n");

            Console.WriteLine(sb.ToString());

        }
        public bool[,] GetNextState(bool[,] previousState)
        {
            bool[,] currentState = (bool[,])previousState.Clone();

            for (int y = 0; y < previousState.GetLength(1); y++)
            {
                for (int x = 0; x < previousState.GetLength(0); x++)
                {
                    if (previousState[x, y] == true)
                    {
                        int neighbors = 0;

                        if (y > 0)
                        {
                            if (previousState[x, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y > 0 && x < previousState.GetLength(0) - 1)
                        {
                            if (previousState[x + 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) - 1)
                        {
                            if (previousState[x + 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) - 1 && y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x + 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x - 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0)
                        {
                            if (previousState[x - 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y > 0)
                        {
                            if (previousState[x - 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (neighbors < 2 || neighbors > 3)
                        {
                            currentState[x, y] = false;
                        }
                    }
                    if (previousState[x, y] == false)
                    {
                        int neighbors = 0;

                        if (y > 0)
                        {
                            if (previousState[x, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y > 0 && x < previousState.GetLength(0) - 1)
                        {
                            if (previousState[x + 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) - 1)
                        {
                            if (previousState[x + 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x < previousState.GetLength(0) - 1 && y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x + 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y < previousState.GetLength(1) - 1)
                        {
                            if (previousState[x - 1, y + 1])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0)
                        {
                            if (previousState[x - 1, y])
                            {
                                neighbors++;
                            }
                        }
                        if (x > 0 && y > 0)
                        {
                            if (previousState[x - 1, y - 1])
                            {
                                neighbors++;
                            }
                        }
                        if (neighbors == 3)
                        {
                            currentState[x, y] = true;
                        }
                    }
                }
            }
            return currentState;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cell
    {        
        public int X { get; }
        public int Y { get; }
        List<Cell> allNeighbours = new List<Cell>();
        public List<Cell> AliveNeighbours = new List<Cell>();
        public List<Cell> DeadNeighbours = new List<Cell>();

        public Cell()
        {
            allNeighbours = DefineNeighbours();
        }        

        public Cell(int xCoord, int yCoord)
        {            
            X = xCoord;
            Y = yCoord;

            allNeighbours = DefineNeighbours();
        }

        public void SetDeadNeighbourList()
        {            
            DeadNeighbours.Clear();
            List<int> cellsToAdd = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };

            int i = 0;
            foreach (Cell liveCell in AliveNeighbours)
            {
                foreach (var aCell in allNeighbours)
                {
                    if (liveCell.Compare(aCell))
                        cellsToAdd.RemoveAt(cellsToAdd.IndexOf(i));
                }
                i++;
            }

            for (int j = 0; j < cellsToAdd.Count; j++)
            {
                DeadNeighbours.Add(allNeighbours[j]);
            }


        }

        private List<Cell> DefineNeighbours()
        {
            Cell upperRight = new Cell(X + 1, Y + 1);
            Cell upperMid = new Cell(X, Y + 1);
            Cell upperLeft = new Cell(X - 1, Y + 1);

            Cell hRight = new Cell(X + 1, Y);
            Cell hLeft = new Cell(X - 1, Y);

            Cell lowerRight = new Cell(X + 1, Y - 1);
            Cell lowerMid = new Cell(X, Y - 1);
            Cell lowerLeft = new Cell(X - 1, Y - 1);

            List<Cell> allNeighbours = new List<Cell>() { upperRight, upperMid, upperLeft, hLeft, hRight, lowerLeft, lowerMid, lowerRight };
            return allNeighbours;
        }

        public bool Compare(Cell cell)
        {
            if (X == cell.X && Y == cell.Y) return true;
            else return false;
        }

        

    }
}

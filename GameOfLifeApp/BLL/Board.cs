using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Board
    {        
        
        public ObservableCollection<Cell> currentIterationCells = new ObservableCollection<Cell>();
        public ObservableCollection<Cell> nextIterationCells = new ObservableCollection<Cell>();

        public int IterationCount { get; }

        public List<Cell> CellsToBeRevived = new List<Cell>();
        public List<Cell> DeadAdjecantCells = new List<Cell>();

        public Board()
        {
            currentIterationCells.CollectionChanged += Cells_CollectionChanged;
        }
        
        private void Cells_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RecalculateAllCellLiveNeighbours();
        }

        private void RecalculateAllCellLiveNeighbours()
        {
            foreach (var recalculatedCell in currentIterationCells)
            {
                ObservableCollection<Cell> listOfExistingLiveNeigbors = new ObservableCollection<Cell>();
                
                for (int i = 0; i < currentIterationCells.Count; i++)
                {
                    bool isVerticalNeighbour = recalculatedCell.X == currentIterationCells[i].X && (recalculatedCell.Y == currentIterationCells[i].Y - 1 || recalculatedCell.Y == currentIterationCells[i].Y + 1);
                    bool isHorizontalNeighbour = recalculatedCell.Y == currentIterationCells[i].Y && (recalculatedCell.X == currentIterationCells[i].X - 1 || recalculatedCell.X == currentIterationCells[i].X + 1);
                    bool isRightDiagonalNeighbour = recalculatedCell.X + 1 == currentIterationCells[i].X && (recalculatedCell.Y == currentIterationCells[i].Y - 1 || recalculatedCell.Y == currentIterationCells[i].Y + 1);
                    bool isLeftDiagonalNeighbour = recalculatedCell.X - 1 == currentIterationCells[i].X && (recalculatedCell.Y == currentIterationCells[i].Y - 1 || recalculatedCell.Y == currentIterationCells[i].Y + 1);

                    if (isVerticalNeighbour || isHorizontalNeighbour || isRightDiagonalNeighbour || isLeftDiagonalNeighbour)
                    {
                        listOfExistingLiveNeigbors.Add(currentIterationCells[i]);
                    }
                }
                                
                recalculatedCell.AliveNeighbours = new List<Cell>(listOfExistingLiveNeigbors);
                recalculatedCell.SetDeadNeighbourList();
            }
        }
        
        public void ExecuteCurrentIteration()
        {
            nextIterationCells = new ObservableCollection<Cell>(currentIterationCells.ToArray());

            RemoveCellsFromNextIteration();

            CellsToBeRevived = FindCellsToRevive();
        }

        private List<Cell> FindCellsToRevive()
        {            
            List<Cell> cellToBeRevivedWithDuplicates = GetCellsToBeRevivedWithDuplicates();

            return GetDistinctCellsToRevive(cellToBeRevivedWithDuplicates);            
        }

        private static List<Cell> GetDistinctCellsToRevive(List<Cell> cellToBeRevivedWithDuplicates)
        {
            List<Cell> cellsToRevive = new List<Cell>();
            for (int i = 0; i < cellToBeRevivedWithDuplicates.Count; i++)
            {
                for (int j = 0; j < cellToBeRevivedWithDuplicates.Count; j++)
                {
                    if (cellToBeRevivedWithDuplicates[i].Compare(cellToBeRevivedWithDuplicates[j]))
                    {
                        cellsToRevive.Add(cellToBeRevivedWithDuplicates[i]);
                        cellToBeRevivedWithDuplicates.RemoveAll(x => x.X == cellsToRevive[cellsToRevive.Count - 1].X && x.Y == cellsToRevive[cellsToRevive.Count - 1].Y);
                    }
                }
            }

            return cellsToRevive;
        }

        private List<Cell> GetCellsToBeRevivedWithDuplicates()
        {
            List<Cell> cellToBeRevivedWithDuplicates = new List<Cell>();
            List<Cell> allDeadNeighbourCells = new List<Cell>();
            foreach (Cell cell in currentIterationCells)
            {
                allDeadNeighbourCells.AddRange(cell.DeadNeighbours.ToArray());
            }

            for (int i = 0; i < allDeadNeighbourCells.Count; i++)
            {
                int repeatCount = 0;

                for (int j = 0; j < allDeadNeighbourCells.Count; j++)
                {
                    if (allDeadNeighbourCells[i].Compare(allDeadNeighbourCells[j]))
                        repeatCount++;
                }

                if (repeatCount == 3)
                {
                    cellToBeRevivedWithDuplicates.Add(allDeadNeighbourCells[i]);
                }
            }

            return cellToBeRevivedWithDuplicates;
        }

        private void RemoveCellsFromNextIteration()
        {            
            RemoveCellsFromList(FindCellsToRemove());
        }

        private void RemoveCellsFromList(List<int> cellsToRemoveIndexes)
        {
            for (int i = cellsToRemoveIndexes.Count - 1; i >= 0; i--)
            {
                nextIterationCells.RemoveAt(cellsToRemoveIndexes[i]);
            }
        }

        private List<int> FindCellsToRemove()
        {
            List<int> cellsToRemove = new List<int>();

            for (int i = 0; i < currentIterationCells.Count; i++)
            {
                if (currentIterationCells[i].AliveNeighbours.Count < 2 || currentIterationCells[i].AliveNeighbours.Count > 3)
                    cellsToRemove.Add(i);
            }

            return cellsToRemove;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BLL;

namespace BLLTests
{
    [TestFixture]
    public class BoardTests
    {
        Board board;

        private void AddCellsToBoard(List<Cell> cells)
        {
            foreach (var cell in cells)
            {
                board.currentIterationCells.Add(cell);
            }            
        }

        [SetUp]
        public void SetupTest()
        {
            board = new Board();
        }

        [Test]
        public void CreateEmptyBoard()
        {
            Assert.That(board.currentIterationCells.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddADefaultCellToBoard()
        {
            Cell cell = new Cell();
            board.currentIterationCells.Add(cell);

            Assert.That(board.currentIterationCells.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetIterationsDefault()
        {
            Assert.That(board.IterationCount, Is.EqualTo(0));
        }

        [Test]//underpopulation
        public void KillCellsWithFewerThanTwoNeighbours()
        {
            Cell cell0 = new Cell(0, 0);
            Cell cell1 = new Cell(0, 1);
            Cell cell2 = new Cell(0, 2);

            AddCellsToBoard(new List<Cell>() { cell0, cell1, cell2 });

            board.ExecuteCurrentIteration();

            AssertThatCellsAreKilledDueToUnderpopulation(cell0, cell1, cell2);
        }

        private void AssertThatCellsAreKilledDueToUnderpopulation(Cell cell0, Cell cell1, Cell cell2)
        {
            Assert.True(!board.nextIterationCells.Contains(cell0));
            Assert.True(!board.nextIterationCells.Contains(cell2));

            Assert.True(board.nextIterationCells.Contains(cell1));
        }

        [Test] //overpopulation
        public void KillCellsWithMoreThan3Neighbours()
        {
            Cell cell0 = new Cell(0, 0);
            Cell cell1 = new Cell(0, 1); //more than 3 neighbours
            Cell cell2 = new Cell(0, 2);
            Cell cell3 = new Cell(1, 0);
            Cell cell4 = new Cell(1, 1); //more than 3 neighbours
            Cell cell5 = new Cell(1, 2);

            AddCellsToBoard(new List<Cell>() { cell0, cell1, cell2, cell3, cell4, cell5 });

            board.ExecuteCurrentIteration();

            AssertThatCellsAreKilledDueToOverpopulation(cell0, cell1, cell2, cell3, cell4, cell5);

        }

        private void AssertThatCellsAreKilledDueToOverpopulation(Cell cell0, Cell cell1, Cell cell2, Cell cell3, Cell cell4, Cell cell5)
        {
            Assert.True(!board.nextIterationCells.Contains(cell1));
            Assert.True(!board.nextIterationCells.Contains(cell4));

            Assert.True(board.nextIterationCells.Contains(cell0));
            Assert.True(board.nextIterationCells.Contains(cell2));
            Assert.True(board.nextIterationCells.Contains(cell3));
            Assert.True(board.nextIterationCells.Contains(cell5));
        }

        
        [Test]
        public void FindDeadCellsWithExactly3LiveNeighbours()
        {
            Cell cell0 = new Cell(0, 0);
            Cell cell1 = new Cell(0, 1); //more than 3 neighbours
            Cell cell2 = new Cell(0, 2);
            Cell cell3 = new Cell(1, 0);
            Cell cell4 = new Cell(1, 1); //more than 3 neighbours
            Cell cell5 = new Cell(1, 2);

            AddCellsToBoard(new List<Cell>() { cell0, cell1, cell2, cell3, cell4, cell5 });

            board.ExecuteCurrentIteration();

            Assert.That(board.CellsToBeRevived.Count, Is.EqualTo(2));

        }

        [Test]
        public void ReviveCellsWithExactly3Neighbours()
        {
            Cell cell0 = new Cell(0, 0);
            Cell cell1 = new Cell(0, 1); //more than 3 neighbours
            Cell cell2 = new Cell(0, 2);
            Cell cell3 = new Cell(1, 0);
            Cell cell4 = new Cell(1, 1); //more than 3 neighbours
            Cell cell5 = new Cell(1, 2);

            AddCellsToBoard(new List<Cell>() { cell0, cell1, cell2, cell3, cell4, cell5 });

            board.ExecuteCurrentIteration();

            Assert.That(board.nextIterationCells.Count, Is.EqualTo(2));
        }

    }
        
}

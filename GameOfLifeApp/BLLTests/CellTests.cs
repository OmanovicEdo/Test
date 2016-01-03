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
    public class CellTests
    {
        Cell cell;

        [SetUp]
        public void SetupMethods()
        {
            cell = new Cell();
        }

        [Test]
        public void DefaultCell_00_coordinates()
        {
            Assert.That(cell.X, Is.EqualTo(0));
            Assert.That(cell.Y, Is.EqualTo(0));
        }

        [Test]
        public void SetCellCooridinates()
        {
            cell = new Cell(5, 6);
            Assert.That(cell.X, Is.EqualTo(5));
            Assert.That(cell.Y, Is.EqualTo(6));
        }

        [Test]
        public void GetCellLiveNeighbourCount()
        {
            Board b = new Board();

            Cell cell0 = new Cell(0, 0);
            Cell cell1 = new Cell(0, 1);
            Cell cell2 = new Cell(0, 2);
            Cell cell3 = new Cell(1, 1);

            b.currentIterationCells.Add(cell0);
            b.currentIterationCells.Add(cell1);
            b.currentIterationCells.Add(cell2);
            b.currentIterationCells.Add(cell3);

            AssertThatLiveCellCountIsCorrect(cell0, cell1, cell2, cell3);
        }

        private static void AssertThatLiveCellCountIsCorrect(Cell cell0, Cell cell1, Cell cell2, Cell cell3)
        {
            Assert.That(cell0.AliveNeighbours.Count, Is.EqualTo(2));
            Assert.That(cell1.AliveNeighbours.Count, Is.EqualTo(3));
            Assert.That(cell2.AliveNeighbours.Count, Is.EqualTo(2));
            Assert.That(cell3.AliveNeighbours.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetCellDeadNeighbourCount()
        {
            Board b = new Board();

            Cell cell0 = new Cell(0, 0);
            Cell cell1 = new Cell(0, 1);
            Cell cell2 = new Cell(0, 2);
            Cell cell3 = new Cell(1, 1);

            b.currentIterationCells.Add(cell0);
            b.currentIterationCells.Add(cell1);
            b.currentIterationCells.Add(cell2);
            b.currentIterationCells.Add(cell3);
            
            AssertThatDeadCellCountIsCorrect(cell0, cell1, cell2, cell3);
        }

        private static void AssertThatDeadCellCountIsCorrect(Cell cell0, Cell cell1, Cell cell2, Cell cell3)
        {
            Assert.That(cell0.DeadNeighbours.Count, Is.EqualTo(6));
            Assert.That(cell1.DeadNeighbours.Count, Is.EqualTo(5));
            Assert.That(cell2.DeadNeighbours.Count, Is.EqualTo(6));
            Assert.That(cell3.DeadNeighbours.Count, Is.EqualTo(5));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Sharp_MazeGenerator
{
    public partial class MainForm : Form
    {
        public int numberOfCells = 10;
        public List<Cell> cells = new List<Cell>();
        public Timer t = new Timer();
        public Cell currentCell = null;
        public Cell randomNextCell;
        public int randomNumber;
        public List<Cell> cellTrail = new List<Cell>();
        public int cellTrailIndex;
        public List<int> dirs = new List<int> { 0, 1, 2, 3 };

        public int width = 500;
        public bool onUsedPath = false;
        public MainForm()
        {
            InitializeComponent();
            createCells();
            start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            this.Width = width;
            this.Height = width;
        }
        public void createCells()
        {
            for (int i = 0; i < numberOfCells; i++)
            {
                for (int b = 0; b < numberOfCells; b++)
                {
                    Cell c = new Cell(new Point(b * width / numberOfCells, i * width / numberOfCells), width / numberOfCells - 1, this);
                    cells.Add(c);
                }

            }
        }
        public void start()
        {
            t.Interval = 200;
            t.Start();
            t.Tick += new EventHandler(steps);

        }
        public void steps(object sender, EventArgs e)
        {
            if (!onUsedPath)
            {
                if (currentCell == null)
                {
                    currentCell = cells.ElementAt(34);
                    currentCell.setActive();
                }
                else
                {
                    cellTrail.Add(currentCell);
                    findNewCell(currentCell);

                }
            }
            else
            {
                currentCell = cellNeighbours(currentCell)[0];
                followTillEnd(currentCell);
            }

        }
        public void followTillEnd(Cell nextCell)
        {
            cellTrailIndex = cellTrail.IndexOf(nextCell);
            if (cellTrailIndex <= cellTrail.Count)
            {
                currentCell.setUsed();
                currentCell = cellTrail.ElementAt(cellTrailIndex + 1);
                currentCell.setActive();
                findNewCell(currentCell);

            }
            else
            {
                onUsedPath = false;
                findNewCell(currentCell);

            }
        }
        public void findNewCell(Cell currCell)
        {
            Cell[] neighbours = cellNeighbours(currCell);
            Random r = new Random();
            currentCell.setUsed();
            if (dirs.Count > 0)
            {
                randomNumber = dirs.ToArray()[r.Next(0, dirs.ToArray().Length)];
                randomNextCell = neighbours[randomNumber];
            }
            else if(dirs.Count == 0)
            {
                randomNextCell = neighbours[r.Next(0, 4)];
                onUsedPath = true;
                randomNextCell.setActive();
                currentCell = randomNextCell;
            }


            if (randomNextCell != null && randomNextCell.used == false && onUsedPath == false)
            {


                switch (randomNumber)
                {
                    case 0:
                        randomNextCell.p.Size = new Size(randomNextCell.p.Size.Width + 1, randomNextCell.p.Size.Height);
                        break;
                    case 1:
                        currentCell.p.Size = new Size(currentCell.p.Size.Width + 1, currentCell.p.Size.Height);
                        break;
                    case 2:
                        randomNextCell.p.Size = new Size(randomNextCell.p.Size.Width, randomNextCell.p.Size.Height + 1);
                        break;
                    case 3:
                        currentCell.p.Size = new Size(currentCell.p.Size.Width, currentCell.p.Size.Height + 1);
                        break;
                }
                randomNextCell.setActive();
                dirs.Clear();
                dirs = new List<int> { 0, 1, 2, 3 };
                currentCell = randomNextCell;
            }
            else
            {
                dirs.Remove(randomNumber);
                findNewCell(currCell);
            } 
            if (neighbours[0] == null && neighbours[1] == null && neighbours[2] == null && neighbours[3] == null)
            {
                randomNextCell = neighbours[r.Next(0, 4)];
                onUsedPath = true;
                randomNextCell.setActive();
                currentCell = randomNextCell;
            }
            

        }
        
        public Cell[] cellNeighbours(Cell c)
        {
            Cell[] neighbours = new Cell[4];
            int cellLeftIndex, cellRightIndex, cellUpIndex, cellDownIndex;
            cellLeftIndex = cells.IndexOf(c) - 1;
            cellRightIndex = cells.IndexOf(c) + 1;
            cellUpIndex = cells.IndexOf(c) - numberOfCells;
            cellDownIndex = cells.IndexOf(c) + numberOfCells;

            if (cellLeftIndex > 0 &&
                cells.ElementAt(cellLeftIndex).Location.Y == c.Location.Y &&
                cells.ElementAt(cellLeftIndex).Location.X >= 0 &&
                cells.ElementAt(cellLeftIndex).Location.X <= width)
            {
                neighbours[0] = cells.ElementAt(cellLeftIndex);
            }
            else { neighbours[0] = null; }
            if (cellRightIndex < cells.ToArray().Length &&
                cells.ElementAt(cellRightIndex).Location.Y == c.Location.Y &&
                cells.ElementAt(cellRightIndex).Location.X <= width)
            {
                neighbours[1] = cells.ElementAt(cellRightIndex);
            }
            else { neighbours[1] = null; }
            if (cells.IndexOf(c) - 10 >= 0 &&
                cells.ElementAt(cellUpIndex).Location.Y >= 0)
            {
                neighbours[2] = cells.ElementAt(cellUpIndex);
            }
            else { neighbours[2] = null; }
            if (cells.IndexOf(c) + 10 < cells.ToArray().Length &&
                cells.ElementAt(cellDownIndex).Location.Y <= width)
            {
                neighbours[3] = cells.ElementAt(cellDownIndex);
            }
            else { neighbours[3] = null; }
            return neighbours;
        }
    }
}

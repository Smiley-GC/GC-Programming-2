using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int row;
    public int col;
}

public struct CellValue
{
    public int row;
    public int col;
}

public class TileGrid : MonoBehaviour
{
    abstract class Command
    {
        public Command(Cell cell, int rows, int cols)
        {
            mCell = cell;
            mRows = rows;
            mCols = cols;
        }

        // Run is called externally so its public
        public abstract void Run();
        public abstract void Undo();

        // Nothing but Move uses CanMove, so CanMove should be private
        private bool CanMove(Cell cell)
        {
            return cell.col >= 0 && cell.col < mCols && cell.row >= 0 && cell.row < mRows;
        }
        
        // Move is used by derived classes so its protected
        protected void Move(Cell newCell)
        {
            // C# object addresses are passed by value, meaning we cannot reassign them in functions
            //cell = CanMove(newCell) ? newCell : cell;
            // Hence, we must manually change the row & col of our cell!
            if (CanMove(newCell))
            {
                mCell.row = newCell.row;
                mCell.col = newCell.col;
            }
        }

        private int mRows;
        private int mCols;
        protected Cell mCell;
    }

    class MoveCommand : Command
    {
        public MoveCommand(int dy, int dx, Cell cell, int rows, int cols) : base(cell, rows, cols)
        {
            mDy = dy;
            mDx = dx;
        }

        public override void Run()
        {
            // Since classes are references, this makes it so every time mCell changes, mPrevious changes.
            // Instead, we must make mPrevious a copy of mCell so we have revision history!
            //mPrevious = mCell;
            mPrevious = new Cell { col = mCell.col, row = mCell.row };
            Move(new Cell { col = mCell.col + mDx, row = mCell.row + mDy });
        }

        public override void Undo()
        {
            Move(mPrevious);
        }

        private Cell mPrevious;
        private int mDy;
        private int mDx;
    }

    public GameObject tilePrefab;
    List<List<GameObject>> tiles = new List<List<GameObject>>();

    int[,] tileTypes =
    {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    int[,] previous = new int[10, 20];

    List<Command> commands = new List<Command>();
    Cell player = new Cell { row = 4, col = 9 };

    Timer timer = new Timer();

    void Start()
    {
        // Value (struct) vs reference (class) refresher
        //Cell c0 = new Cell { row = 0, col = 0 };
        //Cell c1 = c0;
        //c0.row = 69;
        //c0.col = 420;
        //
        //CellValue cv0 = new CellValue {  row = 0, col = 0 };
        //CellValue cv1 = cv0;
        //cv0.row = 69;
        //cv0.col = 420;

        timer.total = 0.1f;

        int rows = tileTypes.GetLength(0);
        int cols = tileTypes.GetLength(1);
        float xStart = 0.5f;
        float yStart = 0.5f + (rows - 1);
        float x = xStart;
        float y = yStart;

        for (int row = 0; row < rows;  row++)
        {
            List<GameObject> columns = new List<GameObject>();
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x, y, 0.0f);
                columns.Add(tile);
                x += 1.0f;
            }
            tiles.Add(columns);
            x = xStart;
            y -= 1.0f;
        }
    }

    void Update()
    {
        if (timer.Expired())
        {
            timer.Reset();
            int rows = tileTypes.GetLength(0);
            int cols = tileTypes.GetLength(1);
            
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    previous[row, col] = tileTypes[row, col];
                }
            }

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Cell cell = new Cell { col = col, row = row };
                    int aliveCount = AliveNeighbours(cell);

                    // Die from under-polulation or over-population
                    if (aliveCount < 2 || aliveCount > 3)
                    {
                        tileTypes[cell.row, cell.col] = 0;
                    }

                    // Resurrect if exactly 3 alive neighbours (doesn't matter if we make a previously alive cell alive)
                    else if (aliveCount == 3)
                        tileTypes[cell.row, cell.col] = 1;

                    Color color = tileTypes[row, col] == 1 ? Color.white : Color.black;
                    tiles[row][col].GetComponent<SpriteRenderer>().color = color;
                }
            }
        }
        timer.Tick(Time.deltaTime);

        // Neighbours diagonals test
        //List<Cell> cells = Neighbours(new Cell { row = 4, col = 9 });
        //foreach (Cell cell in cells)
        //{
        //    tiles[cell.row][cell.col].GetComponent<SpriteRenderer>().color = Color.white;
        //}

        //Command command = null;
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    command = new MoveCommand(-1, 0, player, rows, cols);
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    command = new MoveCommand(1, 0, player, rows, cols);
        //}
        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    command = new MoveCommand(0, -1, player, rows, cols);
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    command = new MoveCommand(0, 1, player, rows, cols);
        //}
        //
        //if (command != null)
        //{
        //    command.Run();
        //    commands.Add(command);
        //}
        //
        //if (Input.GetKeyDown(KeyCode.Z) && commands.Count > 0)
        //{
        //    commands[commands.Count - 1].Undo();
        //    commands.RemoveAt(commands.Count - 1);
        //}
        //
        //ColorTile(player, Color.magenta);
    }

    void ColorTile(Cell cell, Color color)
    {
        tiles[cell.row][cell.col].GetComponent<SpriteRenderer>().color = color;
    }

    void ColorTile(Cell cell)
    {
        Color color = tileTypes[cell.row, cell.col] == 1 ? Color.green : Color.red;
        ColorTile(cell, color);
    }

    void Clairvoyance(Cell cell)
    {
        foreach (Cell neighbour in Neighbours(cell))
            ColorTile(neighbour);
        ColorTile(cell, Color.magenta);
    }

    int AliveNeighbours(Cell cell)
    {
        List<Cell> neighbours = Neighbours(cell);
        int aliveCount = 0;
        foreach (Cell neighbour in neighbours)
        {
            if (previous[neighbour.row, neighbour.col] == 1)
                aliveCount++;
        }
        return aliveCount;
    }

    // Returns left-right-up-down cell of the passed in cell
    List<Cell> Neighbours(Cell cell)
    {
        int rows = tileTypes.GetLength(0);
        int cols = tileTypes.GetLength(1);
        int left = cell.col - 1;
        int right = cell.col + 1;
        int up = cell.row - 1;
        int down = cell.row + 1;

        List<Cell> neighbours = new List<Cell>();

        if (left >= 0)
            neighbours.Add(new Cell { row = cell.row, col = left });

        if (right < cols)
            neighbours.Add(new Cell { row = cell.row, col = right });

        if (up >= 0)
            neighbours.Add(new Cell { row = up, col = cell.col });

        if (down < rows)
            neighbours.Add(new Cell { row = down, col = cell.col });

        if (left >= 0 && up >= 0)
            neighbours.Add(new Cell { row = up, col = left });

        if (right < cols && up >= 0)
            neighbours.Add(new Cell { row = up, col = right });

        if (left >= 0 && down < rows)
            neighbours.Add(new Cell { row = down, col = left });

        if (right < cols && down < rows)
            neighbours.Add(new Cell { row = down, col = right });

        return neighbours;
    }
}

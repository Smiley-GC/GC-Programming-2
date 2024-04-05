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
    // This should be kept exclusively as an interface
    abstract class Command
    {
        public abstract void Run();
        public abstract void Undo();
    }

    class MoveCommand : Command
    {
        public MoveCommand(int dy, int dx, Cell cell, int rows, int cols) : base()
        {
            mDy = dy;
            mDx = dx;
            mCell = cell;
            mRows = rows;
            mCols = cols;
        }

        public override void Run()
        {
            Cell newCell = new Cell { row = mCell.row + mDy, col = mCell.col + mDx };
            Move(newCell);
        }

        public override void Undo()
        {
            Cell previous = new Cell { row = mCell.row - mDy, col = mCell.col - mDx };
            Move(previous);
        }

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

        private int mDy;
        private int mDx;
        private Cell mCell;
        private int mRows;
        private int mCols;
    }

    public GameObject tilePrefab;
    List<List<GameObject>> tiles = new List<List<GameObject>>();

    int[,] tileTypes =
    {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    List<Command> commands = new List<Command>();
    Cell player = new Cell { row = 4, col = 9 };

    void Start()
    {
        // Cells are classes which are references,
        // so when we assign one to another, they become the same object!
        Cell c0 = new Cell { row = 69, col = 420 };
        Cell c1 = c0;
        // Both cell rows are 42
        c0.row = 42;

        // CellValues are structs, so they get coppied when assigned.
        CellValue cv0 = new CellValue { row = 69, col = 420 };
        CellValue cv1 = cv0;
        // Only cv0's row is 42
        cv0.row = 42;

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

        // Set the initial pattern
        foreach (Cell cell in Neighbours(new Cell { row = 4, col = 9 }))
        {
            tileTypes[cell.row, cell.col] = 1;
        }
    }

    void Update()
    {
        // Revert colour of every tile to white
        int rows = tileTypes.GetLength(0);
        int cols = tileTypes.GetLength(1);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Cell cell = new Cell { row = row, col = col };
                int aliveCount = AliveNeighbours(cell);

                // Dies due to under-population if less than 2, dies due to over-population if more than 3
                tileTypes[cell.row, cell.col] = aliveCount == 2 || aliveCount == 3 ? 1 : 0;

                // Resurrects if dead and has exactly 3 alive neighbours!
                if (tileTypes[cell.row, cell.col] == 0 && aliveCount == 3)
                    tileTypes[cell.row, cell.col] = 1;

                Color color = tileTypes[row, col] == 1 ? Color.white : Color.black;
                tiles[row][col].GetComponent<SpriteRenderer>().color = color;
            }
        }

        /*
        Command command = null;
        if (Input.GetKeyDown(KeyCode.W))
        {
            command = new MoveCommand(-1, 0, player, rows, cols);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            command = new MoveCommand(1, 0, player, rows, cols);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            command = new MoveCommand(0, -1, player, rows, cols);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            command = new MoveCommand(0, 1, player, rows, cols);
        }

        if (command != null)
        {
            command.Run();
            commands.Add(command);
        }

        if (Input.GetKeyDown(KeyCode.Z) && commands.Count > 0)
        {
            commands[commands.Count - 1].Undo();
            commands.RemoveAt(commands.Count - 1);
        }

        ColorTile(player, Color.magenta);*/

        // Uncomment for lab 4 solution
        // Mouse --> screen space --> world space --> grid space
        //Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //int mouseRow = (tileTypes.GetLength(0) - 1) - (int)mouse.y;
        //int mouseCol = (int)mouse.x;
        //mouseRow = Mathf.Clamp(mouseRow, 0, rows - 1);
        //mouseCol = Mathf.Clamp(mouseCol, 0, cols - 1);
        //Clairvoyance(new Cell { row = mouseRow, col = mouseCol });
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
        int aliveCount = 0;
        foreach (Cell neighbour in Neighbours(cell))
        {
            if (tileTypes[neighbour.row, neighbour.col] == 1)
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

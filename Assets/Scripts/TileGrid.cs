using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int row;
    public int col;
}

public class TileGrid : MonoBehaviour
{
    public abstract class Command
    {
        public abstract void Run();
        public abstract void Undo();

        // *Can change row & col, but CANNOT change address (aka cannot assign to new Cell)*
        public Cell cell;
        public int rows;
        public int cols;

        protected void Move(Cell dst)
        {
            if (CanMove(dst, rows, cols))
            {
                cell.row = dst.row;
                cell.col = dst.col;
            }
        }
    }

    public class LeftCommand : Command
    {
        public override void Run()
        {
            Cell newCell = new Cell { row = cell.row, col = cell.col - 1 };
            Move(newCell);
        }

        public override void Undo()
        {
            Cell newCell = new Cell { row = cell.row, col = cell.col + 1 };
            Move(newCell);
        }
    }

    public class RightCommand : Command
    {
        public override void Run()
        {
            Cell newCell = new Cell { row = cell.row, col = cell.col + 1 };
            Move(newCell);
        }

        public override void Undo()
        {
            Cell newCell = new Cell { row = cell.row, col = cell.col - 1 };
            Move(newCell);
        }
    }

    public class UpCommand : Command
    {
        public override void Run()
        {
            Cell newCell = new Cell { row = cell.row - 1, col = cell.col };
            Move(newCell);
        }

        public override void Undo()
        {
            Cell newCell = new Cell { row = cell.row + 1, col = cell.col };
            Move(newCell);
        }
    }

    public class DownCommand : Command
    {
        public override void Run()
        {
            Cell newCell = new Cell { row = cell.row + 1, col = cell.col };
            Move(newCell);
        }

        public override void Undo()
        {
            Cell newCell = new Cell { row = cell.row - 1, col = cell.col };
            Move(newCell);
        }
    }

    public GameObject tilePrefab;
    List<List<GameObject>> tiles = new List<List<GameObject>>();

    int[,] types =
    {
        { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    Cell player = new Cell { row = 4, col = 9 };
    List<Command> commands = new List<Command>();

    void Start()
    {
        int rows = types.GetLength(0);
        int cols = types.GetLength(1);
        float x = 0.5f;
        float y = 0.5f + rows - 1;
        for (int row = 0; row < rows; row++)
        {
            List<GameObject> columnTiles = new List<GameObject>();
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x, y);
                columnTiles.Add(tile);

                x += 1.0f;
            }
            tiles.Add(columnTiles);
            x = 0.5f;
            y -= 1.0f;
        }
    }

    void Update()
    {
        int rows = types.GetLength(0);
        int cols = types.GetLength(1);

        // Revert tile colours to white every frame
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                tiles[row][col].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        Command command = null;
        if (Input.GetKeyDown(KeyCode.W))
        {
            command = new UpCommand();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            command = new DownCommand();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            command = new LeftCommand();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            command = new RightCommand();
        }

        // Assign data & run command if a key has been pressed (meaning the command will be non-null)
        if (command != null)
        {
            command.cell = player;
            command.rows = rows;
            command.cols = cols;
            command.Run();
            commands.Add(command);
        }

        // If we've pressed the undo button and there's an undo history, perform the undo
        if (Input.GetKeyDown(KeyCode.Z) && commands.Count > 0)
        {
            commands[commands.Count - 1].Undo();
            commands.RemoveAt(commands.Count - 1);
        }

        ColorTile(player, Color.red);

        // Uncomment for lab 4 solution:
        //Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouse = new Vector2(mouse.x, types.GetLength(0) - mouse.y);
        //Cell mouseCell = new Cell { row = (int)mouse.y, col = (int)mouse.x };
        //mouseCell.row = Mathf.Clamp(mouseCell.row, 0, rows - 1);
        //mouseCell.col = Mathf.Clamp(mouseCell.col, 0, cols - 1);
        //Clairvoyance(mouseCell, rows, cols);
        //ColorTile(mouseCell, Color.magenta);
    }

    // Colors a tile prefab based on the types array (1:1 map between "types" and "tiles" arrays)
    void ColorTile(Cell cell)
    {
        ColorTile(cell, types[cell.row, cell.col] == 1 ? Color.green : Color.red);
    }

    // Colors a tile prefab the passed in color
    void ColorTile(Cell cell, Color color)
    {
        tiles[cell.row][cell.col].GetComponent<SpriteRenderer>().color = color;
    }

    // Colors adjacent tiles green if they're walkable or red if they're unwalkable
    void Clairvoyance(Cell current, int rows, int cols)
    {
        foreach (Cell neighbour in Neighbours(current, rows, cols))
            ColorTile(neighbour);
        ColorTile(current);
    }

    // Returns whether the cell is within the bounds of the grid
    public static bool CanMove(Cell cell, int rows, int cols)
    {
        return cell.row >= 0 && cell.col >= 0 && cell.row < rows && cell.col < cols;
    }

    // Returns grid-coordinates (Cell) of adjacent tiles if they're within the grid's bounds
    public static List<Cell> Neighbours(Cell cell, int rows, int cols)
    {
        List<Cell> neighbours = new List<Cell>();

        Cell left = new Cell { row = cell.row, col = cell.col - 1 };
        Cell right = new Cell { row = cell.row, col = cell.col + 1 };
        Cell up = new Cell { row = cell.row - 1, col = cell.col };
        Cell down = new Cell { row = cell.row + 1, col = cell.col };

        if (CanMove(cell, rows, cols)) neighbours.Add(left);
        if (CanMove(right, rows, cols)) neighbours.Add(right);
        if (CanMove(up, rows, cols)) neighbours.Add(up);
        if (CanMove(down, rows, cols)) neighbours.Add(down);

        return neighbours;
    }
}

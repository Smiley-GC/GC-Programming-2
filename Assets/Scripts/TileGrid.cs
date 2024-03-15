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
    abstract class Command
    {
        // Run is called externally so its public
        public abstract void Run();

        // Nothing but Move uses CanMove, so CanMove should be private
        private bool CanMove(Cell cell)
        {
            return cell.col >= 0 && cell.col < cols && cell.row >= 0 && cell.row < rows;
        }
        
        // Move is used by derived classes so its protected
        protected void Move(Cell newCell)
        {
            // C# object addresses are passed by value, meaning we cannot reassign them in functions
            //cell = CanMove(newCell) ? newCell : cell;
            // Hence, we must manually change the row & col of our cell!
            if (CanMove(newCell))
            {
                cell.row = newCell.row;
                cell.col = newCell.col;
            }
        }

        public int rows;
        public int cols;
        public Cell cell;
    }

    class LeftCommand : Command
    {
        public override void Run()
        {
            Cell newCell = new Cell { row = cell.row, col = cell.col - 1 };
            Move(newCell);
        }
    }

    class RightCommand : Command
    {
        public override void Run()
        {
            Cell newCell = new Cell { row = cell.row, col = cell.col + 1 };
            Move(newCell);
        }
    }

    class UpCommand : Command
    {
        public override void Run()
        {
            Cell newCell = new Cell { row = cell.row - 1, col = cell.col };
            Move(newCell);
        }
    }

    class DownCommand : Command
    {
        public override void Run()
        {
            Cell newCell = new Cell { row = cell.row + 1, col = cell.col };
            Move(newCell);
        }
    }

    public GameObject tilePrefab;
    List<List<GameObject>> tiles = new List<List<GameObject>>();

    int[,] tileTypes =
    {
        { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    Cell player = new Cell { row = 4, col = 9 };

    void Start()
    {
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
        // Revert colour of every tile to white
        int rows = tileTypes.GetLength(0);
        int cols = tileTypes.GetLength(1);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                tiles[row][col].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        //int dx = 0;
        //int dy = 0;
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    dy = -1;
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    dy = 1;
        //}
        //else if (Input.GetKeyDown(KeyCode.A))
        //{
        //    dx = -1;
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    dx = 1;
        //}
        //Cell newPlayer = new Cell { row = player.row + dy, col = player.col + dx };
        //player = CanMove(newPlayer) ? newPlayer : player;

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

        if (command != null)
        {
            command.rows = rows;
            command.cols = cols;
            command.cell = player;
            command.Run();
        }

        ColorTile(player, Color.magenta);

        // Uncomment for lab 4 solution
        // Mouse --> screen space --> world space --> grid space
        //Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //int mouseRow = (tileTypes.GetLength(0) - 1) - (int)mouse.y;
        //int mouseCol = (int)mouse.x;
        //mouseRow = Mathf.Clamp(mouseRow, 0, rows - 1);
        //mouseCol = Mathf.Clamp(mouseCol, 0, cols - 1);
        //Clairvoyance(new Cell { row = mouseRow, col = mouseCol });
    }

    bool CanMove(Cell cell)
    {
        int rows = tileTypes.GetLength(0);
        int cols = tileTypes.GetLength(1);
        return cell.col >= 0 && cell.col < cols && cell.row >= 0 && cell.row < rows;
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

        return neighbours;
    }
}

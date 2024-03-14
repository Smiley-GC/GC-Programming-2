using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public struct Cell
{
    public int row;
    public int col;
}

public class TileGrid : MonoBehaviour
{
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

        // TODO -- color using Clairvoyance instead
        //for (int row = 0; row < rows; row++)
        //{
        //    for (int col = 0; col < cols; col++)
        //    {
        //        Color color = types[row, col] == 1 ? Color.red : Color.white;
        //        tiles[row][col].GetComponent<SpriteRenderer>().color = color;
        //    }
        //}
    }

    void Update()
    {
        int rows = types.GetLength(0);
        int cols = types.GetLength(1);

        // Revert colours to white every frame
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                tiles[row][col].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse = new Vector2(mouse.x, types.GetLength(0) - mouse.y);
        Cell mouseCell = new Cell { row = (int)mouse.y, col = (int)mouse.x };
        if (CanMove(mouseCell))
            ColorTile(mouseCell, Color.magenta);

        int dy = 0, dx = 0;
        if (Input.GetKeyDown(KeyCode.W))
        {
            dy = -1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dy = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dx = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dx = 1;
        }
         
        Cell newPlayer = new Cell { row = player.row + dy, col = player.col + dx };
        player = CanMove(newPlayer) ? newPlayer : player;
        ColorTile(player, Color.red);

        //mouseCell.row = Mathf.Clamp(mouseCell.row, 0, rows - 1);
        //mouseCell.col = Mathf.Clamp(mouseCell.col, 0, cols - 1);
        //Clairvoyance(mouseCell);
    }

    // Colors a tile prefab based on the types array (1:1 map between "types" and "tiles" arrays)
    void ColorTile(Cell cell)
    {
        tiles[cell.row][cell.col].GetComponent<SpriteRenderer>().color =
        types[cell.row, cell.col] == 1 ? Color.green : Color.red;
    }

    void ColorTile(Cell cell, Color color)
    {
        tiles[cell.row][cell.col].GetComponent<SpriteRenderer>().color = color;
    }

    void Clairvoyance(Cell current)
    {
        // Bounded automatic implementation:
        foreach (Cell neighbour in Neighbours(current))
            ColorTile(neighbour);
        ColorTile(current);
    }

    bool CanMove(Cell cell)
    {
        int rows = types.GetLength(0);
        int cols = types.GetLength(1);
        return cell.row >= 0 && cell.col >= 0 && cell.row < rows && cell.col < cols;
    }

    // Returns grid-coordinates (Cell) of adjacent tiles if they're on the grid 
    List<Cell> Neighbours(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();
        int rows = types.GetLength(0);
        int cols = types.GetLength(1);

        int left = cell.col - 1;
        int right = cell.col + 1;
        int up = cell.row - 1;
        int down = cell.row + 1;

        if (left >= 0) neighbours.Add(new Cell { row = cell.row, col = left });
        if (right < cols) neighbours.Add(new Cell { row = cell.row, col = right });
        if (up >= 0) neighbours.Add(new Cell { row = up, col = cell.col });
        if (down < rows) neighbours.Add(new Cell { row = down, col = cell.col });

        return neighbours;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
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

        // Mouse --> screen space --> world space --> grid space
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int mouseRow = (tileTypes.GetLength(0) - 1) - (int)mouse.y;
        int mouseCol = (int)mouse.x;
        mouseRow = Mathf.Clamp(mouseRow, 0, rows - 1);
        mouseCol = Mathf.Clamp(mouseCol, 0, cols - 1);
        //Debug.Log(mouse);

        // Render adjacent tiles red if lava, green if stone
        Clairvoyance(mouseRow, mouseCol);
    }

    void ColorTile(int row, int col, Color color)
    {
        tiles[row][col].GetComponent<SpriteRenderer>().color = color;
    }

    void ColorTile(int row, int col)
    {
        Color color = tileTypes[row, col] == 1 ? Color.green : Color.red;
        ColorTile(row, col, color);
    }

    // Tip: start by making a blue +, then colour tiles red/green based on condition, finally fix index errors
    void Clairvoyance(int row, int col)
    {
        // Cursor column (don't change this)
        ColorTile(row, col, Color.magenta);

        int rows = tileTypes.GetLength(0);
        int cols = tileTypes.GetLength(1);

        int left = col - 1;
        int right = col + 1;
        int up = row - 1;
        int down = row + 1;

        if (left >= 0)
            ColorTile(row, left);

        if (right < cols)
            ColorTile(row, right);

        if (up >= 0)
            ColorTile(up, col);

        if (down < rows)
            ColorTile(down, col);
    }
}

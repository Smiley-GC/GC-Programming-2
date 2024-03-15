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

                // Color based on tile type!
                Color color = tileTypes[row, col] == 1 ? Color.red : Color.white;
                tile.GetComponent<SpriteRenderer>().color = color;
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
        //Debug.Log(mouse);

        // Render adjacent tiles red if lava, green if stone
        Clairvoyance(mouseRow, mouseCol);
    }

    // Tip: start by making a blue +, then colour tiles red/green based on condition, finally fix index errors
    void Clairvoyance(int row, int col)
    {
        // Cursor column (don't change this)
        tiles[row][col].GetComponent<SpriteRenderer>().color = Color.magenta;

        tiles[row][col - 1].GetComponent<SpriteRenderer>().color = Color.blue;
        tiles[row][col + 1].GetComponent<SpriteRenderer>().color = Color.blue;
        tiles[row - 1][col].GetComponent<SpriteRenderer>().color = Color.blue;
        tiles[row + 1][col].GetComponent<SpriteRenderer>().color = Color.blue;
    }
}

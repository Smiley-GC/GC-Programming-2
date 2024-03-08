using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public GameObject tilePrefab;
    List<List<GameObject>> tiles = new List<List<GameObject>>();

    void Start()
    {
        float xStart = 0.5f;
        float yStart = 0.5f;
        float x = xStart;
        float y = yStart;

        for (int row = 0; row < 10;  row++)
        {
            List<GameObject> columns = new List<GameObject>();
            for (int col = 0; col < 20; col++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(x, y, 0.0f);
                columns.Add(tile);
                x += 1.0f;
            }
            tiles.Add(columns);
            x = xStart;
            y += 1.0f;
        }
    }

    void Update()
    {
        tiles[0][0].GetComponent<SpriteRenderer>().color = Color.red;
        tiles[9][19].GetComponent<SpriteRenderer>().color = Color.green;
    }
}

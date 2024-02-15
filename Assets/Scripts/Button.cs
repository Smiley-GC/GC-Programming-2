using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    bool PointInRec(Vector2 point, Vector2 position, float width, float height)
    {
        float xMin = position.x - width * 0.5f;
        float xMax = position.x + width * 0.5f;
        float yMin = position.y - height * 0.5f;
        float yMax = position.y + height * 0.5f;

        if (point.x < xMin) return false;
        if (point.x > xMax) return false;
        if (point.y < yMin) return false;
        if (point.y > yMax) return false;

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool collision = PointInRec(mouse, transform.position, 1.0f, 1.0f);
        Color color = collision ? Color.red : Color.green;
        GetComponent<SpriteRenderer>().color = color;
    }
}

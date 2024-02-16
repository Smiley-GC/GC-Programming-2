using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    bool PointInRec(Vector2 point, Vector2 rec, float width, float height)
    {
        float xMin = rec.x - width * 0.5f;
        float xMax = rec.x + width * 0.5f;
        float yMin = rec.y - height * 0.5f;
        float yMax = rec.y + height * 0.5f;

        if (point.x < xMin) return false;
        if (point.x > xMax) return false;
        if (point.y < yMin) return false;
        if (point.y > yMax) return false;

        return true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool collision = PointInRec(mouse, transform.position, 1.0f, 1.0f);
        GetComponent<SpriteRenderer>().color = collision ? Color.red : Color.green;
    }
}

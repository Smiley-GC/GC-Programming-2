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

    //delegate void MouseHandler();
    bool collision = false;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool collisionThisFrame = PointInRec(mouse, transform.position, 1.0f, 1.0f);
        GetComponent<SpriteRenderer>().color = collision ? Color.red : Color.green;

        // If we were previously outside the button, and now we're inside the button, dispatch the mouse-in event!
        if (!collision && collisionThisFrame)
        {
            OnMouseIn();
        }

        // If we were previously inside the button, and we're outside the button, dispatch the mouse-out event!
        if (collision && !collisionThisFrame)
        {
            OnMouseOut();
        }

        // Overlapping if we're currently inside the button, dispatch the mouse-out event!
        if (collisionThisFrame)
        {
            OnMouseOverlap();
        }

        // If we're currently inside the button and we've left-clicked, dispatch the mouse-click event!
        if (collisionThisFrame && Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }

        collision = collisionThisFrame;
    }

    void OnMouseIn()
    {
        Debug.Log("Mouse-in");
    }

    void OnMouseOut()
    {
        Debug.Log("Mouse-out");
    }

    void OnMouseOverlap()
    {
        //Debug.Log("Mouse-over");
    }

    void OnMouseClick()
    {
        Debug.Log("Mouse-click");
    }
}

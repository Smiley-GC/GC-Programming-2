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

    delegate void MouseHandler();
    

    // Start is called before the first frame update
    void Start()
    {
        onMouseIn = OnMouseIn;
        onMouseOut = OnMouseOut;
        onMouseClick = OnMouseClick;

        // If we want to avoid getting spam-logged when our cursor is over the button,
        // simply comment-out this handler so nothing is dispatched on-mouse-over!
        //onMouseOverlap = OnMouseOverlap;
    }

    bool collision = false;
    MouseHandler onMouseIn = null;
    MouseHandler onMouseOut = null;
    MouseHandler onMouseOverlap = null;
    MouseHandler onMouseClick = null;

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool collisionThisFrame = PointInRec(mouse, transform.position, 1.0f, 1.0f);

        // Handle mouse-in event
        if (!collision && collisionThisFrame && onMouseIn != null)
        {
            onMouseIn();
        }

        // Handle mouse-out event
        if (collision && !collisionThisFrame && onMouseOut != null)
        {
            onMouseOut();
        }

        // Handle mouse-over event
        if (collisionThisFrame && onMouseOverlap != null)
        {
            onMouseOverlap();
        }

        // Handle mouse-click event
        if (Input.GetMouseButtonDown(0) && collisionThisFrame && onMouseClick != null)
        {
            onMouseClick();
        }

        Color color = collision ? Color.red : Color.green;
        GetComponent<SpriteRenderer>().color = color;

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
        Debug.Log("Mouse-over");
    }

    void OnMouseClick()
    {
        Debug.Log("Mouse-click");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Change state based on a timer, colour based on state.
// Actions --> coloring
// Conditions --> timeout
public class Enemy : MonoBehaviour
{
    public enum State
    {
        NEUTRAL,
        OFFENSIVE,
        DEFENSIVE
    }

    public State state = State.NEUTRAL;
    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Color color = Color.white;
        switch (state)
        {
            case State.NEUTRAL:
                color = Color.grey;
                break;

            case State.OFFENSIVE:
                color = Color.red;
                break;

            case State.DEFENSIVE:
                color = Color.blue;
                break;
        }
        renderer.color = color;
    }
}

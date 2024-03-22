using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;

    public enum State
    {
        NEUTRAL,
        OFFENSIVE,
        DEFENSIVE
    }

    State state = State.NEUTRAL;

    void Transition(State newState)
    {
        OnExit(state);
        state = newState;
        OnEnter(state);
    }

    void OnEnter(State newState)
    {
        Color color = Color.white;
        switch (newState)
        {
            case State.NEUTRAL:
                color = Color.gray;
                break;
        
            case State.OFFENSIVE:
                color = Color.red;
                break;
        
            case State.DEFENSIVE:
                color = Color.blue;
                break;
        }
        GetComponent<SpriteRenderer>().color = color;

        // Alternative: if its only a data change (no state-specific behaviour),
        // we can map our state to an array-index to save on code!
        //Color[] colors = { Color.gray, Color.red, Color.blue };
        //GetComponent<SpriteRenderer>().color = colors[(int)newState];
    }

    void OnExit(State previousState)
    {

    }

    // OnEnter & OnUpdate happen once, so we pass our newState
    // in case we need to handle some state-specific transitions
    void OnUpdate()
    {

    }

    void Start()
    {
        Transition(State.NEUTRAL);
    }

    void Update()
    {
        OnUpdate();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Transition(State.NEUTRAL);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Transition(State.OFFENSIVE);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Transition(State.DEFENSIVE);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }
}

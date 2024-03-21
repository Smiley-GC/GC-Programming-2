using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Change state based on a timer, colour based on state.
// Actions --> coloring
// Conditions --> timeout
public class Enemy : MonoBehaviour
{
    public GameObject player;

    public Transform[] waypoints;
    int waypointIndex = 0;
    float speed = 10.0f;

    public enum State
    {
        NEUTRAL,
        OFFENSIVE,
        DEFENSIVE
    }

    public State state = State.NEUTRAL;
    SpriteRenderer renderer;
    Timer timer = new Timer();

    void Transition(State newState)
    {
        // TODO -- add on exit
        OnEnter(newState);
    }

    void OnEnter(State newState)
    {
        Color color = Color.white;
        switch (newState)
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
        state = newState;
        Debug.Log(state);
    }

    // State-specific per-frame behaviour!
    void OnUpdate()
    {
        // Time-based transition was just for example, now transitioning based on proximity
        // *Condition* to transition state
        //if (timer.Expired())
        //{
        //    timer.Reset();
        //    switch (state)
        //    {
        //        // Neutral can transition between offensive and defensive
        //        case State.NEUTRAL:
        //            // Toss a coin to determine offensive vs defensive
        //            bool offensive = Random.Range(0, 2) == 1;
        //            State newState = offensive ? State.OFFENSIVE : State.DEFENSIVE;
        //            Transition(newState);
        //            break;
        //
        //        // Offensive & defensive can only transition back to neutral
        //        case State.OFFENSIVE:
        //        case State.DEFENSIVE:
        //            Transition(State.NEUTRAL);
        //            break;
        //    }
        //}

        // State-specific behaviour
        switch (state)
        {
            case State.NEUTRAL:
                transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);
                break;

            case State.OFFENSIVE:
                break;

            case State.DEFENSIVE:
                break;
        }
    }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        timer.total = 5.0f;
        OnEnter(state);
    }

    void Update()
    {
        timer.Tick(Time.deltaTime);
        OnUpdate();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Waypoint"))
        {
            ++waypointIndex;
            waypointIndex %= waypoints.Length;
        }
        if (collision.CompareTag("Player"))
        {
            Transition(State.OFFENSIVE);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transition(State.NEUTRAL);
        }
    }
}

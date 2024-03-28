using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Seaons : MonoBehaviour
{
    public enum Season
    {
        WINTER,
        SPRING,
        SUMMER,
        FALL
    }

    Season state = Season.WINTER;

    Timer timer = new Timer();

    void Start()
    {
        timer.total = 3.0f;
        Transition(Season.SPRING);
    }

    void Update()
    {
        OnUpdate();
    }

    void Transition(Season newState)
    {
        OnExit(state);
        state = newState;
        OnEnter(state);
    }

    void OnEnter(Season newState)
    {
        switch (newState) {
            case Season.WINTER:
		        Debug.Log("Brrrr, getting chilly!");
                break;

            case Season.SPRING:
		        Debug.Log("Flowers are blooming");		
                break;

            case Season.SUMMER:
		        Debug.Log("Feeling the heat");		
                break;

            case Season.FALL:
		        Debug.Log("Leave's a-changin");	
                break;
        }
    }

    void OnExit(Season oldState)
    {
        switch (oldState)
        {
            case Season.WINTER:
                Debug.Log("Snow is melting");
                break;

            case Season.SPRING:
                Debug.Log("Flowers are wilting");
                break;

            case Season.SUMMER:
                Debug.Log("No more hot game dev summer :(");
                break;

            case Season.FALL:
                Debug.Log("Dead leaves :(");
                break;
        }
    }

    void OnUpdate()
    {
        // Exact same condition for all 4 transitions
        // (this is rarely the case in actual video games, our AI for example)
        timer.Tick(Time.deltaTime);
        if (timer.Expired())
        {
            timer.Reset();
            switch (state)
            {
                case Season.WINTER:
                    Transition(Season.SPRING);
                    break;

                case Season.SPRING:
                    Transition(Season.SUMMER);
                    break;

                case Season.SUMMER:
                    Transition(Season.FALL);
                    break;

                case Season.FALL:
                    Transition(Season.WINTER);
                    break;
            }
        }
    }
}

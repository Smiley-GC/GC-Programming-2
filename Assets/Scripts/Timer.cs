using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float current = 0.0f;
    public float total = 0.0f;

    public void Tick(float dt)
    {
        current += dt;
    }

    public void Reset()
    {
        current = 0.0f;
    }

    public bool Expired()
    {
        return current >= total;
    }

    public float Percent()
    {
        return Mathf.Clamp01(current / total);
    }
}

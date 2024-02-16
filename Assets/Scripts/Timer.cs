using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float current = 0.0f;
    public float total = 0.0f;

    public void Reset()
    {
        current = 0.0f;
    }

    public void Tick(float deltaTime)
    {
        current += deltaTime;
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

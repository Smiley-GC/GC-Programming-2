using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Review : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Integers are truncated (rounded down) whereas floats store decimal numbers
        int integer = 1 / 2;
        float fraction = 1.0f / 2.0f;
        Debug.Log(integer);
        Debug.Log(fraction);

        // Doubles have twice the precision of floats, so they can store much larger numbers accurately!
        Debug.Log(0.9999999999999);
        Debug.Log(0.9999999999999f);

        // Different data-types have different sizes (sizeof tells you how many bytes each data-type stores).
        Debug.Log("Size of single integer: " + sizeof(int));
        Debug.Log("Size of single decimal: " + sizeof(float));
        Debug.Log("Size of single double-precision decimal: " + sizeof(double));
        Debug.Log("Size of single character: " + sizeof(char));
        Debug.Log("Size of single byte: " + sizeof(byte));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hello World!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 10.0f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.right * -1.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * 1.0f);
        }
    }
}

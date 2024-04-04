using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    Rigidbody2D rb;
    const int jumpCount = 2;
    int jumps = jumpCount;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            jumps--;
            rb.AddForce(Vector3.up * 15.0f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Consider separating platforms by Ground vs Wall tags to prevent infinite wall jumping
        jumps = jumpCount;
    }
}

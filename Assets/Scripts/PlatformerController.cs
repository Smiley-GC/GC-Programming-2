using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    Rigidbody2D rb;
    float accX = 5.0f;
    float maxVelX = 10.0f;
    float jumpForce = 15.0f;

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
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * accX);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * accX);
        }

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxVelX, maxVelX), rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        jumps = jumpCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    Rigidbody2D rb;
    const int jumpCount = 2;
    int jumps = jumpCount;

    float maxSpeedX = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
        {
            jumps--;

            // Reset vertical velocity before applying jump impulse (if we don't want to punish players that are decelerating)
            // Optionally, you could consider only reseting if v.y is < 0 to preserve upwards momentum!
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
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

        // Limit horizontal speed
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeedX, maxSpeedX), rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Consider separating platforms by Ground vs Wall tags to prevent infinite wall jumping
        jumps = jumpCount;
    }
}

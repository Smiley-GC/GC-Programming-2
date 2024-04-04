using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    Rigidbody2D rb;

    const int jumpCount = 2;
    int jumps = jumpCount;
    bool grounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            jumps--;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        //Debug.Log(name);
        if (collision.CompareTag("Ground"))
        {
            jumps = jumpCount;
            grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}

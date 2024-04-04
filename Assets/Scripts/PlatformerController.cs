using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
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
}

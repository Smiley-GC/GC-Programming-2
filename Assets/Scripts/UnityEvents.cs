using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathArgs : EventArgs
{
    public Vector3 deathPosition = Vector3.zero;
}

public class UnityEvents : MonoBehaviour
{
    int deathCount = 0;
    public GameObject player;

    // We can type "using UnityEngine.Events;" at the top of the file to tell C# that we'd like to use the UnityEngine.Events namespace.
    // Otherwise, we'd have to write this variable as "UnityEngine.Events.UnityEvent onPlayerDeath;"
    public UnityEvent<PlayerDeathArgs> onPlayerDeath;

    // "Publishes" our player death event!
    void Die()
    {
        PlayerDeathArgs args = new PlayerDeathArgs();
        args.deathPosition = player.transform.position;

        // We use .Invoke() to publish (also refered to as "dispatch", or "broadcast") unity events!
        onPlayerDeath.Invoke(args);
    }

    void Start()
    {
        onPlayerDeath.AddListener(OnDeathRespawn);  // Subscribe to respawn event handler
        onPlayerDeath.AddListener(OnDeathCounter);  // Subscribe to death count event handler
    }

    void Update()
    {
        player.transform.position += Vector3.right * Time.deltaTime;

        // Dispatch player-death event when we press space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Die();
        }
    }

    void OnDeathCounter(PlayerDeathArgs args)
    {
        if (deathCount == 4)
        {
            Debug.Log("Achievement unlocked -- die x5");
        }
        deathCount++;
        Debug.Log("Death count: " + deathCount);
    }

    void OnDeathRespawn(PlayerDeathArgs args)
    {
        player.transform.position = Vector3.right * -5.0f;
        Debug.Log("Player died at location " + args.deathPosition);
        Debug.Log("Resetting player position to " + player.transform.position);
    }
}

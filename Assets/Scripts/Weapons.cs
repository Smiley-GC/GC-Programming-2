using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class. Defines common attributes (variables) and behaviour (methods) for all derived classes
public abstract class Weapon
{
    // All derived weapons have access to this prefab and color
    public GameObject prefab;
    public Color color;
    public float speed;

    // These aren't required for our homework, but if you're bored feel free to use these
    public int amoCount;
    public float rateOfFire;

    public abstract void Fire(Vector3 position, Vector3 direction);
}

public class Rifle : Weapon
{
    // Rifle constructor. Defines rifle-specific values
    public Rifle()
    {
        color = Color.red;
        amoCount = 35;
        rateOfFire = 420.0f;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        Debug.Log("Fired rifle");
    }
}

public class Shotgun : Weapon
{
    // Shotgun constructor. Defines shotgun-specific values
    public Shotgun()
    {
        color = Color.green;
        amoCount = 5;
        rateOfFire = 15.0f;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        Debug.Log("Fired shotgun");
    }
}

public class Grenade : Weapon
{
    // Grenade constructor. Defines grenade-specific values
    public Grenade()
    {
        color = Color.blue;
        amoCount = 3;
        rateOfFire = 69.0f;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        Debug.Log("Fired grenade");
    }
}
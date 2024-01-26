using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    // Variables necessary for homework:
    public GameObject prefab;
    public float speed;
    public Color color;

    // Optional fun variables:
    public float turnRate;
    public int clipSize;
    public float reloadTime;
    public float bulletDamage;

    // All weapons should have their own Fire() method, so base class is abstract!
    public abstract void Fire(Vector3 position, Vector3 direction);
}

public class Rifle : Weapon
{
    public Rifle()
    {
        speed = 10.0f;
        color = Color.red;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        GameObject projectile = Object.Instantiate(prefab);
        projectile.transform.position = position;

        Debug.Log("Fired rifle");
    }
}

public class Shotgun : Weapon
{
    public Shotgun()
    {
        speed = 5.0f;
        color = Color.green;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        Debug.Log("Fired shotugn");
    }
}

public class Grenade : Weapon
{
    public Grenade()
    {
        speed = 7.5f;
        color = Color.blue;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        Debug.Log("Fired grenade");
    }
}
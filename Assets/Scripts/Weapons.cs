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

    // protected means visible in derived classes, but not externally (ie player can't do weapon.CreateProjectile)
    protected GameObject CreateProjectile(Vector3 position, Vector3 direction)
    {
        GameObject projectile = Object.Instantiate(prefab);
        projectile.transform.position = position + direction;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
        projectile.GetComponent<SpriteRenderer>().color = color;
        return projectile;
    }
}

public class Rifle : Weapon
{
    // Rifle constructor. Defines rifle-specific values
    public Rifle()
    {
        speed = 10.0f;
        color = Color.red;
        amoCount = 35;
        rateOfFire = 420.0f;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        CreateProjectile(position, direction);
    }
}

public class Shotgun : Weapon
{
    // Shotgun constructor. Defines shotgun-specific values
    public Shotgun()
    {
        speed = 5.0f;
        color = Color.green;
        amoCount = 5;
        rateOfFire = 15.0f;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        Vector3 forward = direction;
        Vector3 left = Quaternion.Euler(0.0f, 0.0f, 30.0f) * direction;
        Vector3 right = Quaternion.Euler(0.0f, 0.0f, -30.0f) * direction;

        CreateProjectile(position, forward);
        CreateProjectile(position, left);
        CreateProjectile(position, right);
    }
}

public class Grenade : Weapon
{
    // Grenade constructor. Defines grenade-specific values
    public Grenade()
    {
        speed = 2.5f;
        color = Color.blue;
        amoCount = 3;
        rateOfFire = 69.0f;
    }

    public override void Fire(Vector3 position, Vector3 direction)
    {
        GameObject explosion = CreateProjectile(position, direction);
        explosion.GetComponent<Explosion>().weaponType = WeaponType.GRENADE;
        Object.Destroy(explosion, 1.0f);
    }
}

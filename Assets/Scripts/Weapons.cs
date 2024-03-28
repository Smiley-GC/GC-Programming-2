using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    // Variables necessary for homework:
    public GameObject prefab;
    public float speed;
    public Color color;
    public Owner owner = Owner.NONE;

    // Optional fun variables:
    public float turnRate;
    public int clipSize;
    public float reloadTime;
    public float bulletDamage;

    // All weapons should have their own Fire() method, so base class is abstract!
    public abstract void Fire(Vector3 position, Vector3 direction);

    // protected means this is visible in derived classes but not externally
    // ie Player.cs cannot invoke CreateProjectile()
    protected GameObject CreateProjectile(Vector3 position, Vector3 direction)
    {
        // Instantiate is a static method of Object
        // Static methods allow you to call a function without having an instance of an object
        GameObject projectile = Object.Instantiate(prefab);
        projectile.transform.position = position + direction;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
        projectile.GetComponent<SpriteRenderer>().color = color;
        projectile.GetComponent<Projectile>().owner = owner;
        return projectile;
    }
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
        CreateProjectile(position, direction);
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
        Vector3 forward = direction;
        Vector3 left = Quaternion.Euler(new Vector3(0.0f, 0.0f, 30.0f)) * forward;
        Vector3 right = Quaternion.Euler(new Vector3(0.0f, 0.0f, -30.0f)) * forward;

        CreateProjectile(position, forward);
        CreateProjectile(position, left);
        CreateProjectile(position, right);
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
        GameObject explosion = CreateProjectile(position, direction);
        explosion.GetComponent<Projectile>().weaponType = WeaponType.GRENADE;
        Object.Destroy(explosion, 1.0f);
    }
}
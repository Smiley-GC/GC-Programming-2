using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal
{
    public int instinct;
    // virtual allows dynamic lookup with a base implementation
    // abstract forces dynamic lookip without a base implementation
    public abstract void MakeSound();
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        // Even though we defined insticnt in Animal, since Dog derives from Animal, it also has instinct!
        Debug.Log("Hi I'm Scooby Doo and my master Shaggy has an instinct of " + instinct);
    }

    public void Growl()
    {
        Debug.Log("Grrrrrrrrrr");
    }
}

public class Cat : Animal
{
    public override void MakeSound()
    {
        Debug.Log("Meow meow!");
    }
}

public class Cow : Animal
{
    public override void MakeSound()
    {
        Debug.Log("Moooooooooooooooooooooooooooooooooooooo");
    }
}

public class Fox : Animal
{
    public override void MakeSound()
    {
        Debug.Log("Error 404 -- sound not found");
    }
}

// Homework: Using polymorphism, modify the final exam solution so
// that each weapon is its own class with an overwritten Fire() method.
// Marks -- 1 mark per weapon (3% total),
// 1 mark for storing a single weapon and changing it based on the weapon type,
// 1 mark for overall completeness (5% of your grade total).
public class Polymorphism : MonoBehaviour
{
    public enum AnimalType
    {
        DOG,
        CAT,
        COW,
        FOX
    }

    public void MakeSound(AnimalType type)
    {
        switch (type)
        {
            case AnimalType.DOG:
                Debug.Log("WOOF!");
                break;
        
            case AnimalType.CAT:
                Debug.Log("Meow");
                break;
        
            case AnimalType.COW:
                Debug.Log("Moooooooooo");
                break;
        
            case AnimalType.FOX:
                Debug.Log("*Confusion intensifies*");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Objects interpreted as Animal can only have behaviour common to all animals.
        // (Only the Dog can growl but since dog is an Animal, dog.Growl() is an error).
        //dog.Growl();
        Animal dog = new Dog();
        Animal cat = new Cat();
        dog.MakeSound();
        cat.MakeSound();

        // Press F12 on these to see the different layers of inheritance that MonoBehaviour has!
        IsInvoking();
        Debug.Log(enabled);
        Debug.Log(name);

        // Homework hint:
        // We can only have 1 weapon at a time, so you'll want to create a Weapon object
        // and then switch it based on the collider tag. For example:
        // if rile, weapon = new Rifle
        // if shotgun, weapon = new Shotgun
        // if grenade, weapon = new Grenade

        Dog actualDog = new Dog();
        actualDog.Growl();

        // When we use dynamic lookup (virtual, abstract, override) keywords,
        // the compiler switches through the types automatically.
        AnimalType manualDog = AnimalType.DOG;
        MakeSound(manualDog);

        //Animal[] animals = { new Dog(), new Cat(), new Cow(), new Fox() };
        //for (int i = 0; i < animals.Length; i++)
        //    animals[i].MakeSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

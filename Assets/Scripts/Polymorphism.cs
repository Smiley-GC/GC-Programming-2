using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{
    public virtual void MakeSound()
    {
        Debug.Log("Generic animal sound");
    }
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Debug.Log("WOOF!");
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
    // Start is called before the first frame update
    void Start()
    {
        Animal[] animals = { new Dog(), new Cat(), new Cow(), new Fox() };
        for (int i = 0; i < animals.Length; i++)
            animals[i].MakeSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal
{
    // Default animal constructor
    public Animal()
    {

    }

    // Custom animal constructor
    public Animal(int hp)
    {
        healthPoints = hp;
    }

    public string name = "Connor";
    public int healthPoints;
    // virtual allows dynamic lookup with a base implementation
    // abstract forces dynamic lookip without a base implementation
    //public virtual void MakeSound()
    //{
    //    Debug.Log("*Generic animal noises*");
    //}

    // We don't want unfinished animals in our game. We only want animals with unique sounds!
    // We can force animal-specific behaviour with abstract methods.
    public abstract void MakeSound();

    public void Health()
    {
        Debug.Log("Health: " + healthPoints);
    }
}

public class Dog : Animal
{
    public Dog(int hp) : base(hp)
    {
    }

    public Dog()
    {
        healthPoints = 23;
    }

    // If we remove this method, we get a compiler error cause "abstract" forces said behaviour to be implemented.
    public override void MakeSound()
    {
        Debug.Log(name + ": WOOF!");
        Health();
    }
}

public class Cat : Animal
{
    public Cat(int hp) : base(hp)
    {
        Debug.Log("Creating custom cat with health of " + hp);
    }

    public Cat()
    {
        healthPoints = 14;
    }

    public override void MakeSound()
    {
        Debug.Log(name + ": Meow meow!");
        Health();
    }

    public void Purr()
    {
        Debug.Log("Purrrrrrrrr");
    }
}

public class Cow : Animal
{
    public Cow(int hp) : base(hp)
    {
    }

    public Cow()
    {
        healthPoints = 69;
    }

    public override void MakeSound()
    {
        Debug.Log("Moooooooooooooooooooooooooooooooooooooo");
        Health();
    }
}

public class Fox : Animal
{
    public Fox(int hp) : base(hp)
    {
    }

    public Fox()
    {
        healthPoints = 420;
    }

    public override void MakeSound()
    {
        Debug.Log("Error 404 -- sound not found");
        Health();
    }
}

public abstract class Shape
{
    public abstract float Area();
}

public class Rectangle : Shape
{
    public override float Area()
    {
        return width * height;
    }

    public float width;
    public float height;
}

public class Triangle : Shape
{
    public override float Area()
    {
        return (width * height) / 2.0f;
    }

    public float width;
    public float height;
}

public class Circle : Shape
{
    public override float Area()
    {
        return 2.0f * Mathf.PI * radius;
    }

    public float radius;
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

    // Passes color "by value" (copy of the variable)
    void ChangeColor(Color color)
    {
        color = Color.green;
    }

    // Passes color "by reference" (actual variable)
    void ChangeColor(ref Color color)
    {
        color = Color.green;
    }

    public class Test
    {
        public int value;
    }

    // Classes are automatically passed by reference (you don't need the ref keyword)
    void ChangeTest(Test test)
    {
        test.value = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Not currently polymorphic since all shapes are unique types (ie Rectangle instead of Shape)
        // Only use polymorphism if you actually NEED automation
        Rectangle rectangle = new Rectangle();
        Triangle triangle = new Triangle();
        Circle circle = new Circle();

        rectangle.width = triangle.width = 100.0f;
        rectangle.height = triangle.height = 50.0f;
        circle.radius = 25.0f;

        Debug.Log(rectangle.Area());
        Debug.Log(triangle.Area());
        Debug.Log(circle.Area());

        // "Polymorphism is automation"
        // Only by looping through a collection of generic objects do we achieve polymorphism
        // ("Same thing [shape], different behaviours [shape-specific Area()])
        Shape[] shapes = { rectangle, triangle, circle };
        for (int i = 0; i < shapes.Length; i++)
            Debug.Log(shapes[i].Area());

        Test test = new Test();
        Debug.Log(test.value);
        ChangeTest(test);
        Debug.Log(test.value);

        Color color = Color.red;
        Debug.Log("Color before: " + color);
        ChangeColor(ref color);
        Debug.Log("Color after: " + color);

        // This becomes a compiler error once we make Animal an abstract class
        //Animal anmimal = new Animal();

        Animal dog = new Dog();
        Animal cat = new Cat(2);
        Animal cow = new Cow();
        Animal fox = new Fox();

        // Dynamic lookup because we don't know which animal sound to make until runtime
        dog.MakeSound();
        cat.MakeSound();
        cow.MakeSound();
        fox.MakeSound();

        Cat actualCat = new Cat();
        //cat.Purr();   // <-- apparently not a real cat xDD
        actualCat.Purr();   // "Static lookup" because we know only the Cat can pur

        // When we use dynamic lookup (virtual, abstract, override) keywords,
        // the compiler switches through the types automatically.
        //AnimalType manualFox = AnimalType.FOX;
        //MakeSound(manualFox);

        //Animal[] animals = { new Dog(), new Cat(), new Cow(), new Fox() };
        //for (int i = 0; i < animals.Length; i++)
        //    animals[i].MakeSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

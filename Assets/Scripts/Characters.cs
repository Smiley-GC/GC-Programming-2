using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public abstract class Character
    {
        public virtual void Attack()
        {
            Debug.Log("Intimidating noises");
        }
    }

    public class Human : Character
    {
        public override void Attack()
        {
            Debug.Log("Intimidating Human noises");
        }
    }
    public class Elves : Character
    {
        public override void Attack()
        {
            Debug.Log("Intimidating Elves noises");
        }
    }
    public class Orc : Character
    {
        public override void Attack()
        {
            Debug.Log("Intimidating Orc noises");
        }
    }
    public class Undead : Character
    {
        public override void Attack()
        {
            Debug.Log("Intimidating Undead noises");
        }
    }

    public enum Type
    {
        Human,
        Elve,
        Orc,
        Undead
    }

    public void Attacktype(Type type)
    {
        switch (type)
        {
            case Type.Human:
                Debug.Log("Sword Swing");
                break;

            case Type.Elve:
                Debug.Log("");
                break;

            case Type.Orc:
                Debug.Log("Moooooooooo");
                break;

            case Type.Undead:
                Debug.Log("Confusion intensifies");
                break;
        }
    }

    delegate string AttackFunction();

    string HumanAttack()
    {
        return "Sword Swing";
    }

    string ElfAttack()
    {
        return "";
    }

    string OrcAttack()
    {
        return "MOoooooooooo";
    }

    string UndeadAttack()
    {
        return "Braiiiiiiiiins";
    }

    void Start()
    {
        Character[] characters = { new Human(), new Elves(), new Orc(), new Undead() };
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].Attack();
        }

        Type[] types = { Type.Human, Type.Elve, Type.Orc, Type.Undead };
        for (int i = 0; i < types.Length; i++)
        {
            Attacktype(types[i]);
        }

        AttackFunction[] attacks = { HumanAttack, ElfAttack, OrcAttack, UndeadAttack };
        for (int i = 0; i < attacks.Length; i++)
            Debug.Log(attacks[i]());
    }
}

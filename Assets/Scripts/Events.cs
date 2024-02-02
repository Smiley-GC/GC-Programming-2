using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    // Delegates -- 15 lines of code (#2 speed). Prefer when *behaviour only*
    float Add(float a, float b)
    {
        return a + b;
    }

    float Sub(float a, float b)
    {
        return a - b;
    }

    float Mul(float a, float b)
    {
        return a * b;
    }

    float Div(float a, float b)
    {
        return a / b;
    }

    delegate float MathOperation1(float a, float b);

    void Start()
    {
        float a = 1.0f;
        float b = 2.0f;
        MathOperation1 mathOperation1 = null;
        MathOperation2 mathOperation2 = null;
        MathOperation3 mathOperation3;

        mathOperation1 = Add;
        Debug.Log(mathOperation1(a, b));

        mathOperation1 = Sub;
        Debug.Log(mathOperation1(a, b));

        mathOperation1 = Mul;
        Debug.Log(mathOperation1(a, b));

        mathOperation1 = Div;
        Debug.Log(mathOperation1(a, b));

        mathOperation2 = new AddOperation();
        Debug.Log(mathOperation2.Operation(a, b));

        mathOperation2 = new SubOperation();
        Debug.Log(mathOperation2.Operation(a, b));

        mathOperation2 = new MulOperation();
        Debug.Log(mathOperation2.Operation(a, b));

        mathOperation2 = new DivOperation();
        Debug.Log(mathOperation2.Operation(a, b));

        mathOperation3 = MathOperation3.ADD;
        Debug.Log(Operation(a, b, mathOperation3));

        mathOperation3 = MathOperation3.SUB;
        Debug.Log(Operation(a, b, mathOperation3));

        mathOperation3 = MathOperation3.MUL;
        Debug.Log(Operation(a, b, mathOperation3));

        mathOperation3 = MathOperation3.DIV;
        Debug.Log(Operation(a, b, mathOperation3));

        MathOperation1[] mathOperation1s = { Add, Sub, Mul, Div };
        MathOperation2[] mathOperation2s = { new AddOperation(), new SubOperation(), new MulOperation(), new DivOperation() };
        MathOperation3[] mathOperation3s = { MathOperation3.ADD, MathOperation3.SUB, MathOperation3.MUL, MathOperation3.DIV };
    
        // Did someone say polymorphism? ;)
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(mathOperation1s[i](a, b));
            Debug.Log(mathOperation2s[i].Operation(a, b));
            Debug.Log(Operation(a, b, mathOperation3s[i]));
        }
    }

    // Object-Oriented -- 35 lines of code (#3 speed).
    // Avoid like the plague. Only use when you NEED data AND behaviour AND automation.
    // (9/10 times we need this because we need to know not only our weapon's behaviour,
    // but also its data like color, fire rate, clip size, projectile speed, etc).
    // For those interested in how to fix the performance hit, see: https://gameprogrammingpatterns.com/component.html
    abstract class MathOperation2
    {
        public abstract float Operation(float a, float b);
    }

    class AddOperation : MathOperation2
    {
        public override float Operation(float a, float b)
        {
            return a + b;
        }
    }

    class SubOperation : MathOperation2
    {
        public override float Operation(float a, float b)
        {
            return a - b;
        }
    }

    class MulOperation : MathOperation2
    {
        public override float Operation(float a, float b)
        {
            return a * b;
        }
    }

    class DivOperation : MathOperation2
    {
        public override float Operation(float a, float b)
        {
            return a / b;
        }
    }

    // Old School -- 17 lines of code (#1 speed). Prefer when performance matters
    enum MathOperation3
    {
        ADD,
        SUB,
        MUL,
        DIV
    }

    float Operation(float a, float b, MathOperation3 type)
    {
        switch (type)
        {
            case MathOperation3.ADD: return a + b;
            case MathOperation3.SUB: return a - b;
            case MathOperation3.MUL: return a * b;
            case MathOperation3.DIV: return a / b;
        }
        return 0.0f;
    }
}

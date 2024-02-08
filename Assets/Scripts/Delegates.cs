using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates : MonoBehaviour
{
    public enum MathType
    {
        ADD,
        SUB,
        MUL,
        DIV
    }

    float MathOperation(float a, float b, MathType type)
    {
        switch (type)
        {
            case MathType.ADD: return a + b;
            case MathType.SUB: return a - b;
            case MathType.MUL: return a * b;
            case MathType.DIV: return a / b;
        }
        return 0.0f;
    }

    // delegate / "function-pointer" syntax:
    // "delegate" <return-type> <FunctionName>(argument 1, argument 2, ...argument n);
    delegate float MathFunction(float a, float b);

    // A null math-operation, meaning it "points to" nothing
    MathFunction mathOperation = null;

    // Start is called before the first frame update
    void Start()
    {
        float a = 1.0f;
        float b = 2.0f;

        // Way 1: old-school manual polymorphism (when in doubt, do this)
        MathType type = MathType.ADD;
        Debug.Log(MathOperation(a, b, type));

        type = MathType.SUB;
        Debug.Log(MathOperation(a, b, type));

        type = MathType.MUL;
        Debug.Log(MathOperation(a, b, type));

        type = MathType.DIV;
        Debug.Log(MathOperation(a, b, type));

        // Way 2: delegates ("function pointers" -- best when *behaviour-only*)
        mathOperation = Add;
        Debug.Log(mathOperation(a, b));

        mathOperation = Sub;
        Debug.Log(mathOperation(a, b));

        mathOperation = Mul;
        Debug.Log(mathOperation(a, b));

        mathOperation = Div;
        Debug.Log(mathOperation(a, b));

        // Way 3: object-oriented-style polymorphism. (Use when you *need* to combine data & behaviour)
        MathOp mathOp = null;

        mathOp = new AddOp();
        Debug.Log(mathOp.Operation(a, b));

        mathOp = new SubOp();
        Debug.Log(mathOp.Operation(a, b));

        mathOp = new MulOp();
        Debug.Log(mathOp.Operation(a, b));

        mathOp = new DivOp();
        Debug.Log(mathOp.Operation(a, b));

        // Automate the above process -- use a for-loop and array to
        // loop through math operations and call them

        MathOp[] mathOps = { new AddOp(), new SubOp(), new MulOp(), new DivOp() };
        for (int i = 0; i < mathOps.Length; i++)
        {
            Debug.Log(mathOps[i].Operation(a, b));
        }

        MathFunction[] mathOperations = { Add, Sub, Mul, Div };
        for (int i = 0; i < mathOperations.Length; i++)
            Debug.Log(mathOperations[i](a, b));

        MathType[] operations = { MathType.ADD, MathType.SUB, MathType.MUL, MathType.DIV };
        for (int i = 0; i < operations.Length; i++)
        {
            Debug.Log(MathOperation(a, b, operations[i]));
        }
    }

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

    public abstract class MathOp
    {
        public abstract float Operation(float a, float b);
    }

    public class AddOp : MathOp
    {
        public override float Operation(float a, float b)
        {
            return a + b;
        }
    }

    public class SubOp : MathOp
    {
        public override float Operation(float a, float b)
        {
            return a - b;
        }
    }

    public class MulOp : MathOp
    {
        public override float Operation(float a, float b)
        {
            return a * b;
        }
    }

    public class DivOp : MathOp
    {
        public override float Operation(float a, float b)
        {
            return a / b;
        }
    }
}

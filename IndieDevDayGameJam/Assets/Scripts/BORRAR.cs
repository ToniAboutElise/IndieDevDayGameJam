using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BORRAR : MonoBehaviour
{
    int a, b, c;
    void Start()
    {
        a = 2;
        b = 4;
        c = a + b;
        Console.Write("Result of {0} is {1} plus {2}", a, b, c);
        Debug.Log(Console.Read() + " " + a + " " + b + " ");
    }
}

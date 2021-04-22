using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector2 : MonoBehaviour
{
    // I need a MyVector2 that uses doubles instead of floats
    public double x;
    public double y;

    public MyVector2(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
}

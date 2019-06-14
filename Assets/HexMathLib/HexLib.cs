using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{
    //straight double
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    //variant constructors for unity
    //narrowing version
    public Point(Vector2 convert)
    {
        this.x = convert.x;
        this.y = convert.y;
    }
    //vec 3 for flat on ground hexes
    public Point(Vector3 convert)
    {
        this.x = convert.x;
        this.y = convert.z;
    }
    public readonly double x;
    public readonly double y;

    //utility functions for unity
    Vector2 toVec2(Point point)
    {
        //remember rounding!!!
        return new Vector2((float)point.x, (float)point.y);
    }
    Vector3 toVec3(Point point)
    {
        //remember rounding!!! also its flat
        return new Vector3((float)point.x, 0, (float)point.y);
    }

}

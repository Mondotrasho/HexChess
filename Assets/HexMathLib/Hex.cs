using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public struct Hex
{
    public int q;
    public int r;
    public readonly int s;

    public Hex(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
        if (q + r + s != 0) throw new ArgumentException("q + r + s must be 0");
    }

    public Hex(int q, int r)
    {
        this.q = q;
        this.r = r;
        this.s = -q - r;
    }


    public Hex Add(Hex b)
    {
        return new Hex(q + b.q, r + b.r, s + b.s);
    }


    public Hex Subtract(Hex b)
    {
        return new Hex(q - b.q, r - b.r, s - b.s);
    }

    public Hex hex_multiply(Hex a, int k)
    {
        return new Hex(a.q * k, a.r * k, a.s * k);
    }

    public Hex Scale(int k)
    {
        return new Hex(q * k, r * k, s * k);
    }


    public Hex RotateLeft()
    {
        return new Hex(-s, -q, -r);
    }


    public Hex RotateRight()
    {
        return new Hex(-r, -s, -q);
    }

    //starting with right hex rotating counter clockwise
    //if using a flat layout it is the bottom right hex rotating clockwise
    static public List<Hex> directions = new List<Hex> {
        new Hex(1, 0, -1),        new Hex(1, -1, 0),        new Hex(0, -1, 1),
        new Hex(-1, 0, 1),        new Hex(-1, 1, 0),        new Hex(0, 1, -1) };

    //input must be in range
    static public Hex Direction(int direction)
    {
        Debug.Assert(0 <= direction && direction < 6);
        return Hex.directions[direction];
    }


    public Hex Neighbor(int direction)
    {
        return Add(Hex.Direction(direction));
    }

    static public List<Hex> diagonals = new List<Hex> {
        new Hex(2, -1, -1), new Hex(1, -2, 1), new Hex(-1, -1, 2),
        new Hex(-2, 1, 1), new Hex(-1, 2, -1), new Hex(1, 1, -2) };

    //input must be in range
    public Hex DiagonalNeighbor(int direction)
    {
        Debug.Assert(0 <= direction && direction < 6);
        return Add(Hex.diagonals[direction]);
    }
    

    public int Length()
    {
        return (int)((Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2);
    }


    public int Distance(Hex b)
    {
        return Subtract(b).Length();
    }

    //operators
    public static bool operator ==(Hex a, Hex b)
    {
        return a.q == b.q && a.r == b.r && a.s == b.s;
    }
    public static bool operator !=(Hex a, Hex b)
    {
        return !(a == b);
    }

    public void set(Hex newhex)
    {
        if (newhex.q + newhex.r + newhex.s != 0)
        {
            Debug.LogAssertion(("qrs =" + newhex.q.ToString() + newhex.r.ToString() + newhex.s.ToString()));
            return;
        }

        this = newhex;
    }

    public List<Hex> Neighbors()
    {
        List<Hex> list = new List<Hex>();

        for (int i = 0; i < 6; i++)
        {
            list.Add(this.Neighbor(i));
        }
        return list;
    }


    public static Vector3 HexToVec3(Hex hex, Layout layout, float height = 0.0f)
    {
        Point a = layout.HexToPixel(hex);
        return new Vector3((float)a.x, height, (float)a.y);
    }

    public static Hex Vec3ToHex(Vector3 vec3, Layout layout)
    {
        //fractional point that the hit is
        FractionalHex fractionalhexpos = layout.PixelToHex(new Point(vec3.x, vec3.z));

        return fractionalhexpos.HexRound();
    }

    public static bool HexRaycast(out Hex hexpos, out Vector3 pos, Layout layout,bool upnormal = true)
    {
        //create ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //length to check
        float distance = 5000;
        //returned data
        RaycastHit hit;

        Hex hithex = new Hex(999, 999);
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.normal == Vector3.up)
            {
                hithex = Vec3ToHex(ray.GetPoint(hit.distance), layout);

                pos = HexToVec3(hithex, layout);
                hexpos = hithex;
                return true;
            }
        }

        hexpos = default;
        pos = default;
        return false;
    }

    public static bool HexRaycast(out Hex hexpos, out Vector3 pos, Layout layout, string layerstring, bool upnormal = true)
    {
        //create ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //length to check
        float distance = 5000;
        //returned data
        RaycastHit hit;
      
        int layer = (1 << (LayerMask.NameToLayer(layerstring)));
        if (Physics.Raycast(ray, out hit, distance,layer))
        {
            if (hit.normal == Vector3.up)
            {
                Hex hithex = Vec3ToHex(ray.GetPoint(hit.distance), layout);

                pos = HexToVec3(hithex, layout);
                hexpos = hithex;
                return true;
            }
        }

        hexpos = default;
        pos = default;
        return false;
    }
}

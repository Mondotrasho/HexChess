using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public struct HexStorage
{

    public static void makemap()
    {
        //tests storing in the map
        Dictionary<Hex, float> heights = new Dictionary<Hex, float>();
        heights.Add(new Hex(1, -2), 4.3f);
        Debug.Log(heights[new Hex(1, -2)]);
    }

    public static HashSet<Hex> storeparrelogramn()
    {
        HashSet<Hex> map = new HashSet<Hex>();
        int q1 = 1;
        int q2 = 10;
        int r1 = 1;
        int r2 = 10;

        for (int q = q1; q <= q2; q++)
        {
            for (int r = r1; r <= r2; r++)
            {
                map.Add(new Hex(q, r, -q - r));
            }
        }

        return map;
    }

    public static Dictionary<Hex, int> storeparrelogramnwithrandtype(int numtiles, int rowslength, int colslength)
    {
        Dictionary<Hex, int> map = new Dictionary<Hex, int>();
        int q1 = 1;
        int q2 = rowslength;
        int r1 = 1;
        int r2 = colslength;

        Random rnd = new Random();

        for (int q = q1; q <= q2; q++)
        {
            for (int r = r1; r <= r2; r++)
            {
                map.Add(new Hex(q, r, -q - r), rnd.Next(0, numtiles));
            }
        }

        return map;
    }

    void storetriangleupdown()
    {
        HashSet<Hex> map = new HashSet<Hex>();
        int map_size = 10;


        for (int q = 0; q <= map_size; q++)
        {
            for (int r = 0; r <= map_size - q; r++)
            {
                map.Add(new Hex(q, r, -q - r));
            }
        }
    }

    void storetriangleleftright()
    {
        HashSet<Hex> map = new HashSet<Hex>();
        int map_size = 10;


        for (int q = 0; q <= map_size; q++)
        {
            for (int r = map_size - q; r <= map_size; r++)

            {
                map.Add(new Hex(q, r, -q - r));
            }
        }
    }

    void storetriangleHex()
    {
        HashSet<Hex> map = new HashSet<Hex>();
        int map_radius = 10;


        for (int q = -map_radius; q <= map_radius; q++)
        {
            int r1 = Math.Max(-map_radius, -q - map_radius);
            int r2 = Math.Min(map_radius, -q + map_radius);
            for (int r = r1; r <= r2; r++)
            {
                map.Add(new Hex(q, r, -q - r));
            }
        }
    }

    void storetriangleRect()
    {
        HashSet<Hex> map = new HashSet<Hex>();
        int map_height = 10;
        int map_width = 10;

        for (int r = 0; r < map_height; r++)
        {
            //hope this works i added narrowing conversions etc
            double dome = r / 2;
            int r_offset = (int) Math.Floor(dome); // or r>>1
            for (int q = -r_offset; q < map_width - r_offset; q++)
            {
                map.Add(new Hex(q, r, -q - r));
            }
        }
    }

    /*
     * unordered_set<Hex> map;
for (int q = -map_radius; q <= map_radius; q++) {
    int r1 = max(-map_radius, -q - map_radius);
    int r2 = min(map_radius, -q + map_radius);
    for (int r = r1; r <= r2; r++) {
        map.insert(Hex(q, r, -q-r));
    }
}
     */
    public static Dictionary<Hex, int> storehexagonmap(int numtiles, int radius)
    {
        Dictionary<Hex, int> map = new Dictionary<Hex, int>();

        Random rnd = new Random();

        for (int q = -radius; q <= radius; q++)
        {
            int r1 = Math.Max(-radius, -q - radius);
            int r2 = Math.Min(radius, -q + radius);
            for (int r = r1; r <= r2; r++)
            {
                //starting tiles are red
                if (r == 3 || r == 4 || r == -3 || r == -4)
                {
                    map.Add(new Hex(q, r, -q - r), 0);
                }
                else
                {
                    //empty tiles are grey
                    //wateer tiles are blue
                    //low chance of blue tiles
                    int chance = rnd.Next(1, 11);
                    if (chance == 10)
                    {
                        map.Add(new Hex(q, r, -q - r), 1);
                    }
                    else if(chance == 8)
                    {
                        map.Add(new Hex(q, r, -q - r), 6);
                    }
                    else
                    {
                        map.Add(new Hex(q, r, -q - r), 2);
                    }

                }
                    
            }
        }
        return map;
    }
}
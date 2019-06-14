using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePasser : MonoBehaviour
{
    [Header("Bridge Tiles")]

    public HexTile Bridge1;
    public HexTile Bridge2;
    public HexTile Bridge3;

    [Header("Ground Tiles")]
    [Space(3)]

    public HexTile Ground1;
    public HexTile Ground2;

    [Header("Water Tiles")]
    [Space(3)]

    public HexTile Water1;

    [Header("Mountain Tiles")]
    [Space(3)]

    public HexTile Mountain1;
    public HexTile Mountain2;
    public HexTile Mountain3;
    public HexTile Mountain4;
    public HexTile Mountain5;
    public HexTile Mountain6;
    public HexTile Mountain7;
    public HexTile Mountain8;
    public HexTile Mountain9;

    [Header("Spawn Tiles")]
    [Space(3)]

    public HexTile Spawn1;
    public HexTile Spawn2;
    public HexTile Spawn3;
    public HexTile Spawn4;
    public HexTile Spawn5;

    private List<HexTile> PassMe = new List<HexTile>();

    private void initall()
    {
        Bridge1.init();
        Bridge2.init();
        Bridge3.init();

        Ground1.init();
        Ground2.init();

        Water1.init();

        Mountain1.init();
        Mountain2.init();
        Mountain3.init();
        Mountain4.init();
        Mountain5.init();
        Mountain6.init();
        Mountain7.init();
        Mountain8.init();
        Mountain9.init();

        Spawn1.init();
        Spawn2.init();
        Spawn3.init();
        Spawn4.init();
        Spawn5.init();
        
    }

    public List<HexTile> Pass()
    {
        initall();
        PassMe.Clear();

        PassMe.Add(Bridge1);
        PassMe.Add(Bridge2);
        PassMe.Add(Bridge3);

        PassMe.Add(Ground1);
        PassMe.Add(Ground2);

        PassMe.Add(Water1);

        PassMe.Add(Mountain1);
        PassMe.Add(Mountain2);
        PassMe.Add(Mountain3);
        PassMe.Add(Mountain4);
        PassMe.Add(Mountain5);
        PassMe.Add(Mountain6);
        PassMe.Add(Mountain7);
        PassMe.Add(Mountain8);
        PassMe.Add(Mountain9);

        PassMe.Add(Spawn1);
        PassMe.Add(Spawn2);
        PassMe.Add(Spawn3);
        PassMe.Add(Spawn4);
        PassMe.Add(Spawn5);

        return PassMe;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//A GameObject that is a tile and HALF the Height of its mesh

public class HexTile : MonoBehaviour
{
    public bool Pathable;
    public float TravelCost = 1;
    public GameObject TileObject;
    public float Yextent;
    public float Xextent;


    public void init()
    {
        TileObject = transform.gameObject;
        if (GetComponent<MeshRenderer>())
        {
            Yextent = TileObject.GetComponent<MeshRenderer>().bounds.extents.y;
            Xextent = TileObject.GetComponent<MeshRenderer>().bounds.extents.x;
        }
    }

    public Vector3 GetTopFace()
    {
        return transform.position + new Vector3(0, Yextent, 0);
    }
    public Vector3 GetBottomFace()
    {
        return transform.position - new Vector3(0, Yextent, 0);
    }

    public static void DeleteOldTiles(GameObject ParentObj)
    {
        List<GameObject> Children = new List<GameObject>();
        foreach (Transform child in ParentObj.transform)
        {
            Children.Add(child.gameObject);
        }

        foreach (var ch in Children)
        {
            //Have to use
            if (!Application.isPlaying)
            {
                DestroyImmediate(ch);
            }
            else
            {
                Destroy(ch);
            }
        }
    }

    public static void InstantiateTiles(Dictionary<Hex, HexCollection> dictionary, Layout layout,GameObject ParentObj)
    {
        foreach (var entry in dictionary)
        {//todo maybe switch for an entry.value.pos
            var clone = Instantiate(entry.Value.Tile.transform.gameObject, Hex.HexToVec3(entry.Key, layout) + new Vector3(0, entry.Value.Tile.Yextent), Quaternion.identity);
            clone.GetComponent<HexTile>().Pathable = entry.Value.Tile.Pathable;
            dictionary[entry.Key].Traversable = entry.Value.Tile.Pathable;
            clone.name = entry.Key.q + " : " + entry.Key.r;
            clone.transform.parent = ParentObj.transform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPiece : MonoBehaviour
{
    public enum Team { None,White,Black };

    public GameObject PieceObject;
    public float Yextent, Xextent;
    public Team team = Team.None;
    public PathMoverCorutine MoveSystem = new PathMoverCorutine();

    public void init()
    {
        PieceObject = this.gameObject;
        if (GetComponent<MeshRenderer>())
        {
            Yextent = GetComponent<MeshRenderer>().bounds.extents.y;
            Xextent = GetComponent<MeshRenderer>().bounds.extents.x;
        }

        MoveSystem = GetComponent<PathMoverCorutine>();
    }

    public void UpdateGameobject()
    {
        PieceObject = this.gameObject;
    }

    public Vector3 GetTopFace()
    {
        return transform.position + new Vector3(0, Yextent, 0);
    }
    public Vector3 GetBottomFace()
    {
        return transform.position - new Vector3(0, Yextent, 0);
    }

    public void Move(Hex start, Hex end, List<Vector3> path,Dictionary<Hex,HexCollection> map)
    {
        LeavePos(start, map);

        SetCorutineMovement(path);

        EnterPos(end, map);
    }

    public void SetCorutineMovement(List<Vector3> path)
    {
        MoveSystem.Targets = path;
        MoveSystem.moving = true;
    }

    public void LeavePos(Hex start, Dictionary<Hex, HexCollection> map)
    {
        if (map.ContainsKey(start))
        {
            map[start].Traversable = true;
            map[start].Occupiedteam = HexCollection.Team.None;
            map[start].Piece = null;
            map[start].Tile.Pathable = true;
        }
    }

    private void EnterPos(Hex end, Dictionary<Hex, HexCollection> map)
    {
        if (map.ContainsKey(end)) {
        map[end].Traversable = false;
        map[end].Occupiedteam = (HexCollection.Team)team;
        map[end].Piece = this;
        map[end].Tile.Pathable = false;
        }
    }

    public static List<Hex> ReturnAllHexWithPieces(Dictionary<Hex, HexCollection> dictionary)
    {
        List<Hex> list = new List<Hex>();

        foreach (var obj in dictionary)
        {
            if (obj.Value.Piece != null)
            {
                list.Add(obj.Key);
            }
        }

        return list;
    }

    public static List<Hex> ReturnAllHexWithWhitePieces(Dictionary<Hex, HexCollection> dictionary)
    {
        var all = ReturnAllHexWithPieces(dictionary);
        List<Hex> White = new List<Hex>();

        foreach (var a in all)
        {
            if (dictionary[a].Occupiedteam == HexCollection.Team.White)
            {
                White.Add(a);
            }
        }

        return White;
    }

    public static List<Hex> ReturnAllHexWithBlackPieces(Dictionary<Hex, HexCollection> dictionary)
    {
        var all = ReturnAllHexWithPieces(dictionary);
        List<Hex> Black = new List<Hex>();

        foreach (var a in all)
        {
            if (dictionary[a].Occupiedteam == HexCollection.Team.Black)
            {
                Black.Add(a);
            }
        }

        return Black;
    }

    public static void InstantiatePieces(Dictionary<Hex, HexCollection> dictionary, Layout layout, GameObject ParentObj)
    {
        Dictionary<Hex, HexCollection> newDictionary = new Dictionary<Hex, HexCollection>();

        foreach (var entry in dictionary)
        {
            if (entry.Value.Piece != null)
            {
                var clone = Instantiate(entry.Value.Piece.PieceObject,
                    Hex.HexToVec3(entry.Key, layout) + new Vector3(0, entry.Value.Tile.Yextent * 2),
                    Quaternion.identity);
                clone.name = entry.Value.Piece.name + " Clone" + Random.value;
                clone.GetComponent<HexPiece>().UpdateGameobject();
                newDictionary.Add(entry.Key, entry.Value);
                newDictionary[entry.Key].Piece= clone.GetComponent<HexPiece>();
                clone.transform.parent = ParentObj.transform;
            }
            else
            {
                newDictionary.Add(entry.Key, entry.Value);
            }
            
        }

        dictionary = newDictionary;
    }

    public static void DeleteOldPieces(GameObject ParentObj)
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
}

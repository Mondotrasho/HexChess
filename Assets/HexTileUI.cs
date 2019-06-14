using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HexTileUI : MonoBehaviour
{

    public Hex MousePos;
    public Vector3 pos;
    public Hex OldPos;
    public HexTile mouseObj;
    //public HexTile GlowBob;
    public Material OK;
    public Material NOTOK;
    public Material MYPIECE;
    private HexCollection.Team CurrentTeam = HexCollection.Team.White;

    public void initMouse()
    {
        mouseObj.init();
    }

    public void updateMousePos(Layout layout)
    {
        Hex temp;
        Vector3 temppos;
        if (Hex.HexRaycast(out temp, out temppos, layout, "Tile"))
        {
            MousePos = temp;
            pos = temppos;
        }
    }

    public void DrawMousehex(Dictionary<Hex, HexCollection> dictionary)
    {
        if (OldPos != MousePos)
        {
            var mod = new Vector3(0, dictionary[MousePos].Tile.Yextent * 2);
            var clone = Instantiate(mouseObj.TileObject, pos + mod, Quaternion.identity);
            clone.GetComponent<Bob>().originalY = mod.y;
            clone.name = "Mouse";
            OldPos = MousePos;
            DeleteOldTileUI(this.gameObject);
            clone.transform.parent = this.gameObject.transform;
            if (dictionary[MousePos].Occupiedteam == CurrentTeam)
            {
                clone.GetComponent<MeshRenderer>().material = MYPIECE;
            }
            else
            {
                clone.GetComponent<MeshRenderer>().material = dictionary[MousePos].Traversable == true ? OK : NOTOK;
            }

        }
    }

    public static void HightlightSurroundingHexes(Dictionary<Hex, HexCollection> dictionary, Layout layout, GameObject ParentObj, HexTile UIGlowbob)
    {
        Hex hex;
        Vector3 vec3;

        if (Hex.HexRaycast(out hex, out vec3, layout))
        {
            //ObjectsDictionary[hex].Piece.PieceObject = vec3 + Vector3.up;

            HexTileUI.DeleteOldTileUI(ParentObj);
            for (int i = 0; i < 6; i++)
            {
                if (dictionary[hex.Neighbor(i)].Traversable)
                {
                    var pos = Hex.HexToVec3(hex.Neighbor(i), layout);
                    var mod = new Vector3(0, dictionary[hex.Neighbor(i)].Tile.Yextent * 2);

                    var clone = Instantiate(UIGlowbob.TileObject, pos + mod, Quaternion.identity);
                    clone.GetComponent<Bob>().originalY = mod.y;
                    clone.name = "Neighbour " + i;
                    clone.transform.parent = ParentObj.transform;
                }
            }

        }
    }

    public static void HighlightLineHexes(Dictionary<Hex, HexCollection> dictionary, Layout layout,GameObject ParentObj, HexTile UIGlowbob, HexPathfinding pathfinder, HexTileUI tileUi)
    {
        HexTileUI.DeleteOldTileUI(ParentObj);

        if (dictionary.ContainsKey(pathfinder.Start) && dictionary.ContainsKey(tileUi.MousePos))
        {
            List<Hex> positions = pathfinder.BreadthFirstSearch(pathfinder.Start, tileUi.MousePos);


            List<GameObject> LineObj = new List<GameObject>();
            foreach (Hex point in positions)
            {
                var pos = Hex.HexToVec3(point, layout);
                var mod = new Vector3(0, dictionary[point].Tile.Yextent * 2);
                var clone = Instantiate(UIGlowbob.TileObject, pos + mod, Quaternion.identity);
                clone.GetComponent<Bob>().originalY = mod.y;
                clone.name = "Line";
                LineObj.Add(clone);
            }

            int multi = 1;
            foreach (var obj in LineObj)
            {
                obj.transform.parent = ParentObj.transform;
                obj.GetComponent<Bob>().timeOffset = (6.3f / LineObj.Count) * multi;
                multi++;
            }
        }

    }

    public static void DeleteOldTileUI(GameObject ParentObj)
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

    public static void InstatiateTileUI(HexTile UIGlowbob)
    {
        UIGlowbob.init();
    }

    public bool cannotclick = false;
    public void MouseHexClick(HexPathfinding pathfinder, Dictionary<Hex, HexCollection> dictionary)
    {
         if (cannotclick)
        {
            StartCoroutine(pause(.1f));
        }
         else if (Input.GetMouseButton(0))
        {
            if ((dictionary[MousePos].Traversable || dictionary[MousePos].Occupiedteam == CurrentTeam) && dictionary.ContainsKey(MousePos))
            {
                if (dictionary[MousePos].Piece != null)
                {
                    bool test = pathfinder.selectedPiece == null;
                    pathfinder.selectedPiece = dictionary[MousePos].Piece.PieceObject;
                    if (pathfinder.selectedPiece != null && test)
                    {
                        Debug.Log("Changed");
                    }
                    pathfinder.Start = MousePos;
                    pathfinder.End = pathfinder.EMPTY;
                    cannotclick = true;
                }
                else if (pathfinder.selectedPiece != null && pathfinder.Start != pathfinder.EMPTY)
                {
                    pathfinder.End = MousePos;

                    cannotclick = true;
                }

            }
        }


    }

    public void setClicked(bool a)
    {
        cannotclick = a;
    }
    public IEnumerator pause(float time)
    {
        //print(Time.time);
        yield return new WaitForSeconds(time);
        setClicked(false);
        //print(Time.time);
    }
}

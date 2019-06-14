using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPathfinding : MonoBehaviour
{
    public Hex EMPTY = new Hex(int.MaxValue, int.MaxValue);
    public Hex Start;
    public Hex End;
    public Layout layout;
    private Dictionary<Hex, HexCollection> dictionary;

    public List<Hex> points = new List<Hex>();
    public Hex oldStart;
    public Hex oldEnd;
    public List<Hex> OldPath = new List<Hex>();
    public GameObject selectedPiece = null;
    public List<Hex> OldUIPath = new List<Hex>();

    public void Setup(Dictionary<Hex, HexCollection> Dict, Layout lay)
    {
        layout = lay;
        dictionary = Dict;

        Start = EMPTY;
        End = EMPTY;

        oldStart = EMPTY;
        oldEnd = EMPTY;
    }

    public List<Hex> BreadthFirstSearch()
    {
        Queue frontier = new Queue();
        frontier.Enqueue(Start);
        //wether its been visited
        Hashtable CameFrom = new Hashtable();
        CameFrom[Start] = null;
        Hex current = EMPTY;



        if (!(Start == oldStart && End == oldEnd) && OldPath != null)
        {

            oldStart = Start;
            oldEnd = End;

            int Count = 0;
            while (frontier.Count > 0 || !(Count > 1))
            {
                if (frontier.Count == 0)
                {
                    break;
                }
                current = (Hex)frontier.Dequeue();
                if (dictionary.ContainsKey(current))
                {
                    foreach (Hex next in current.Neighbors())
                    {
                        if (dictionary.ContainsKey(next))
                        {
                            if (!CameFrom.Contains(next) && (dictionary[next].Traversable))
                            {
                                frontier.Enqueue(next);
                                CameFrom[next] = current;
                            }
                        }
                    }
                }

                Count++;
            }

            current = End;
            List<Hex> Path = new List<Hex>();

            while (current != Start)
            {
                Path.Add(current);
                current = (Hex)CameFrom[current];

            }

            if (dictionary.ContainsKey(Start))
            {
                if (dictionary[Start].Traversable)
                {
                    Path.Add(Start);
                }
            }

            OldPath = Path;
            return Path;
        }
        else
        {
            return OldPath;
        }

    }
    
    //private bool clicked = false;

    public IEnumerator pause(float time)
    {
        print(Time.time);
        yield return new WaitForSeconds(time);
        print(Time.time);
    }


    public bool MovePiece(HexTurnManager Turnmanager,HexTileUI TileUI)
    {
        if (selectedPiece != null && Start != End && End != EMPTY)
        {
            var path = Vector3PathFromHexList(points, dictionary);

            if (points.Count > selectedPiece.GetComponent<HexStats>().Range)
            {
                while (points.Count > selectedPiece.GetComponent<HexStats>().Range)
                {
                    Debug.Log("Longer NOW");
                    points.RemoveAt(points.Count - 1);
                    selectedPiece.GetComponent<HexPiece>().Move(Start, points[points.Count - 1], path, dictionary);
                }
            }
            else
            {
                selectedPiece.GetComponent<HexPiece>().Move(Start, TileUI.MousePos, path, dictionary);
            }

            Turnmanager.Activate(selectedPiece);
            selectedPiece = null;
            Start = EMPTY;
            End = EMPTY;
            return true;
        }
        return false;
    }

    public List<Vector3> Vector3PathFromHexList(List<Hex> hexpath, Dictionary<Hex, HexCollection> dictionary)
    {
        List<Vector3> path = new List<Vector3>();

        foreach (var point in hexpath)
        {
            var pos = Hex.HexToVec3(point, layout);
            var mod = new Vector3(0, dictionary[point].Tile.Yextent * 2);

            path.Add(pos + mod);
        }

        return path;
    }

    public Hex oldOtherStart;
    public Hex oldOtherEnd;
    public List<Hex> OldOtherPath = new List<Hex>();

    public List<Hex> BreadthFirstSearch(Hex pathfinderStart, Hex tileUiMousePos)
    {
            Queue frontier = new Queue();
            frontier.Enqueue(pathfinderStart);
            //wether its been visited
            Hashtable CameFrom = new Hashtable();
            CameFrom[pathfinderStart] = null;
            Hex current = EMPTY;



            if (!(pathfinderStart == oldOtherStart && tileUiMousePos == oldOtherEnd) && OldOtherPath != null)
            {

            oldOtherStart = pathfinderStart;
            oldOtherEnd = tileUiMousePos;

                int Count = 0;
                while (frontier.Count > 0 || !(Count > 1))
                {
                    if (frontier.Count == 0)
                    {
                        break;
                    }
                    current = (Hex)frontier.Dequeue();
                    if (dictionary.ContainsKey(current))
                    {
                        foreach (Hex next in current.Neighbors())
                        {
                            if (dictionary.ContainsKey(next))
                            {
                                if (!CameFrom.Contains(next) && (dictionary[next].Traversable))
                                {
                                    frontier.Enqueue(next);
                                    CameFrom[next] = current;
                                }
                            }
                        }
                    }

                    Count++;
                }

                current = tileUiMousePos;
                List<Hex> Path = new List<Hex>();

                while (current != Start)
                {
                    Path.Add(current);
                    current = (Hex)CameFrom[current];

                }

                if (dictionary.ContainsKey(Start))
                {
                    if (dictionary[Start].Traversable)
                    {
                        Path.Add(Start);
                    }
                }

            OldOtherPath = Path;
                return Path;
            }
            else
            {
                return OldOtherPath;
            }

        }
    }

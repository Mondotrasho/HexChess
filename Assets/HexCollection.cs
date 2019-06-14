using System.Collections.Generic;
using UnityEngine;


//All the info a grid position can store
public class HexCollection
{
    public enum Team { None, White, Black };

    public HexCollection(HexTile tile,Hex hex)
    {
        Tile = tile;
        for (int i = 0; i < 6; i++)
        {
            Neighbours[i] = hex.Neighbor(i);
        }

        Traversable = tile.Pathable;
        cost = tile.TravelCost;
    }

    public List<GameObject> gameObjects = null;
    public HexTile Tile;
    public HexPiece Piece;
    public float cost;
    public Hex[] Neighbours = new Hex[6];
    public Team Occupiedteam = Team.None;
    public bool Traversable = true;

    public void SetPieceTeamandPathable(HexPiece p, Team t, bool pathable)
    {
        Piece = p;
        Occupiedteam = t;
        Traversable = pathable;
    }

    public void UnSetPieceTeamandPathable()
    {
        Piece = null;
        Occupiedteam = Team.None;
        Traversable = true;
    }


}

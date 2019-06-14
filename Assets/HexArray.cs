using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class HexArray : MonoBehaviour
{
    public Layout layout = new Layout(Layout.flat, new Point(1.02, 1.02), new Point(0, 0));

    public Dictionary<Hex, HexCollection> ObjectsDictionary = new Dictionary<Hex, HexCollection>();

    public GameObject Map_Tiles, Map_Pieces, UI_Tiles;

    public PiecePasser piecesPasser;
    public TilePasser tilePasser;

    public HexTile GlowBob;

    public float heightmulti = 2;

    public HexPathfinding pathfinding;

    public HexTileUI TileUI;

    public HexTurnManager Turnmanager;
    // Start is called before the first frame update

    void Start()
    {
        Turnmanager.init(ObjectsDictionary,pathfinding);
        //because we are instatiating
        HexTile.DeleteOldTiles(Map_Tiles);
        HexPiece.DeleteOldPieces(Map_Pieces);

        //The Tiles need to be stored in the Dictionary THIS ALSO CREATES THE KEYS
        GenDictionaryAndAddTiles(tilePasser.Pass());

        //the Tiles need to be placed
        HexTile.InstantiateTiles(ObjectsDictionary,layout,Map_Tiles);

        //The Pieces initial locations need to be stored in the Dictionary
        AddPieces(piecesPasser.Pass());

        //the Pieces need to be placed
        HexPiece.InstantiatePieces(ObjectsDictionary,layout,Map_Pieces);

        TileUI.initMouse();
        HexTileUI.InstatiateTileUI(GlowBob);

        pathfinding.Setup(ObjectsDictionary, layout);
    }

    private void Update()
    {
        //update the Hex the mouse is in
        TileUI.updateMousePos(layout);

        //Mouse input set start and end
        TileUI.MouseHexClick(pathfinding,ObjectsDictionary,Turnmanager);

        Turnmanager.UpdateTurnManager(pathfinding);

        if (pathfinding.selectedPiece != null)
        {
            if (Turnmanager.CheckAP(pathfinding.selectedPiece))
            {
                //if end is set aka not Hex(Max,Max) set pathfinding points
                if (pathfinding.End != pathfinding.EMPTY)
                {
                    List<Hex> a = pathfinding.BreadthFirstSearch();
                    a.Reverse();
                    List<Hex> Shortened = new List<Hex>();
                    if (pathfinding.selectedPiece.GetComponent<HexStats>().Range < a.Count)
                    {
                        for (int i = 0; i < pathfinding.selectedPiece.GetComponent<HexStats>().Range; i++)
                        {
                            Shortened.Add(a[i]);
                        }
                    }
                    else
                    {
                        Shortened = a;
                    }

                    pathfinding.points = Shortened;
                }
            }
        }

        if (pathfinding.selectedPiece != null)
        {
            if (Turnmanager.CheckAP(pathfinding.selectedPiece))
            {
                //if you move make sure to delete the Hex line
                if (pathfinding.MovePiece(Turnmanager,TileUI))
                {
                    HexTileUI.DeleteOldTileUI(UI_Tiles);
                }
            }
        }
    }
    
    private void FixedUpdate()
    {
        //draw the mouse hex
        TileUI.DrawMousehex(ObjectsDictionary,Turnmanager);


        if (pathfinding.selectedPiece != null && pathfinding.Start != pathfinding.EMPTY)
        {
            if (Turnmanager.CheckAP(pathfinding.selectedPiece))
            {
                HexTileUI.HighlightLineHexes(ObjectsDictionary, layout, UI_Tiles, GlowBob, pathfinding, TileUI);

            }
        }
    }

    public void GenDictionaryAndAddTiles(List<HexTile> Tiles)
    {
        HexCollection tile = null;

        //bridge
        HexTile B1 = Tiles[0];
        HexTile B2 = Tiles[1];
        HexTile B3 = Tiles[1];
        //ground
        HexTile G1 = Tiles[3];
        HexTile G2 = Tiles[4];
        //Water
        HexTile L1 = Tiles[5];
        //Mountains
        HexTile M1 = Tiles[6];
        HexTile M2 = Tiles[7];
        HexTile M3 = Tiles[8];
        HexTile M4 = Tiles[9];
        HexTile M5 = Tiles[10];
        HexTile M6 = Tiles[11];
        HexTile M7 = Tiles[12];
        HexTile M8 = Tiles[13];
        HexTile M9 = Tiles[14];
        //White Spawn
        HexTile WS1 = Tiles[15];
        HexTile WS2 = Tiles[16];
        HexTile WS3 = Tiles[17];
        HexTile WS4 = Tiles[18];
        HexTile WS5 = Tiles[19];
        //Black Spawn
        HexTile BS1 = Tiles[15];
        HexTile BS2 = Tiles[16];
        HexTile BS3 = Tiles[17];
        HexTile BS4 = Tiles[18];
        HexTile BS5 = Tiles[19];

        //pathableness
        //bridge
        B1.Pathable = true;
        B2.Pathable = true;
        B3.Pathable = true;
        //ground
        G1.Pathable = true;
        G2.Pathable = true;
        //Water
        L1.Pathable = true;
        //Mountains
        M1.Pathable = false;
        M2.Pathable = false;
        M3.Pathable = false;
        M4.Pathable = false;
        M5.Pathable = false;
        M6.Pathable = false;
        M7.Pathable = false;
        M8.Pathable = false;
        M9.Pathable = false;
        //White Spawn
        WS1.Pathable = true;
        WS2.Pathable = true;
        WS3.Pathable = true;
        WS4.Pathable = true;
        WS5.Pathable = true;
        //Black Spawn
        BS1.Pathable = true;
        BS2.Pathable = true;
        BS3.Pathable = true;
        BS4.Pathable = true;
        BS5.Pathable = true;

        int q = -8;
        //LEFT
        for (int r = 15; r <= 16; r++)
        {
            if (r == 15)
            {
                tile = new HexCollection(M8, new Hex(q, r));
            }
            if (r == 16)
            {
                tile = new HexCollection(M9, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 12; r <= 18; r++)
        {
            if (r == 14 || r == 16)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else if (r == 15)
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 9; r <= 20; r++)
        {
            if (r <= 10 || r >= 19)
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 14 || r == 15)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 7; r <= 21; r++)
        {
            if (r <= 9 || r >= 19)
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r >= 13 && r <= 15)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 5; r <= 22; r++)
        {
            if (r <= 7 || r >= 20)
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r >= 13 && r <= 14)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else if ((r == 8 || r == 19))
            {
                tile = new HexCollection(M2, new Hex(q, r));
            }
            else if ((r == 9 || r == 18))
            {
                tile = new HexCollection(M4, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 3; r <= 23; r++)
        {
            if ((r >= 4 && r <= 6) || (r >= 20 && r <= 22))
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 12 || r == 14)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else if (r == 7 || r == 19)
            {
                tile = new HexCollection(M1, new Hex(q, r));
            }
            else if ((r == 8 || r == 18))
            {
                tile = new HexCollection(M3, new Hex(q, r));
            }
            else if ((r == 11 || r == 15))
            {
                tile = new HexCollection(M6, new Hex(q, r));
            }
            else if (r == 3)
            {
                tile = new HexCollection(WS5, new Hex(q, r));
            }
            else if (r == 23)
            {
                tile = new HexCollection(BS5, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 2; r <= 23; r++)
        {
            if ((r >= 4 && r <= 8) || (r >= 17 && r <= 21))
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 12 || r == 13)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else if (r == 10 || r == 15)
            {
                tile = new HexCollection(M5, new Hex(q, r));
            }
            else if ((r == 11 || r == 14))
            {
                tile = new HexCollection(M7, new Hex(q, r));
            }
            else if (r == 2)
            {
                tile = new HexCollection(WS4, new Hex(q, r));
            }
            else if (r == 3)
            {
                tile = new HexCollection(WS5, new Hex(q, r));
            }
            else if (r == 23)
            {
                tile = new HexCollection(BS4, new Hex(q, r));
            }
            else if (r == 22)
            {
                tile = new HexCollection(BS5, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 1; r <= 23; r++)
        {
            if ((r >= 4 && r <= 7) || (r >= 17 && r <= 20))
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 10 || r == 14)
            {
                tile = new HexCollection(B1, new Hex(q, r));
            }
            else if (r == 11 || r == 13)
            {
                tile = new HexCollection(B2, new Hex(q, r));
            }
            else if (r == 12)
            {
                tile = new HexCollection(B3, new Hex(q, r));
            }
            else if (r == 1)
            {
                tile = new HexCollection(WS3, new Hex(q, r));
            }
            else if (r == 2)
            {
                tile = new HexCollection(WS4, new Hex(q, r));
            }
            else if (r == 3)
            {
                tile = new HexCollection(WS5, new Hex(q, r));
            }
            else if (r == 23)
            {
                tile = new HexCollection(BS4, new Hex(q, r));
            }
            else if (r == 22)
            {
                tile = new HexCollection(BS4, new Hex(q, r));
            }
            else if (r == 21)
            {
                tile = new HexCollection(BS5, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        //middle
        for (int r = 0; r <= 23; r++)
        {
            if ((r >= 3 && r <= 7) || (r >= 16 && r <= 20))
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 9 || r == 14)
            {
                tile = new HexCollection(B1, new Hex(q, r));
            }
            else if (r == 10 || r == 13)
            {
                tile = new HexCollection(B2, new Hex(q, r));
            }
            else if (r == 11 || r == 12)
            {
                tile = new HexCollection(B3, new Hex(q, r));
            }
            else if (r == 0)
            {
                tile = new HexCollection(WS1, new Hex(q, r));
            }
            else if (r == 1)
            {
                tile = new HexCollection(WS2, new Hex(q, r));
            }
            else if (r == 2)
            {
                tile = new HexCollection(WS5, new Hex(q, r));
            }
            else if (r == 23)
            {
                tile = new HexCollection(BS1, new Hex(q, r));
            }
            else if (r == 22)
            {
                tile = new HexCollection(BS2, new Hex(q, r));
            }
            else if (r == 21)
            {
                tile = new HexCollection(BS5, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        //RIGHT
        for (int r = 0; r <= 22; r++)
        {
            if ((r >= 3 && r <= 6) || (r >= 16 && r <= 19))
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 9 || r == 13)
            {
                tile = new HexCollection(B1, new Hex(q, r));
            }
            else if (r == 10 || r == 12)
            {
                tile = new HexCollection(B2, new Hex(q, r));
            }
            else if (r == 11)
            {
                tile = new HexCollection(B3, new Hex(q, r));
            }
            else if (r == 0)
            {
                tile = new HexCollection(WS3, new Hex(q, r));
            }
            else if (r == 1)
            {
                tile = new HexCollection(WS4, new Hex(q, r));
            }
            else if (r == 2)
            {
                tile = new HexCollection(WS5, new Hex(q, r));
            }
            else if (r == 22)
            {
                tile = new HexCollection(BS4, new Hex(q, r));
            }
            else if (r == 21)
            {
                tile = new HexCollection(BS4, new Hex(q, r));
            }
            else if (r == 20)
            {
                tile = new HexCollection(BS5, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 0; r <= 21; r++)
        {
            if ((r >= 2 && r <= 6) || (r >= 15 && r <= 19))
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 10 || r == 11)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else if (r == 8 || r == 13)
            {
                tile = new HexCollection(M5, new Hex(q, r));
            }
            else if ((r == 9 || r == 12))
            {
                tile = new HexCollection(M7, new Hex(q, r));
            }
            else if (r == 0)
            {
                tile = new HexCollection(WS4, new Hex(q, r));
            }
            else if (r == 1)
            {
                tile = new HexCollection(WS5, new Hex(q, r));
            }
            else if (r == 21)
            {
                tile = new HexCollection(BS4, new Hex(q, r));
            }
            else if (r == 20)
            {
                tile = new HexCollection(BS5, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 0; r <= 20; r++)
        {
            if ((r >= 1 && r <= 3) || (r >= 17 && r <= 19))
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 9 || r == 11)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else if (r == 4 || r == 16)
            {
                tile = new HexCollection(M1, new Hex(q, r));
            }
            else if ((r == 5 || r == 15))
            {
                tile = new HexCollection(M3, new Hex(q, r));
            }
            else if ((r == 8 || r == 12))
            {
                tile = new HexCollection(M6, new Hex(q, r));
            }
            else if (r == 0)
            {
                tile = new HexCollection(WS5, new Hex(q, r));
            }
            else if (r == 20)
            {
                tile = new HexCollection(BS5, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 1; r <= 18; r++)
        {
            if (r <= 3 || r >= 16)
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r >= 9 && r <= 10)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else if ((r == 4 || r == 15))
            {
                tile = new HexCollection(M2, new Hex(q, r));
            }
            else if ((r == 5 || r == 14))
            {
                tile = new HexCollection(M4, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 2; r <= 16; r++)
        {
            if (r <= 4 || r >= 14)
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r >= 8 && r <= 9)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 3; r <= 14; r++)
        {
            if (r <= 4 || r >= 13)
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else if (r == 8 || r == 9)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 5; r <= 11; r++)
        {
            if (r == 7 || r == 9)
            {
                tile = new HexCollection(L1, new Hex(q, r));
            }
            else if (r == 8)
            {
                tile = new HexCollection(G1, new Hex(q, r));
            }
            else
            {
                tile = new HexCollection(G2, new Hex(q, r));
            }

            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
        q++;
        for (int r = 7; r <= 8; r++)
        {
            if (r == 7)
            {
                tile = new HexCollection(M8, new Hex(q, r));
            }
            if (r == 8)
            {
                tile = new HexCollection(M9, new Hex(q, r));
            }
            ObjectsDictionary.Add(new Hex(q, r), tile);
        }
    }

    private void AddPieces(List<HexPiece> pieces)
    {
        //white
        //Bishop
        ObjectsDictionary[new Hex(-1, 1)].SetPieceTeamandPathable(pieces[0], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(1, 0)].SetPieceTeamandPathable(pieces[0], HexCollection.Team.White, false);
        //Castle
        ObjectsDictionary[new Hex(-2, 2)].SetPieceTeamandPathable(pieces[1], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(2, 0)].SetPieceTeamandPathable(pieces[1], HexCollection.Team.White, false);
        //King
        ObjectsDictionary[new Hex(0, 0)].SetPieceTeamandPathable(pieces[2], HexCollection.Team.White, false);
        //Knight
        ObjectsDictionary[new Hex(1, 1)].SetPieceTeamandPathable(pieces[3], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(-1, 2)].SetPieceTeamandPathable(pieces[3], HexCollection.Team.White, false);
        //Pawn
        ObjectsDictionary[new Hex(-3, 3)].SetPieceTeamandPathable(pieces[4], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(-2, 3)].SetPieceTeamandPathable(pieces[4], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(-1, 3)].SetPieceTeamandPathable(pieces[4], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(3, 0)].SetPieceTeamandPathable(pieces[4], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(2, 1)].SetPieceTeamandPathable(pieces[4], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(1, 2)].SetPieceTeamandPathable(pieces[4], HexCollection.Team.White, false);
        ObjectsDictionary[new Hex(0, 2)].SetPieceTeamandPathable(pieces[4], HexCollection.Team.White, false);
        //Queen
        ObjectsDictionary[new Hex(0, 1)].SetPieceTeamandPathable(pieces[5], HexCollection.Team.White, false);

        //Black
        //Bishop
        ObjectsDictionary[new Hex(-1, 23)].SetPieceTeamandPathable(pieces[6], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(1, 22)].SetPieceTeamandPathable(pieces[6], HexCollection.Team.Black, false);
        //Castle
        ObjectsDictionary[new Hex(-2, 23)].SetPieceTeamandPathable(pieces[7], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(2, 21)].SetPieceTeamandPathable(pieces[7], HexCollection.Team.Black, false);
        //King
        ObjectsDictionary[new Hex(0, 23)].SetPieceTeamandPathable(pieces[8], HexCollection.Team.Black, false);
        //Knight
        ObjectsDictionary[new Hex(1, 21)].SetPieceTeamandPathable(pieces[9], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(-1, 22)].SetPieceTeamandPathable(pieces[9], HexCollection.Team.Black, false);
        //Pawn
        ObjectsDictionary[new Hex(-3, 23)].SetPieceTeamandPathable(pieces[10], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(-2, 22)].SetPieceTeamandPathable(pieces[10], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(-1, 21)].SetPieceTeamandPathable(pieces[10], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(3, 20)].SetPieceTeamandPathable(pieces[10], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(2, 20)].SetPieceTeamandPathable(pieces[10], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(1, 20)].SetPieceTeamandPathable(pieces[10], HexCollection.Team.Black, false);
        ObjectsDictionary[new Hex(0, 21)].SetPieceTeamandPathable(pieces[10], HexCollection.Team.Black, false);

        //Queen
        ObjectsDictionary[new Hex(0, 22)].SetPieceTeamandPathable(pieces[11], HexCollection.Team.Black, false);

    }

    public Vector3 PiecePosMovement(Hex hex)
    {
        var a = Hex.HexToVec3(hex, layout) + new Vector3(0, ObjectsDictionary[hex].Tile.Yextent * 2);

        return a;
    }

    public void fixTraversable(Dictionary<Hex, HexCollection> dictionary)
    {
        foreach (var obj in dictionary)
        {
            if (obj.Value.Occupiedteam != HexCollection.Team.None)
                obj.Value.Traversable = false;
            else
                obj.Value.Traversable = true;

            if (!obj.Value.Tile.Pathable)
            {
                obj.Value.Traversable = false;
            }
        }
    }

    

}

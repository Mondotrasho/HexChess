using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePasser : MonoBehaviour
{
    [Header("White Pieces")]
    [Space(10)]
    public HexPiece WhiteBishop;
    public HexPiece WhiteCastle;
    public HexPiece WhiteKing;
    public HexPiece WhiteKnight;
    public HexPiece WhitePawn;
    public HexPiece WhiteQueen;

    [Space(10)]
    [Header("Black Pieces")]
    [Space(10)]
    public HexPiece BlackBishop;
    public HexPiece BlackCastle;
    public HexPiece BlackKing;
    public HexPiece BlackKnight;
    public HexPiece BlackPawn;
    public HexPiece BlackQueen;

    private List<HexPiece> PassMe = new List<HexPiece>();

    public List<HexPiece> Pass()
    {
        PassMe.Clear();

        PassMe.Add(WhiteBishop);
        PassMe.Add(WhiteCastle);
        PassMe.Add(WhiteKing);
        PassMe.Add(WhiteKnight);
        PassMe.Add(WhitePawn);
        PassMe.Add(WhiteQueen);

        PassMe.Add(BlackBishop);
        PassMe.Add(BlackCastle);
        PassMe.Add(BlackKing);
        PassMe.Add(BlackKnight);
        PassMe.Add(BlackPawn);
        PassMe.Add(BlackQueen);

        foreach (var piece in PassMe)
        {
            piece.init();
        }

        return PassMe;
    }
}

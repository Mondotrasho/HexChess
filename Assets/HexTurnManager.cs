using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class HexTurnManager : MonoBehaviour
{
    public HexCollection.Team ActiveTeam = HexCollection.Team.White;
    public int AP = 10;
    public int AP_Left = 10;
    public List<HexPiece> MovedPieces;
    public List<HexPiece> ActivatedOnce;

    public bool CheckIfMoved(HexPiece ThePiece)
    {
        if (MovedPieces.Contains(ThePiece))
        {
            return true;
        }

        return false;
    }

    public bool Activate(HexPiece piece)
    {
        switch (piece.gameObject.GetComponent<HexStats>().remaining_AP)
        {
            case 0:
                return false;
            case 1:
                piece.gameObject.GetComponent<HexStats>().remaining_AP -= 1;
                return true;
            case 2:
                piece.gameObject.GetComponent<HexStats>().remaining_AP -= 1;
                return true;
            default:
                Debug.Log("Something Weird Happened to the AP");
                return false;
        }
    }

}

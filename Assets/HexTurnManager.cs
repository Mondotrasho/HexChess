using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.WSA;

public class HexTurnManager : MonoBehaviour
{
    public HexCollection.Team ActiveTeam = HexCollection.Team.White;
    public int AP = 10;
    public int AP_Left = 10;

    public TextMeshProUGUI APText;
    public TextMeshProUGUI ActiveTeamText;
    public TextMeshProUGUI ActivePieceText;

    private Dictionary<Hex, HexCollection> dictionary;

    public void init(Dictionary<Hex, HexCollection> d)
    {
        dictionary = d;
    }

    public void SwitchTeam()
    {
        var TilesWithPieces = HexPiece.ReturnAllHexWithPieces(dictionary);

        foreach (var Tile in TilesWithPieces)
        {
            dictionary[Tile].Piece.gameObject.GetComponent<HexStats>().resetAP();
        }

        if (ActiveTeam == HexCollection.Team.White)
        {
            ActiveTeam = HexCollection.Team.Black;
        }
        else
        {
            ActiveTeam = HexCollection.Team.White;
        }
        
        AP_Left = AP;


    }

    public void UpdateTurnManager(HexPathfinding pathfinder)
    {
        APText.text = AP.ToString() + " / " + AP_Left.ToString();
        
        if (pathfinder.selectedPiece != null)
        {
            ActivePieceText.text = pathfinder.selectedPiece.name.ToString();
        }
        else
        {
            ActivePieceText.text = "None";
        }
        if (ActiveTeam == HexCollection.Team.White)
            ActiveTeamText.text = "White";
        if (ActiveTeam == HexCollection.Team.Black)
            ActiveTeamText.text = "Black";
    }

    public bool CheckTotalAP()
    {
        return AP_Left > 0;

    }

    public bool CheckAP(HexPiece ThePiece)
    {
        return ThePiece.gameObject.GetComponent<HexStats>().remaining_AP > 0;
    }

    public bool CheckAP(GameObject ThePieceObj)
    {
        return ThePieceObj.GetComponent<HexStats>().remaining_AP > 0;
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
    public bool Activate(GameObject pieceObj)
    {

            switch (pieceObj.GetComponent<HexStats>().remaining_AP)
            {
                case 0:
                    return false;
                case 1:
                    AP_Left -= 1;
                    pieceObj.GetComponent<HexStats>().remaining_AP -= 1;
                    return true;
                case 2:
                    AP_Left -= 1;
                    pieceObj.GetComponent<HexStats>().remaining_AP -= 1;
                    return true;
                default:
                    Debug.Log("Something Weird Happened to the AP");
                    return false;
            }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoftwareKingdom.Algorithms.Minmax.Tictactoe;
public class TictactoeSquare : MonoBehaviour
{
    //Settings
    public int i;
    public int j;
    // Connections
    Renderer myRenderer;
    public Material emptyMat;
    public Material xMat;
    public Material oMat;   
    // State Variables
    
    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        //InitState();
    }
    void InitConnections(){
        myRenderer = GetComponent<Renderer>();
    }
    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPiece(int pieceType)
    {
        Material targetMat = emptyMat;
        if(pieceType == TicTacToeState.X_VAL)
        {
            targetMat = xMat;
        }else if(pieceType == TicTacToeState.O_VAL)
        {
            targetMat = oMat;
        }
        myRenderer.material = targetMat;
    }
}


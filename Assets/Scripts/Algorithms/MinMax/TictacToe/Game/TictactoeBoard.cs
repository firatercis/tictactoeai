using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoftwareKingdom.Algorithms.Minmax.Tictactoe;

public enum PlayerSide
{
    Random,
    X,
    O
}

public class TictactoeBoard : MonoBehaviour
{
    //Settings
    public int boardSize;
    public PlayerSide userSide;
    // Connections
    public GameObject tilePrefab;
    Grid grid;
    Camera mainCamera;
    MinMaxAlgorithm minmaxAlgorithm;
    TicTacToeEvaluator evaluator;
    // State Variables
    TicTacToeState gameState;
    TictactoeSquare[,] cells;
    PlayerSide cpuSide;
    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        InitState();
    }
    void InitConnections(){
        mainCamera = Camera.main;
        grid = GetComponent<Grid>();   
    }
    void InitState(){
        gameState = new TicTacToeState(boardSize);
        cells = new TictactoeSquare[boardSize, boardSize];
        CreateBoard(boardSize);
        if (userSide == PlayerSide.Random)
        {
            userSide = (PlayerSide)Random.Range((int)PlayerSide.X,(int)PlayerSide.O + 1);
        }
        // Init the CPU player
        minmaxAlgorithm = new MinMaxAlgorithm();
        cpuSide = userSide == PlayerSide.X ? PlayerSide.O : PlayerSide.X;
        evaluator = new TicTacToeEvaluator((int)cpuSide);

        if(cpuSide == PlayerSide.X)
        {
            Invoke(nameof(CPUPlay), 1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckUserClick();
    }

    public void CreateBoard(int size)
    {

        for(int i=0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {

                Vector3 cellPosition = grid.CellToWorld(new Vector3Int(i, j, 0));
                GameObject tileGO = Instantiate(tilePrefab);
                tileGO.transform.position = cellPosition;
                TictactoeSquare tile = tileGO.GetComponent<TictactoeSquare>();
                cells[i, j] = tile;
                tile.i = i;
                tile.j = j;
            }
        }
    }

    void CheckUserClick()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out hit, 100))
            {
                TictactoeSquare hitSquare = hit.collider.GetComponent<TictactoeSquare>();
                if(hitSquare != null)
                {
                    OnUserClick(hitSquare.i, hitSquare.j);
                }
                
            }
        }
    }

    void OnUserClick(int i, int j)
    {
        Debug.Log("Clicked (" + i + "," + j + ")");
        gameState.Play((int)userSide, i, j);
        DisplayCurrentState();
        Invoke(nameof(CPUPlay),1);


    }

    void DisplayCurrentState()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                cells[i, j].SetPiece(gameState.board[i, j]);
                
            }
        }
    }

    void CPUPlay()
    {
        string moveAnnotation = "";
        double val = evaluator.Evaluate(gameState);
        Debug.Log("Current board val " + val);
        minmaxAlgorithm.GetMinMaxValue(gameState, evaluator, boardSize* boardSize, 1, out moveAnnotation, -MinMaxAlgorithm.MAX_VAL);
        string[] parts = moveAnnotation.Split('_');
        Debug.Log(moveAnnotation);
        gameState.Play(System.Convert.ToInt32(parts[0]), System.Convert.ToInt32(parts[1]), System.Convert.ToInt32(parts[2]));
        DisplayCurrentState();
    }

}


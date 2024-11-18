using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.EventSystems;
using Unity.VisualScripting;

public class GridManager : MonoBehaviour
{
    //El arreglo requiere el mismo orden que el enum
    public GameObject[] gridPiecesPrefabs;
    public Vector2Int gridSize;
    public Transform parent;
 
    public GameObject wallPref;
    public GameObject wallDestructiblePref;

    public GridPiece[,] grid;

    private void Awake()
    {
        CreateGrid();
    }

    //Se encarga de crear la grilla
    public void CreateGrid()
    {
        //Inicializo la matriz (arreglo bidimensional) segun el tamaño de la grilla
        grid = new GridPiece[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for(int z = 0; z < gridSize.y; z++)
            {
                //Obtengo posicion en grilla
                Vector2Int gridPos = new Vector2Int(x, z);
                
                //Segun la posicion devuelvo un tipo de pieza
                GridPieceType gridPieceType = GetPieceType(gridPos);
                
                //Ocupo la posicion en grilla y el tipo de pieza para instancia la pieza
                GridPiece newPiece = CreatePiece(gridPieceType, gridPos);
                newPiece.pos = gridPos;
                //Guardo la pieza creada en la matriz
                grid[x,z] = newPiece;
                
            }
        }
    }

    //Se encarga de instanciar la pieza y setearla
    GridPiece CreatePiece(GridPieceType pieceType, Vector2Int gridPos)
    {
        GridPiece piece = null;
        Vector3 position = new Vector3(gridPos.x,-0.5f, gridPos.y);
        GameObject pref = gridPiecesPrefabs[(int)pieceType];

        GameObject pieceObj = Instantiate(pref, position, Quaternion.identity,parent);
      
        
        switch (pieceType)
        {
            case GridPieceType.Empty:
                GridPiece_Empty gridPiece_Empty = pieceObj.GetComponent<GridPiece_Empty>();
                gridPiece_Empty.isEmpty = true;
                gridPiece_Empty.isWalkable = true;
                piece = gridPiece_Empty;
                TextMeshProUGUI newTextMeshPro = gridPiece_Empty.texto;
                if (newTextMeshPro != null)
                {
                    newTextMeshPro.text = (gridPos.x + "," + gridPos.y);
                }
                break;
            case GridPieceType.Wall:
                GridPiece_Wall gridPiece_Wall = pieceObj.GetComponent<GridPiece_Wall>();
                gridPiece_Wall.isWalkable = false;
                gridPiece_Wall.isEmpty = false;
                gridPiece_Wall.isDestructible = false;
                gridPiece_Wall.CreateWall(wallPref);
                piece = gridPiece_Wall;
                break;
            case GridPieceType.DestructibleWall:
                GridPiece_Wall gridPiece_WallDestructible = pieceObj.GetComponent<GridPiece_Wall>();
                gridPiece_WallDestructible.isWalkable = false;
                gridPiece_WallDestructible.isEmpty = false;
                gridPiece_WallDestructible.isDestructible = true;
                gridPiece_WallDestructible.CreateWall(wallDestructiblePref);
                piece = gridPiece_WallDestructible;
                break;
        }

        return piece;   
    }

    //Se encarga de elegir el tipo de pieza segun la posicion
    GridPieceType GetPieceType(Vector2Int pos)
    {
        GridPieceType gridPieceType = GridPieceType.Empty;
        if(pos.x == 0 || pos.x == gridSize.x-1 || pos.y == 0 || pos.y == gridSize.y-1)
        {
            gridPieceType = GridPieceType.Wall;
        }
        else if (pos.x == 1 || pos.x == gridSize.x - 2 || pos.y == 1 || pos.y == gridSize.y - 2)
        {
            gridPieceType = GridPieceType.DestructibleWall;
        }
        if(pos.x == 4 && pos.y == 2 || pos.x == 4 && pos.y == 3 || pos.x == 4 && pos.y == 4 || pos.x == 3 && pos.y == 4)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 4 && pos.y == 7 || pos.x == 3 && pos.y == 7 || pos.x == 3 && pos.y == 8 || pos.x == 3 && pos.y == 9)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 3 && pos.y == 12 || pos.x == 3 && pos.y == 13 || pos.x == 3 && pos.y == 14 || pos.x == 3 && pos.y == 15 || pos.x == 4 && pos.y == 13)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 4 && pos.y == 14 || pos.x == 2 && pos.y == 18 || pos.x == 3 && pos.y == 18 || pos.x == 4 && pos.y == 18 || pos.x == 5 && pos.y == 18)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 5 && pos.y == 19 || pos.x == 5 && pos.y == 20 || pos.x == 4 && pos.y == 20 || pos.x == 3 && pos.y == 22 || pos.x == 4 && pos.y == 22)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 2 && pos.y == 24 || pos.x == 2 && pos.y == 25 || pos.x == 2 && pos.y == 26 || pos.x == 2 && pos.y == 27)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 3 && pos.y == 24)//cuadrado que quiero hacer trigger
        {
            gridPieceType = GridPieceType.DestructibleWall;
        }
        if (pos.x == 4 && pos.y == 24 || pos.x == 4 && pos.y == 25 || pos.x == 4 && pos.y == 26 || pos.x == 4 && pos.y == 27)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 6 && pos.y == 2 || pos.x == 6 && pos.y == 3 || pos.x == 6 && pos.y == 4 || pos.x == 6 && pos.y == 5)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 7 && pos.y == 5 || pos.x == 8 && pos.y == 5 || pos.x == 9 && pos.y == 5 || pos.x == 9 && pos.y == 4 || pos.x == 9 && pos.y == 3 || pos.x == 8 && pos.y == 3)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if(pos.x == 6 && pos.y == 8 || pos.x == 7 && pos.y == 8 || pos.x == 8 && pos.y == 8 || pos.x == 9 && pos.y == 8 || pos.x == 10 && pos.y == 8 || pos.x == 7 && pos.y == 9 || pos.x == 8 && pos.y == 9 || pos.x == 9 && pos.y == 9)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 8 && pos.y == 11 || pos.x == 11 && pos.y == 10 || pos.x == 11 && pos.y == 18 || pos.x == 19 && pos.y == 9 || pos.x == 19 && pos.y == 19)//cuadros solitarios
        {
            gridPieceType = GridPieceType.Wall;
        }
        if(pos.x == 7 && pos.y == 13 || pos.x == 8 && pos.y == 13 || pos.x == 9 && pos.y == 13 || pos.x == 7 && pos.y == 14 || pos.x == 7 && pos.y == 15 || pos.x == 8 && pos.y == 15 || pos.x == 9 && pos.y == 15)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if(pos.x == 9 && pos.y == 14)//cuadrado que quiero trigger
        {
            gridPieceType = GridPieceType.DestructibleWall;
        }
        if(pos.x == 23 && pos.y == 25)//cuadrado que quiero trigger
        {
            gridPieceType = GridPieceType.DestructibleWall;
        }
        if (pos.x == 7 && pos.y == 18 || pos.x == 7 && pos.y == 19 || pos.x == 7 && pos.y == 20 || pos.x == 8 && pos.y == 20 || pos.x == 9 && pos.y == 18 || pos.x == 9 && pos.y == 19 || pos.x == 9 && pos.y == 20 
         || pos.x == 6 && pos.y == 23 || pos.x == 6 && pos.y == 24 || pos.x == 6 && pos.y == 25 || pos.x == 7 && pos.y == 23 || pos.x == 7 && pos.y == 24 || pos.x == 7 && pos.y == 25 || pos.x == 8 && pos.y == 23
         || pos.x == 8 && pos.y == 24 || pos.x == 8 && pos.y == 25)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 12 && pos.y == 4 || pos.x == 12 && pos.y == 5 || pos.x == 13 && pos.y == 3 || pos.x == 13 && pos.y == 4 || pos.x == 13 && pos.y == 5 || pos.x == 13 && pos.y == 6 || pos.x == 13 && pos.y == 7 ||
            pos.x == 14 && pos.y == 4 || pos.x == 14 && pos.y == 7 || pos.x == 12 && pos.y == 12 || pos.x == 12 && pos.y == 13 || pos.x == 12 && pos.y == 14 || pos.x == 12 && pos.y == 15 || pos.x == 12 && pos.y == 16 ||
            pos.x == 11 && pos.y == 20 || pos.x == 11 && pos.y == 21 || pos.x == 12 && pos.y == 20 || pos.x == 13 && pos.y == 20 || pos.x == 14 && pos.y == 20 || pos.x == 10 && pos.y == 23 || pos.x == 11 && pos.y == 23
            || pos.x == 12 && pos.y == 23 || pos.x == 13 && pos.y == 23 || pos.x == 13 && pos.y == 22 || pos.x == 10 && pos.y == 26 || pos.x == 10 && pos.y == 27 || pos.x == 11 && pos.y == 26 || pos.x == 11 && pos.y == 27
            || pos.x == 12 && pos.y == 26 || pos.x == 12 && pos.y == 27 || pos.x == 13 && pos.y == 26 || pos.x == 13 && pos.y == 27)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 13 && pos.y == 9 || pos.x == 14 && pos.y == 9 || pos.x == 15 && pos.y == 9 || pos.x == 16 && pos.y == 9 || pos.x == 17 && pos.y == 9 || pos.x == 14 && pos.y == 13 || pos.x == 14 && pos.y == 14 ||
            pos.x == 15 && pos.y == 11 || pos.x == 15 && pos.y == 12 || pos.x == 15 && pos.y == 13 || pos.x == 15 && pos.y == 14 || pos.x == 15 && pos.y == 15 || pos.x == 15 && pos.y == 16 || pos.x == 16 && pos.y == 13
            || pos.x == 16 && pos.y == 14 || pos.x == 13 && pos.y == 18 || pos.x == 14 && pos.y == 18 || pos.x == 15 && pos.y == 18 || pos.x == 16 && pos.y == 18 || pos.x == 17 && pos.y == 18 || pos.x == 16 && pos.y == 20
            || pos.x == 16 && pos.y == 21 || pos.x == 17 && pos.y == 20 || pos.x == 17 && pos.y == 21 || pos.x == 15 && pos.y == 24 || pos.x == 15 && pos.y == 25 || pos.x == 16 && pos.y == 23 || pos.x == 16 && pos.y == 24
            || pos.x == 16 && pos.y == 25 || pos.x == 17 && pos.y == 24 || pos.x == 17 && pos.y == 25 || pos.x == 18 && pos.y == 11 || pos.x == 18 && pos.y == 12 || pos.x == 18 && pos.y == 13 || pos.x == 18 && pos.y == 14
            || pos.x == 18 && pos.y == 15 || pos.x == 18 && pos.y == 16)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 16 && pos.y == 3 || pos.x == 16 && pos.y == 5 || pos.x == 16 && pos.y == 6 || pos.x == 17 && pos.y == 3 || pos.x == 17 && pos.y == 4 || pos.x == 17 && pos.y == 5 || pos.x == 17 && pos.y == 6 ||
            pos.x == 17 && pos.y == 7 || pos.x == 18 && pos.y == 4 || pos.x == 18 && pos.y == 5 || pos.x == 18 && pos.y == 6 || pos.x == 18 && pos.y == 7 || pos.x == 19 && pos.y == 6 || pos.x == 20 && pos.y == 2 ||
            pos.x == 20 && pos.y == 3 || pos.x == 21 && pos.y == 2 || pos.x == 22 && pos.y == 2 || pos.x == 21 && pos.y == 3 || pos.x == 21 && pos.y == 4 || pos.x == 22 && pos.y == 2 || pos.x == 22 && pos.y == 3 ||
            pos.x == 21 && pos.y == 8 || pos.x == 21 && pos.y == 9 || pos.x == 22 && pos.y == 8 || pos.x == 22 && pos.y == 9 || pos.x == 20 && pos.y == 16 || pos.x == 21 && pos.y == 15 || pos.x == 21 && pos.y == 16 
            || pos.x == 21 && pos.y == 17 || pos.x == 22 && pos.y == 16 || pos.x == 20 && pos.y == 22 || pos.x == 21 && pos.y == 21 || pos.x == 21 && pos.y == 22 || pos.x == 21 && pos.y == 23 || pos.x == 19 && pos.y == 26
            || pos.x == 19 && pos.y == 27 || pos.x == 20 && pos.y == 26 || pos.x == 20 && pos.y == 27)
        {
            gridPieceType = GridPieceType.Wall;
        }
        if (pos.x == 23 && pos.y == 5 || pos.x == 24 && pos.y == 4 || pos.x == 24 && pos.y == 5 || pos.x == 24 && pos.y == 6 || pos.x == 23 && pos.y == 12 || pos.x == 24 && pos.y == 11 || pos.x == 24 && pos.y == 12 ||
           pos.x == 24 && pos.y == 13 || pos.x == 25 && pos.y == 12 || pos.x == 24 && pos.y == 22 || pos.x == 25 && pos.y == 21 || pos.x == 25 && pos.y == 22 || pos.x == 25 && pos.y == 23 || pos.x == 26 && pos.y == 22 
           || pos.x == 22 && pos.y == 26 || pos.x == 22 && pos.y == 27 || pos.x == 23 && pos.y == 26 || pos.x == 23 && pos.y == 27 || pos.x == 24 && pos.y == 26 || pos.x == 24 && pos.y == 27
           || pos.x == 26 && pos.y == 2 || pos.x == 26 && pos.y == 3 || pos.x == 26 && pos.y == 4 || pos.x == 27 && pos.y == 2 || pos.x == 27 && pos.y == 3 || pos.x == 27 && pos.y == 4 || pos.x == 25 && pos.y == 8 ||
           pos.x == 26 && pos.y == 7 || pos.x == 26 && pos.y == 8 || pos.x == 26 && pos.y == 9 || pos.x == 27 && pos.y == 8 || pos.x == 26 && pos.y == 14 || pos.x == 26 && pos.y == 15 || pos.x == 27 && pos.y == 14 ||
           pos.x == 27 && pos.y == 15 || pos.x == 26 && pos.y == 18 || pos.x == 26 && pos.y == 19 || pos.x == 27 && pos.y == 17 || pos.x == 27 && pos.y == 18 || pos.x == 27 && pos.y == 19 || pos.x == 27 && pos.y == 20
           || pos.x == 26 && pos.y == 25 || pos.x == 26 && pos.y == 26 || pos.x == 26 && pos.y == 27 || pos.x == 27 && pos.y == 25 || pos.x == 27 && pos.y == 26 || pos.x == 27 && pos.y == 27) 
        {
            gridPieceType = GridPieceType.Wall;
        }
        //cubos que seran obstaculos o algo en el piso
        if (pos.x == 5 && pos.y == 10 || pos.x == 5 && pos.y == 11 || pos.x == 11 && pos.y == 7 || pos.x == 19 && pos.y == 24
            || pos.x == 21 && pos.y == 6 || pos.x == 20 && pos.y == 12 || pos.x == 20 && pos.y == 19 || pos.x == 20 && pos.y == 20 || pos.x == 21 && pos.y == 12 || pos.x == 23 && pos.y == 18 || pos.x == 23 && pos.y == 19
            || pos.x == 24 && pos.y == 15 || pos.x == 24 && pos.y == 16)
        {
            gridPieceType = GridPieceType.DestructibleWall;
        }
        return gridPieceType;
    }

    public bool IsPieceWalkable(Vector2Int piecePos)
    {
        return grid[piecePos.x, piecePos.y].isWalkable;
    }

    public GridPiece GetGridPiece(Vector2Int piecePos)
    {
        return grid[piecePos.x,piecePos.y];
    }

    public bool IsPosOnArray(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSize.x && pos.y >= 0 && pos.y < gridSize.y;
    }
   
}

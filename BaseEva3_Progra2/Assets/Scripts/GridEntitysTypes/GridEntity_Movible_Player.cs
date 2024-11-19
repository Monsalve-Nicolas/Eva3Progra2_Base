using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity_Movible_Player : GridEntity_Movible
{
    public GridShooter gridShooter;
    public Vector2Int startPos;
    public GameObject linterna;
    bool isTurnOn = true;
    protected override void Awake2()
    {

    }

    private void Start()
    {
        SetPlayerPos(startPos);
    }

    protected override void Update2()
    {
        if (!isMoving)
        {
            MoveInputs();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gridShooter.Shoot(gridPos);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {   
            if (isTurnOn)
            {
                linterna.SetActive(false);
                isTurnOn = false;
            }
            else
            {
                isTurnOn = true;
                linterna.SetActive(true);
            }

        }
        
    }  

    public void SetPlayerPos(Vector2Int pos)
    {
        gridPos = pos;
        gridManager.GetGridPiece(pos).OnEntityEnter(this);
    }

    void MoveInputs()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector2Int dir = Vector2Int.zero;

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }

        if (dir.magnitude != 0)
        {
            transform.forward = new Vector3(dir.x, 0, dir.y);
            Move(dir);
        }
    }

    public override void InteractWhitOtherEntity(GridEntity other)
    {
        other.InteractWhitOtherEntity(this);
    }


    protected override void Die()
    {
        print("PlayerDead");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity_Movible_Ghost : GridEntity_Movible
{
    public GameObject fantasma;
    public Transform[] pos;
    public float velocidad = 1;
    public Vector2Int starGhostPos;

    private void Start()
    {
        SetGhostPos(starGhostPos);
        StartCoroutine(MovimientoP());

    }
    protected override void Awake2()
    {
        
    }
    protected override void Update2()
    {
       
    }
    public void SetGhostPos(Vector2Int pos)
    {
        gridPos = pos;
        gridManager.GetGridPiece(pos).OnEntityEnter(this);
    }
    IEnumerator MovimientoP()
    {
        int i = 1;
        Vector3 newPos = new Vector3(pos[i].position.x, fantasma.transform.position.y, pos[i].position.z);

        while (true)
        {
            while (fantasma.transform.position != newPos)
            {

                fantasma.transform.position = Vector3.MoveTowards(fantasma.transform.position, newPos, velocidad * Time.deltaTime);
                yield return null;
            }
            if (i < 3) i++; else i = 0;
            newPos = new Vector3(pos[i].position.x, fantasma.transform.position.y, pos[i].position.z);
            yield return new WaitForSeconds(5);

        }
    }
    protected override void Die()
    {
        
    }
    public override void InteractWhitOtherEntity(GridEntity other)
    {
        
    }
}

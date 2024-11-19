using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece_Collectable : GridPiece
{
    public float yOffSet;
    GameObject collectable;

    public override void OnEntityEnter(GridEntity gridEntity)
    {
        if(gridEntity.CompareTag("Player"))
        {
            currentGridEntity.InteractWhitOtherEntity(gridEntity);
        }
    }

    public GridEntity CreateWall(GameObject collectablePref)
    {
        Vector3 pos = transform.position;
        pos += Vector3.up * yOffSet;
        collectable = Instantiate(collectablePref, pos, Quaternion.identity, transform);
        return collectable.GetComponent<GridEntity>();
    }

    public override void OnEntityExit()
    {
        Destroy(collectable);
    }
    public override void OnEntityStay()
    {
        
    }

}

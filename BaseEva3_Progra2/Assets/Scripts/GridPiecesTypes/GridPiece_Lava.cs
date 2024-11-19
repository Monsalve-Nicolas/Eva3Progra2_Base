using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece_Lava : GridPiece
{
    public override void OnEntityExit()
    {
        
    }
    public override void OnEntityStay()
    {
        
    }
    public override void OnEntityEnter(GridEntity gridEntity)
    {
        base.OnEntityEnter(gridEntity);
        currentGridEntity.TakeDamage(1f);
    }
}

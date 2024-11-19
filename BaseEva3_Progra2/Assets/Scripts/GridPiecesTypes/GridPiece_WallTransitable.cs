using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece_WallTransitable : GridPiece
{
    public float yOffSet;
    GameObject wall;

    public void CreateWall(GameObject wallPref)
    {
        Vector3 pos = transform.position;
        pos += Vector3.up * yOffSet;
        wall = Instantiate(wallPref, pos, Quaternion.identity, transform);
    }

    public override void OnEntityExit()
    {
        Destroy(wall);
    }

    public override void OnEntityStay()
    {

    }
}

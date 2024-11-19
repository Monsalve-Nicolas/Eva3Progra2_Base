using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : GridEntity
{
    public GameManager gm;

    public override void InteractWhitOtherEntity(GridEntity other)
    {
        if(other.CompareTag("Player"))
        {
            gm.AddPoints();
            Destroy(gameObject);
        }
    }

    protected override void Awake2()
    {

    }

    protected override void Die()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyP : MonoBehaviour
{
    public Transform[] pos;
    private int index;

    private void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if(this.transform.position == pos[index].position)
        {
            if(index < pos.Length)
            {
                index++;
            }
            else if(index == pos.Length - 1)
            {
                index = 0;
            }
        }
    }
}

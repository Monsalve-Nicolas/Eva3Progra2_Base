using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyP : MonoBehaviour
{

    public GameObject fantasma;
    public Transform[] pos;
    public float velocidad = 1;

    private void Start()
    {
        StartCoroutine(MovimientoP());
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
}

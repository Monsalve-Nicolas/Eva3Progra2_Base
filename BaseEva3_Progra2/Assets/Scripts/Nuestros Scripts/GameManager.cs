using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer sprite;
    public AudioSource sonido;
    public Transform ghost;
    public GameObject ghostPrefab;
    public TextMeshProUGUI textTimer,textAddPoints;
    public bool isTimerOn;
    public float count;
    public float timer;
    int min, seg;
    // Start is called before the first frame update
    void Start()
    {
 
        isTimerOn = true;
    }
    private void Update()
    {
        Timer();
    }
    public void StartGame()
    {

    }
    public void EndGame()
    {

    }
    public void Timer()
    {
        if (!isTimerOn)
        {
            return;
        }
        timer -= Time.deltaTime;
        min = (int)(timer / 60f);
        seg = (int)(timer - min * 60f);
        textTimer.text = string.Format("{0:00}:{1:00}", min, seg);
        if(timer <= 60)
        {
            textTimer.color = Color.red;

        }
        else if(timer == 0)
        {
            EndGame();
        }
    }
    public void SpawnerColeccionables()
    {

    }
    public void AddPoints()
    {
        count++;
        textAddPoints.text = count.ToString();
    }
    public bool Caminable()
    {
        return true;
    }
    

}

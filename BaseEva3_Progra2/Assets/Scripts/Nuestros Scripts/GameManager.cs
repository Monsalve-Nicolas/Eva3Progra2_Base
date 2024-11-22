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
    public void WinGame()
    {
        SceneManager.LoadScene(2);
    }
    public void EndGame()
    {

        SceneManager.LoadScene(0);
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

        if(timer <= 0)
        {
            EndGame();
            isTimerOn = false;
        }
        else if (timer <= 60)
        {
            textTimer.color = Color.red;

        }
    }
    public void SpawnerColeccionables()
    {

    }
    public void AddPoints()
    {
        count++;
        textAddPoints.text = count.ToString();
        Debug.Log("Lalala");
        if (count == 5)
        {
            WinGame();
        }
    }
    public bool Caminable()
    {
        return true;
    }
    

}

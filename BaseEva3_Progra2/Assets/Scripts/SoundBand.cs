using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundBand : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clipMusic;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            source.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            source.Pause();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            source.PlayOneShot(clipMusic, 5f);
        }
    }
    

}

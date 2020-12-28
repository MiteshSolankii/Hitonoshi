using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicPlayer;
    public AudioClip[] audioClip;
    public AudioClip finaleClip;
    void Start()
    {
        musicPlayer.clip = audioClip[Random.Range(0, audioClip.Length)];
        musicPlayer.Play();
        Debug.Log("current track is " + musicPlayer.clip.name);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicPlayer.isPlaying)
        {
            ChangeMusic();
        }
    }

    void ChangeMusic()
    {
        musicPlayer.clip = audioClip[Random.Range(0, audioClip.Length)];
        musicPlayer.Play();
        Debug.Log("current track is " + musicPlayer.clip.name);
    }

  public void FinalMusic()
  {
        musicPlayer.Stop();
        musicPlayer.clip = finaleClip;
        musicPlayer.Play();
        Debug.Log("current track is " + musicPlayer.clip.name);

  }
}

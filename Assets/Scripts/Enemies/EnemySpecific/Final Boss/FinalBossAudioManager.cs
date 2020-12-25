using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static FinalBossAudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    void Start() //play on awake audio here
    {


    }
    public void PlaySound(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}

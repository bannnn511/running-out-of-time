using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioScript : MonoBehaviour
{
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.audioSrc = gameObject.AddComponent<AudioSource>();
            s.audioSrc.clip = s.clip;
            s.audioSrc.volume = s.volume;
            s.audioSrc.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    public void PlaySound(string name)
    {
      
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (name == "PlayerWalking")
            s.audioSrc.loop = true;
        s.audioSrc.Play();
        
    }
    public void StopSound(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
      
        s.audioSrc.Stop();

    }
}

using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixer;
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = audioMixer;
            s.source.clip = s.clip;

            s.source.volume = s.volume * 0.25f;
            
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

   public void Play(string soundName)
    {
        Sound s = Array.Find(instance.sounds, sound => sound.soundName == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found.");
            return;
        }
        s.source.Play();
    }
}

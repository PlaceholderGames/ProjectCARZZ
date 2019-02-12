using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOnAwake : MonoBehaviour {

    [Header("Audio setup")]
    public AudioMixerGroup mainMixer;
    public AudioClip[] clips;
    [Space(2, order = 0)]
    [Header("Time Delay")]
    public float timeVal;
    public bool randomTime;
    [Space(2, order = 0)]
    [Header("Volume properties")]
    [Tooltip("If a random volume is desired, press the button and specify both the lowest and highest volumes, otherwise the highest volume is used.")]
    public bool randomVolume;
    public float lowestVol;
    public float highestVol;

    void PlayRandomSound()
    {
        Debug.Log("Collided with an object that makes a noise");
        int randomClip = Random.Range(0, clips.Length);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mainMixer;
        audioSource.clip = clips[randomClip];
        if (randomVolume == true)
        {
            audioSource.volume = Random.Range(lowestVol, highestVol);
        }
        else
        {
            audioSource.volume = highestVol;
        }
        audioSource.Play();
        Debug.Log(audioSource.volume);
    }

    private void Awake()
    {
        if (randomTime != true)
        {
            Invoke("PlayRandomSound", timeVal);
        }
        else
            Invoke("PlayRandomSound", Random.Range(0, timeVal));
    }

}

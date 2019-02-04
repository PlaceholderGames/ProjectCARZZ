using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOnAwake : MonoBehaviour {

    public AudioMixerGroup mainMixer;
    public AudioClip[] clips;
    public float timeVal;
    public bool randomTime;
    void PlayRandomSound()
    {
        Debug.Log("Collided with an object that makes a noise");
        int randomClip = Random.Range(0, clips.Length);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mainMixer;
        audioSource.clip = clips[randomClip];
        audioSource.Play();
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

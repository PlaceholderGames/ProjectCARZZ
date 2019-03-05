using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOnTrigger : MonoBehaviour
{
    [Header("Sound setup")]
    public AudioMixerGroup mainMixer;
    public AudioClip[] clips;
    [Header("Volume setup")]
    public bool randomVolume;
    public int lowestVol;
    public int highestVol;

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
        audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vehicle2" || other.tag == "Vehicle3")
        {
            PlayRandomSound();
        }
    }

}

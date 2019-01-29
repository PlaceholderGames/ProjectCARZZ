using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOnHit : MonoBehaviour
{
    public AudioMixerGroup mainMixer;
    public AudioClip[] clips;

    // Use this for initialization
    void Start()
    {
    }

    void PlayRandomSound()
    {
        Debug.Log("Collided with an object that makes a noise");
        int randomClip = Random.Range(0, clips.Length);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mainMixer;
        audioSource.clip = clips[randomClip];
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Vehicle2" || collision.collider.tag == "Vehicle3")
        {
            PlayRandomSound();
        }
    }

}

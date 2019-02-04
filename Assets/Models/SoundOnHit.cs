using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOnHit : MonoBehaviour
{
    [Header("Audio setup")]
    [Space(2, order = 0)]
    public AudioMixerGroup mainMixer;
    public AudioClip[] clips;
    [Space(5, order = 1)]
    [Header("Volume properties")]
    [Space(2, order = 2)]
    [Tooltip("If a random volume is desired, press the button and specify both the lowest and highest volumes, otherwise the highest volume is used.")]
    public bool randomVolume;
    public int lowestVol = 40;
    public int highestOrDefaultVol = 80;

    void PlayRandomSound()
    {
        Debug.Log("Collided with an object that makes a noise");
        int randomClip = Random.Range(0, clips.Length);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mainMixer;
        audioSource.clip = clips[randomClip];
        if (randomVolume == true)
        {
            audioSource.volume = Random.Range(lowestVol, highestOrDefaultVol);
        }
        else
        {
            audioSource.volume = highestOrDefaultVol;
        }
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

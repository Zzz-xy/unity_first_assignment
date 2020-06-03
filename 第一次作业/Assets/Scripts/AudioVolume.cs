using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioVolume : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer mixer;
    //private AudioSource audio;
    
    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }
    public void turnVolume()
    {
        float i = volumeSlider.value;
        mixer.SetFloat("Master", i);
    }
}

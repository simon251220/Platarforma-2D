using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam = "MyExposedParam";
    public Slider slider;
    public void ChangeValue()
    {
        group.SetFloat(floatParam, slider.value);
    }
}

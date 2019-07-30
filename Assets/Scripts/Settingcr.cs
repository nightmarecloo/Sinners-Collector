using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class Settingcr : MonoBehaviour
{
    public AudioMixer am;
    public GameObject settingsM;
    public bool settingsMenu;

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }

    public void SettingsActvie()
    {
        settingsMenu = settingsMenu ? false : true;
        if (settingsMenu == true) settingsM.SetActive(true);
        if (settingsMenu == false) settingsM.SetActive(false);
    }
}

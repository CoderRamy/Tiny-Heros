using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//this script for game Setting 

public class UISettings : MonoBehaviour {

   public AudioMixer MusicMixer;
   public AudioMixer SoundMixer;
    // public Slider QualitySlider;
   public Toggle MusicToggle;
   public GameObject MusicToggle_on;
   public GameObject MusicToggle_off;

   public Toggle SoundToggle;
   public GameObject SoundToggle_on;
   public GameObject SoundToggle_off;

   public Slider MusicSlider;
   public Slider SoundSlider;

    //check the previous setting at start game
    private void Start()
    {
        #region Audio Settings

        MusicMixer.SetFloat("Volume", SettingProperties.MusicVolume);
        SoundMixer.SetFloat("Volume", SettingProperties.SoundVolume);
        MusicSlider.value = SettingProperties.MusicVolume;
        SoundSlider.value = SettingProperties.SoundVolume;

        if (SettingProperties.Music == 1)
        {
            On("Music");
            MusicMixer.SetFloat("Volume", SettingProperties.MusicVolume);
            MusicToggle.isOn = true;
        }
        else if (SettingProperties.Music == 0)
        {
            Off("Music");
            MusicMixer.SetFloat("Volume", -80);
            MusicToggle.isOn = false;
        }

        if (SettingProperties.Sound == 1)
        {
            On("Sound");
            SoundMixer.SetFloat("Volume", SettingProperties.SoundVolume);
            SoundToggle.isOn = true;
        }
        else if (SettingProperties.Sound == 0)
        {
            Off("Sound");
            SoundMixer.SetFloat("Volume", -80);
            SoundToggle.isOn = false;
        }

        #endregion

        #region Quality Settings
        //QualitySettings.SetQualityLevel(SettingProperties.Quality); 
        //QualitySlider.value = QualitySettings.GetQualityLevel();
        #endregion;
    }


    //public void ChangeQuality(float Quality)
    //{
    //    QualitySettings.SetQualityLevel((int)Quality);
    //    SettingProperties.Quality = (int)Quality;
    //}

    public void SoundChange(bool Sound)
    {
        Debug.Log(Sound);
        if (Sound)
        {
            SoundMixer.SetFloat("Volume", SoundSlider.value);
            SettingProperties.Sound = 1;
        }
        else
        {
            SoundMixer.SetFloat("Volume", -80);
            SettingProperties.Sound = 0;
        }
    }

    public void MusicChange(bool Music)
    {
        Debug.Log(Music);
        if (Music)
        {

            MusicMixer.SetFloat("Volume", MusicSlider.value);
           
            SettingProperties.Music = 1;
        }
        else
        {
            MusicMixer.SetFloat("Volume", -80);
            SettingProperties.Music = 0;
        }

    }

    public void SoundVolume(float Sound)
    {
        if(Sound == -40)
        {
            SoundMixer.SetFloat("Volume", -80);
            SettingProperties.SoundVolume = -80;
        }
        else
        {
            if (SoundToggle.isOn)
            {
                SoundMixer.SetFloat("Volume", Sound);
            }

            SettingProperties.SoundVolume = Sound;
        } 
    }

    public void MusicVolume(float Music)
    {
        Debug.Log(Music);

        if (Music == -40)
        {
            MusicMixer.SetFloat("Volume", -80);
            SettingProperties.MusicVolume = -80;
        }
        else
        {
            if (MusicToggle.isOn)
            {
                MusicMixer.SetFloat("Volume", Music);
            }

            SettingProperties.MusicVolume = Music;
        }
    }

    void Off(string toggle)
    {
        if (toggle == "Music")
        {
            MusicToggle_on.SetActive(false);
            MusicToggle_off.SetActive(true);
        }
        else if (toggle == "Sound")
        {
            SoundToggle_on.SetActive(false);
            SoundToggle_off.SetActive(true);
        }
    }
    void On(string toggle)
    {
        if (toggle == "Music")
        {
            MusicToggle_on.SetActive(true);
            MusicToggle_off.SetActive(false);
        }
        else if (toggle == "Sound")
        {
            SoundToggle_on.SetActive(true);
            SoundToggle_off.SetActive(false);
        }
    }

}

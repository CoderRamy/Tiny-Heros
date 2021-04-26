using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script for game Setting Properties

public class SettingProperties : MonoBehaviour
{

    private static int music;
    private static int sound;
    private static int quality;

    private static float soundVolume;
    private static float musicVolume;


    public static int Music
    {
        get
        {
            music = PlayerPrefs.GetInt("Music",1);
            return music;
        }
        set
        {
            music = value;
            PlayerPrefs.SetInt("Music", music);
        }
    }
    public static int Sound
    {
        get
        {
            sound = PlayerPrefs.GetInt("Sound", 1);
            return sound;
        }
        set
        {
            sound = value;
            PlayerPrefs.SetInt("Sound", sound);
        }
    }
    public static float SoundVolume
    {
        get
        {
            soundVolume = PlayerPrefs.GetFloat("SoundVolume",1);
            return soundVolume;
        }
        set
        {
            soundVolume = value;
            PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        }
    }
    public static float MusicVolume
    {
        get
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
            return musicVolume;
        }
        set
        {
            musicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }
    }
    public static int Quality
    {
        get
        {
            quality = PlayerPrefs.GetInt("Quality", 3);
            return quality;
        }
        set
        {
            quality = value;
            PlayerPrefs.SetInt("Quality", quality);
        }
    }
}

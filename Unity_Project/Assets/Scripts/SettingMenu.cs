using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    //to have access to the main audio Mixer : 
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public Slider musicSlider;
    public Slider soundSlider;

    public void Start()
    {

        
        //gather the resolutions possible on the User's screen : 
        //use sort of sql request to avoid duplicates thanks to the Distinct(): 
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        //first empty the list of options : 
        resolutionDropdown.ClearOptions();
        //then add the new options :
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            //to select the current User's resolution : 
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        //add the option to the dropDown 
        resolutionDropdown.AddOptions(options);
        //to automatically select the current resolution of the User's Screen : 
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();//refresh the selection
        /*N.B : to test the resolution go on the game screen in Unity and click on the dropdown menu next Display 1 and add a resolution wished with the + 
         * (check then if this res is selected on the dropdown menu in game)*/

        //to repair the bug of the full screen : 
        Screen.fullScreen = true;


        //to load the current value of the mixers volume to the sliders : 
        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

        audioMixer.GetFloat("Sound", out float soundValueForSlider);
        soundSlider.value = soundValueForSlider;
    }


    public void SetMusicVolume(float volumeMusic)
    {
        //to set the audio mixer volume (rename in Unity by : "Volume") 
        audioMixer.SetFloat("Music", volumeMusic);
     
    }

    public void SetSoundVolume(float volumeSound)
    {
        //to set the audio mixer volume (rename in Unity by : "Volume") 
        audioMixer.SetFloat("Sound", volumeSound);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}

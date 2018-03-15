using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {

    public Dropdown resolutionsDropdown;//список разрешений экрана
    Resolution[] resolutions;
    public Dropdown qualityDropdown;//список качество графики
    public AudioMixer audioMixer;//миксер главного звука



    void Start()
    {
        //разрешение экрана
        resolutions = Screen.resolutions;
        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();      
        int currenrResolutionValue = 0;
        string isOption = "";
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (isOption == option) continue;
            isOption = option; 
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currenrResolutionValue = i;
            }
        }
       
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currenrResolutionValue;
        resolutionsDropdown.RefreshShownValue();
        //конец

        //список качество графики
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        //конец
    }
//Меню настроек

        public void SetResolution(int resotionIndex)
    {
        Resolution resolution = resolutions[resotionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    public void SetSound(float sound)
    {
        audioMixer.SetFloat("sound", sound);
    }
  
    public void SetQualityLevel()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void ShowSettings(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void HideSettings(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void FullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
 //Конец

 //Главное меню
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
//Конец

}

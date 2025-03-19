using JetBrains.Annotations;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropDown;

    private Resolution[] res;
    void Start()
    {
        resolutionDropDown.ClearOptions();
        Resolution [] resolutions = Screen.resolutions;
        res = resolutions.Distinct().ToArray();
        int currentResolutionIndex = 0;
        string[] stringResolution = new string[res.Length];
        for (int i = 0; i < res.Length; i++)
        {
            stringResolution[i] = res[i].width.ToString() + "x" + res[i].height.ToString();
            if (res[i].width == Screen.currentResolution.width && res[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropDown.AddOptions(stringResolution.ToList());
        //Screen.SetResolution(res[res.Length-1].width, res[res.Length-1].height,true);
    }

    public void SetRes()
    {
        Screen.SetResolution(res[resolutionDropDown.value].width, res[resolutionDropDown.value].height, true);
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionSettingPreference", resolutionDropDown.value);
    }
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("ResolutionSettingPreference"))
            resolutionDropDown.value = PlayerPrefs.GetInt("ResolutionSettingPreference");
        else 
            resolutionDropDown.value = currentResolutionIndex;
    }
}

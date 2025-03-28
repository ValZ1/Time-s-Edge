using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropDown;

    private Resolution[] _resolutions;
    private Resolution[] _res;
    void Start()
    {
        resolutionDropDown.ClearOptions();
        _resolutions = Screen.resolutions;
        _res = _resolutions.Distinct(new ResolutionComparer()).ToArray();
        int currentResolutionIndex = 0;
        string[] stringResolution = new string[_res.Length];

        for (int i = 0; i < _res.Length; i++)
        {
            stringResolution[i] = $"{_res[i].width} x {_res[i].height}";
            if (_res[i].width == Screen.currentResolution.width && _res[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropDown.AddOptions(stringResolution.ToList());
        //Screen.SetResolution(res[res.Length-1].width, res[res.Length-1].height,true);
        resolutionDropDown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    public void SetRes()
    {
        Screen.SetResolution(_res[resolutionDropDown.value].width, _res[resolutionDropDown.value].height, true);
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
    private class ResolutionComparer : IEqualityComparer<Resolution>
    {
        public bool Equals(Resolution x, Resolution y)
        {
            return x.width == y.width && x.height == y.height;
        }

        public int GetHashCode(Resolution obj)
        {
            return obj.width.GetHashCode() ^ obj.height.GetHashCode();
        }
    }
}

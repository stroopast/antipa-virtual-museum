using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Button applyBtn;
    [SerializeField] private Button backBtn;

    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider brightnessSlider;

    [SerializeField] private GameObject startUI;

    private Resolution[] resolutions;
    private int currentResolutionIndex;

    private void Start()
    {
        LoadResolutions();

        fullScreenToggle.isOn = Screen.fullScreen;
    }

    private void OnEnable()
    {
        if (SettingsManager.Instance != null)
        {
            brightnessSlider.value = SettingsManager.Instance.GetBrightness();
        }
    }

    private void LoadResolutions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution res = resolutions[i];
            string option = res.width + " x " + res.height;

            if (!options.Contains(option))
            {
                options.Add(option);

                if (res.width == Screen.currentResolution.width &&
                    res.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Awake()
    {
        applyBtn.onClick.AddListener(ApplySettings);

        backBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            startUI.gameObject.SetActive(true);
        });

        soundSlider.onValueChanged.AddListener(SetVolume);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
    }

    private void ApplyResolution()
    {
        string[] selected = resolutionDropdown.options[resolutionDropdown.value].text.Split('x');
        int width = int.Parse(selected[0].Trim());
        int height = int.Parse(selected[1].Trim());

        Screen.SetResolution(width, height, Screen.fullScreen);
        Debug.Log($"Resolution changed to: {width}x{height}");
    }

    private void ApplySettings()
    {
        ApplyResolution();
        Screen.fullScreen = fullScreenToggle.isOn;
    }

    private void SetVolume(float volume)
    {
        SettingsManager.Instance.SetVolume(volume);
    }

    private void SetBrightness(float value)
    {
        SettingsManager.Instance.SetBrightness(value);
    }
}

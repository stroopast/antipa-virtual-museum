using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    private float volume = 1f;
    private float brightness = 1f;

    private GameObject overlayObject;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetVolume(float v)
    {
        volume = v;
        AudioListener.volume = volume;
    }

    public void SetBrightness(float b)
    {
        brightness = b;
        ApplyBrightnessToScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioListener.volume = volume;
        ApplyBrightnessToScene();
    }

    private void ApplyBrightnessToScene()
    {
        if (overlayObject != null) Destroy(overlayObject);

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null) return;

        overlayObject = new GameObject("BrightnessOverlay");
        overlayObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = overlayObject.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        Image img = overlayObject.AddComponent<Image>();
        img.color = new Color(0, 0, 0, 1f - brightness);
        img.raycastTarget = false;

    }

    public float GetBrightness()
    {
        return brightness;
    }
}

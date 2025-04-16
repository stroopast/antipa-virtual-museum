using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Security.Cryptography;
using System;

[System.Serializable]
public class TophyContent
{
    public Image image;
    public TextMeshProUGUI achievementName;
}

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    public List<AchievementData> allAchievements;
    public List<TophyContent> achievementsIcons;
    public Dictionary<string, bool> unlockedAchievements = new();

    public GameObject AchievementsMenu;
    public GameObject WinTrophyMenu;
    public Image winTrophyIcon;
    public TextMeshProUGUI winTrophyName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitAchievements();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        HandleInput();

        for (var iconIndex = 0; iconIndex < achievementsIcons.Count; iconIndex++)
        {
            achievementsIcons[iconIndex].image.sprite = allAchievements[iconIndex].trophyIcon;

            if (unlockedAchievements[allAchievements[iconIndex].animalName] == false)
            {

                achievementsIcons[iconIndex].image.color = Color.gray;
            }
            else
            {
                achievementsIcons[iconIndex].image.color = Color.white;
                achievementsIcons[iconIndex].achievementName.text = allAchievements[iconIndex].achievementName;
            }
        }
    }

    void InitAchievements()
    {
        foreach (var achievement in allAchievements)
        {
            unlockedAchievements[achievement.animalName] = false;
        }
    }

    public void UnlockAchievement(string animalName)
    {
        if (unlockedAchievements.ContainsKey(animalName))
        {
            unlockedAchievements[animalName] = true;
            WinTrophyMenu.SetActive(true);
            foreach (var achievement in allAchievements)
            {
                if(achievement.animalName == animalName)
                {
                    winTrophyIcon.sprite = achievement.trophyIcon;
                    winTrophyName.text = achievement.achievementName;
                    break;
                }
            }
            Invoke(nameof(ExitWinTrophyMenu), 5f);
            Debug.Log($"Achievement unlocked: {animalName}");
        }
    }

    public void OpenAchievementsInventory()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AchievementsMenu.gameObject.SetActive(true);
    }

    public void ExitWinTrophyMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        WinTrophyMenu.SetActive(false);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(AchievementsMenu.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                AchievementsMenu.gameObject.SetActive(false);
            }
        }
    }
}

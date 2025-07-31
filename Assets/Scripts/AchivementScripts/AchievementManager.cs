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

    public GameObject WinTrophyMenu;
    public Image winTrophyIcon;
    public TextMeshProUGUI winTrophyName;

    private int countUnlockedAchievements = 0;

    private void Awake()
    {
        Instance = this;
        InitAchievements();
    }

    private void Update()
    {
        for (var iconIndex = 0; iconIndex < achievementsIcons.Count; iconIndex++)
        {
            achievementsIcons[iconIndex].image.sprite = allAchievements[iconIndex].trophyIcon;

            if (unlockedAchievements[allAchievements[iconIndex].animalName] == false)
            {

                achievementsIcons[iconIndex].image.color = Color.gray;
                achievementsIcons[iconIndex].achievementName.text = "";
            }
            else
            {
                achievementsIcons[iconIndex].image.color = Color.white;
                achievementsIcons[iconIndex].achievementName.text = allAchievements[iconIndex].achievementName;
            }
        }

    }

    public void AddAchievement()
    {
        countUnlockedAchievements++;
    }

    public int GetAchievementCount()
    {
        return countUnlockedAchievements;
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
            AddAchievement();
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
        }
    }

    public void ExitWinTrophyMenu()
    {
        HelperFunctions.LockCursor();
        WinTrophyMenu.SetActive(false);
    }

    public List<string> GetUnlockedAchievements()
    {
        List<string> list = new();
        foreach (var trophy in unlockedAchievements)
        {
            if (trophy.Value)
            {
                list.Add(trophy.Key);
            }
        }
        return list;
    }

    public void SetUnlockedAchievements(List<string> list)
    {
        foreach (var achievement in allAchievements)
        {
            unlockedAchievements[achievement.animalName] = list.Contains(achievement.animalName);
        }
    }

}

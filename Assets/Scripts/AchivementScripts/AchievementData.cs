using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Achievement")]
public class AchievementData : ScriptableObject
{
    public string animalName;
    public string achievementName;
    public Sprite trophyIcon;
}

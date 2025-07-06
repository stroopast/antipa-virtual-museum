using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewMinigameData", menuName = "Minigame/Minigame Data")]
public class MinigameData : ScriptableObject
{
    [System.Serializable]
    public class AnimalTrackQuestion
    {
        public Sprite animalTracks;
        public string correctAnswer;
        public List<string> answers;
    }

    public List<AnimalTrackQuestion> questions = new List<AnimalTrackQuestion>();
}


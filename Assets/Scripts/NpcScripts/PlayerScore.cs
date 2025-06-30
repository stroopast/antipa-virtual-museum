using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    int quizScore = 0;

    public static PlayerScore Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void UpdateScore(int score)
    {
        quizScore += score;
    }

    public int GetPlayerScore()
    {
        return quizScore;
    }
}

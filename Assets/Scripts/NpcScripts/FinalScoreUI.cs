using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FinalScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = $"Felicitări ai terminat testele cu un punctaj de {PlayerScore.Instance.GetPlayerScore()} puncte. Vorbește cu ghidul pentru a vedea clasamentul!";
    }

}

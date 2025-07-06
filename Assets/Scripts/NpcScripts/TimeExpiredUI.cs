using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeExpiredUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = $"Din păcate timpul a expirat, dar ai terminat testele cu un punctaj de {PlayerScore.Instance.GetPlayerScore()} puncte. Vorbește cu ghidul pentru a vedea clasamentul!";
    }
}

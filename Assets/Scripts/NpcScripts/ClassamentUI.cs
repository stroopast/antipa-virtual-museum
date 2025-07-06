using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class ClassamentUI : NetworkBehaviour
{
    private List<(string playerName, int score)> scoreList = new List<(string, int)>();
    public GameObject buttonTemplate;
    public Transform container;

    private void Update()
    {

    }

    private void OnEnable()
    {
        if (GameModeManager.Instance.GetGameMode() == 1)
        {
            PopulateLeaderboard();
        }
    }

    public void PopulateLeaderboard()
    {
        // Clear previous entries except the template itself
        foreach (Transform child in container)
        {
            if (child != buttonTemplate.transform)
                Destroy(child.gameObject);
        }

        scoreList = MultiplayerManager.Instance.GetPlayersScore();

        int rank = 1;
        foreach (var (playerName, score) in scoreList)
        {
            GameObject buttonGO = Instantiate(buttonTemplate, container);
            buttonGO.SetActive(true);

            TMP_Text text = buttonGO.GetComponentInChildren<TMP_Text>();
            text.text = $"{rank}. {playerName} - {score} puncte";

            rank++;
        }
    }
}

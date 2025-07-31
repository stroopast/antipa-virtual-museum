using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject guidedTourUI;
    [SerializeField] private GameObject classamentUI;
    [SerializeField] private Button startTourBtn;
    [SerializeField] private Button watchClassamentBtn;

    [SerializeField] private Button unlockFinalTrophyBtn;
    [SerializeField] private TextMeshProUGUI finalTrophyText;


    private void Update()
    {
        if(GameModeManager.Instance.GetGameMode() == 0)
        {
            watchClassamentBtn.gameObject.SetActive(false);
        }

        if(AchievementManager.Instance.GetAchievementCount() == 1)
        {
            unlockFinalTrophyBtn.gameObject.SetActive(true);
            finalTrophyText.text = "Felicitări! Ai reușit sa strângi toate trofeele din joc, acum esti un savant!";
        }
        else
        {
            unlockFinalTrophyBtn.gameObject.SetActive(false);
            finalTrophyText.text = "Genial! Ai răspuns corect la toate întrebările și ai câștigat un trofeu!";
        }
    }

    private void Awake()
    {
        startTourBtn.onClick.AddListener(() =>
        {
            guidedTourUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });

        watchClassamentBtn.onClick.AddListener(() =>
        {
            classamentUI.SetActive(true);
            gameObject.SetActive(false);
        });

        unlockFinalTrophyBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            AchievementManager.Instance.UnlockAchievement("Final");
        });
    }
}

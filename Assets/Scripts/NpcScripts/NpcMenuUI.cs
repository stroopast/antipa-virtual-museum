using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject guidedTourUI;
    [SerializeField] private GameObject classamentUI;
    [SerializeField] private Button startTourBtn;
    [SerializeField] private Button watchClassamentBtn;

    private void Update()
    {
        if(GameModeManager.Instance.GetGameMode() == 0)
        {
            watchClassamentBtn.gameObject.SetActive(false);
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
    }
}

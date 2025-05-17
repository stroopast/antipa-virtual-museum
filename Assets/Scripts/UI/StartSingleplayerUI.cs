using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSingleplayerUI : MonoBehaviour
{
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button loadGameBtn;
    [SerializeField] private Button backBtn;

    [SerializeField] private GameObject singleplayerNewGameUI;
    [SerializeField] private GameObject singleplayerLoadUI;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject character;

    private void Awake()
    {
        newGameBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            singleplayerNewGameUI.gameObject.SetActive(true);
            character.gameObject.SetActive(true);
        });

        loadGameBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            singleplayerLoadUI.gameObject.SetActive(true);

        });

        backBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            startUI.gameObject.SetActive(true);
        });
    }
}

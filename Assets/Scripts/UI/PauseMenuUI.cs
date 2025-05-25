using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject saveMenuUI;
    [SerializeField] private GameObject loadMenuUI;

    [SerializeField] private Button saveMenuBtn;
    [SerializeField] private Button loadMenuBtn;
    [SerializeField] private Button resumeGameBtn;
    [SerializeField] private Button exitGameBtn;
    [SerializeField] private Button backToMainMenuBtn;

    [SerializeField] private TextMeshProUGUI playerNameText;

    private void Awake()
    {
        saveMenuBtn.onClick.AddListener(() =>
        {
            Hide();
            saveMenuUI.gameObject.SetActive(true);
        });

        loadMenuBtn.onClick.AddListener(() =>
        {
            Hide();
            loadMenuUI.gameObject.SetActive(true);
        });

        resumeGameBtn.onClick.AddListener(() =>
        {
            Hide();
            HelperFunctions.LockCursor();
        });

        exitGameBtn.onClick.AddListener(() =>
        {
            Debug.Log("Exiting game...");
            Application.Quit();
        });

        backToMainMenuBtn.onClick.AddListener(() =>
        {
            GameObject player = GameObject.FindWithTag("Player");
            Destroy(AchievementManager.Instance.gameObject);
            Destroy(SaveManager.Instance.gameObject);
            Destroy(player);
            SceneManager.LoadScene("InitialScene");
        });
    }

    private void Start()
    {
        Hide();
        ModifyResumeButtonsBasedOnGameMode();
    }

    private void ModifyResumeButtonsBasedOnGameMode()
    {
        if (GameModeManager.Instance.GetGameMode() == 1)
        {
            saveMenuBtn.gameObject.SetActive(false);
            loadMenuBtn.gameObject.SetActive(false);

            RectTransform resumeBtnTransform = resumeGameBtn.GetComponent<RectTransform>();
            Vector2 resumeBtnNewPos = resumeBtnTransform.anchoredPosition;
            resumeBtnNewPos.y = 150;
            resumeBtnTransform.anchoredPosition = resumeBtnNewPos;

            RectTransform exitBtnTransform = exitGameBtn.GetComponent<RectTransform>();
            Vector2 exitBtnNewPos = exitBtnTransform.anchoredPosition;
            exitBtnNewPos.y = 0;
            exitBtnTransform.anchoredPosition = exitBtnNewPos;

            backToMainMenuBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Deconectează-te";
            RectTransform backToMainMenuBtnTransform = backToMainMenuBtn.GetComponent<RectTransform>();
            Vector2 backToMainMenuBtnNewPos = backToMainMenuBtnTransform.anchoredPosition;
            backToMainMenuBtnNewPos.y = -150;
            backToMainMenuBtnTransform.anchoredPosition = backToMainMenuBtnNewPos;
        }
        else
        {
            return;
        }
    }

    private void UpdatePlayerNameText()
    {
        if (GameModeManager.Instance.GetGameMode() == 1)
        {
            playerNameText.text = MultiplayerManager.Instance.GetPlayerName();
        }
        else if (GameModeManager.Instance.GetGameMode() == 0)
        {
            playerNameText.text = GameModeManager.Instance.GetPlayerName();
        }
    }

    private void OnEnable()
    {
        UpdatePlayerNameText();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

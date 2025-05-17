using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleplayerNewGameUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInputField;

    [SerializeField] private Button selectFemaleBtn;
    [SerializeField] private Button selectMaleBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button startGameBtn;

    [SerializeField] private GameObject startSinglePlayerUI;
    [SerializeField] private GameObject character;

    private void Awake()
    {
        selectFemaleBtn.onClick.AddListener(() =>
        {
            character.GetComponent<CharacterSelectSinglePlayer>().SetCharacterToFemale();
            GameModeManager.Instance.SetPlayerGender("female");
        });

        selectMaleBtn.onClick.AddListener(() =>
        {
            character.GetComponent<CharacterSelectSinglePlayer>().SetCharacterToMale();
            GameModeManager.Instance.SetPlayerGender("male");
        });

        startGameBtn.onClick.AddListener(() =>
        {
            GameModeManager.Instance.SetPlayerName(playerNameInputField.text);
            SceneManager.LoadScene("GameScene");
        });

        backBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            character.SetActive(false);
            startSinglePlayerUI.gameObject.SetActive(true);
            playerNameInputField.text = "Nume Jucător​";
        });
    }
}

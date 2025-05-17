using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleplayerLoadUI : MonoBehaviour
{
    [SerializeField] private Button loadSlot1Btn;
    [SerializeField] private Button loadSlot2Btn;
    [SerializeField] private Button loadSlot3Btn;
    [SerializeField] private Button removeLoadFromSlot1Btn;
    [SerializeField] private Button removeLoadFromSlot2Btn;
    [SerializeField] private Button removeLoadFromSlot3Btn;
    [SerializeField] private Button backBtn;

    [SerializeField] private GameObject singleplayerNewGameUI;
    [SerializeField] private GameObject deleteSaveUI;

    private void Awake()
    {
        loadSlot1Btn.onClick.AddListener(() =>
        {
            LoadFromSlot(1);
        });

        loadSlot2Btn.onClick.AddListener(() =>
        {
            LoadFromSlot(2);
        });

        loadSlot3Btn.onClick.AddListener(() =>
        {
            LoadFromSlot(3);
        });

        removeLoadFromSlot1Btn.onClick.AddListener(() =>
        {

            OpenDeleteSaveUI(1);
        });

        removeLoadFromSlot2Btn.onClick.AddListener(() =>
        {
            OpenDeleteSaveUI(2);
        });

        removeLoadFromSlot3Btn.onClick.AddListener(() =>
        {
            OpenDeleteSaveUI(3);
        });

        backBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            singleplayerNewGameUI.gameObject.SetActive(true);

        });
    }

    private void LoadFromSlot(int slot)
    {
        if (File.Exists(SaveManager.Instance.GetSlotPath(slot)))
        {
            PlayerPrefs.SetInt("SlotToLoad", slot);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.Log("Nu exista salvare pe acest slot!");
        }
    }

    private void OpenDeleteSaveUI(short slot)
    {
        if (!File.Exists(SaveManager.Instance.GetSlotPath(slot))) return;

        gameObject.SetActive(false);
        deleteSaveUI.gameObject.SetActive(true);
        deleteSaveUI.GetComponent<DeleteSaveUI>().SetCurrentSlot(slot);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenuUI : MonoBehaviour
{
    [SerializeField] private Button saveSlot1Btn;
    [SerializeField] private Button saveSlot2Btn;
    [SerializeField] private Button saveSlot3Btn;

    [SerializeField] private GameObject overwriteSaveUI;
    [SerializeField] private GameObject saveLoadManager;

    private void Awake()
    {
        saveSlot1Btn.onClick.AddListener(() =>
        {
            CheckSaveOverWrite(1);
        });

        saveSlot2Btn.onClick.AddListener(() =>
        {
            CheckSaveOverWrite(2);
        });

        saveSlot3Btn.onClick.AddListener(() =>
        {
            CheckSaveOverWrite(3);
        });
    }

    private void Start()
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void CheckSaveOverWrite(short slot)
    {
        string path = SaveManager.Instance.GetSlotPath(slot);
        if (File.Exists(path))
        {
            Hide();
            overwriteSaveUI.gameObject.SetActive(true);
            overwriteSaveUI.GetComponent<OverwriteSaveUI>().SetCurrentSlot(slot);
        }
        else
        {
            Hide();
            HelperFunctions.LockCursor();
            saveLoadManager.GetComponent<SaveLoadManager>().SaveToSlot(slot);
        }
    }
}

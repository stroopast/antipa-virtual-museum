using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverwriteSaveUI : MonoBehaviour
{
    private short currentSlot;

    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    [SerializeField] private GameObject saveMenuUI;
    [SerializeField] private GameObject saveLoadManager;

    private void Awake()
    {
        yesBtn.onClick.AddListener(() =>
        {
            Hide();
            SaveManager.Instance.DeleteSave(currentSlot);
            saveLoadManager.GetComponent<SaveLoadManager>().SaveToSlot(currentSlot);
            HelperFunctions.LockCursor();
        });

        noBtn.onClick.AddListener(() =>
        {
            Hide();
            saveMenuUI.gameObject.SetActive(true);
        });
    }

    private void Start()
    {
        Hide();
    }

    public void SetCurrentSlot(short slot)
    {
        currentSlot = slot;
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

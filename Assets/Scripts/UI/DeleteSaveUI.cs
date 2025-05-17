using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSaveUI : MonoBehaviour
{
    private short currentSlot;

    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    [SerializeField] private GameObject singleplayerLoadUI;

    private void Awake()
    {
        yesBtn.onClick.AddListener(() =>
        {
            SaveManager.Instance.DeleteSave(currentSlot);
            gameObject.SetActive(false);
            singleplayerLoadUI.gameObject.SetActive(true);
        });

        noBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            singleplayerLoadUI.gameObject.SetActive(true);
        });
    }

    public void SetCurrentSlot(short slot)
    {
        currentSlot = slot;
    }
}

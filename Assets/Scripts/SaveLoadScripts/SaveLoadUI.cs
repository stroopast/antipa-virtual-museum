using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    public GameObject LoadMenu;
    public GameObject SaveMenu;
    public TextMeshProUGUI SavePopUpText;
    public void SaveToSlot(int slot)
    {
        SaveManager.Instance.SaveGame(slot);
        SaveMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(WaitForSavingProcess("save", slot));
    }

    public void LoadFromSlot(int slot)
    {
        // same thing is done in the LoadGame function in SaveManager script
        if (SaveManager.Instance.SaveExists(slot))
        {
            SaveManager.Instance.LoadGame(slot);
            LoadMenu.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(WaitForSavingProcess("load", slot));
        }
        else
        {
            Debug.Log($"No save found in slot {slot}!");
            // maybe add a pop up menu
        }
    }

    public void DeleteSlot(int slot)
    {
        SaveManager.Instance.DeleteSave(slot);
    }

    private IEnumerator WaitForSavingProcess(string option, int slot)
    {
        switch(option)
        {
            case "save":
                SavePopUpText.gameObject.SetActive(true);
                SavePopUpText.text = $"Jocul a fost salvat cu succes în slotul {slot}!";
                yield return new WaitForSeconds(3f);
                SavePopUpText.gameObject.SetActive(false);
                break;
            case "load":
                SavePopUpText.gameObject.SetActive(true);
                SavePopUpText.text = $"Jocul a fost încărcat cu succes din slotul {slot}!";
                yield return new WaitForSeconds(3f);
                SavePopUpText.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
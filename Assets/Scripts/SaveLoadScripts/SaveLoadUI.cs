using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    public GameObject LoadMenu;
    public GameObject SaveMenu;
    public TextMeshProUGUI SavePopUpText;
    public List<GameObject> DeleteLoadPopUpVect; 
    public List<GameObject> OverwriteSavePopUpVect;

    public void CheckSaveOverWrite(int slot)
    {
        string path = SaveManager.Instance.GetSlotPath(slot);
        if (File.Exists(path))
        {
            SaveMenu.gameObject.SetActive(false);
            OverwriteSavePopUpVect[slot - 1].gameObject.SetActive(true);
        }
        else
        {
            SaveToSlot(slot);
        }
    }
    public void SaveToSlot(int slot)
    {
        if(OverwriteSavePopUpVect[slot - 1].gameObject.activeSelf)
        {
            OverwriteSavePopUpVect[slot - 1].gameObject.SetActive(false);
        }
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
            LoadMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(WaitForSavingProcess("load", slot));
        }
        else
        {
            StartCoroutine(WaitForSavingProcess("empty", slot));
        }
    }

    public void DeleteSlot(int slot)
    {
        SaveManager.Instance.DeleteSave(slot);
        LoadMenu.gameObject.SetActive(true);
        DeleteLoadPopUpVect[slot - 1].gameObject.SetActive(false);
    }

    public void PressNoButtonOnLoadDelete(int slot)
    {
        LoadMenu.gameObject.SetActive(true);
        DeleteLoadPopUpVect[slot - 1].gameObject.SetActive(false);
    }

    public void PressNoButtonOnSaveOverWrite(int slot)
    {
        SaveMenu.gameObject.SetActive(true);
        OverwriteSavePopUpVect[slot - 1].gameObject.SetActive(false);
    }

    public void PressRemoveSlotButton(int slot)
    {
        string path = SaveManager.Instance.GetSlotPath(slot);
        if (File.Exists(path))
        {
            LoadMenu.gameObject.SetActive(false);
            DeleteLoadPopUpVect[slot - 1].gameObject.SetActive(true);
        }
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
            case "empty":
                SavePopUpText.gameObject.SetActive(true);
                SavePopUpText.text = $"Nu există o salvare în slotul {slot}!";
                yield return new WaitForSeconds(3f);
                SavePopUpText.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
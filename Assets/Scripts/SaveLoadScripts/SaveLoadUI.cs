using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    public GameObject LoadMenu;
    public void SaveToSlot(int slot)
    {
        SaveManager.Instance.SaveGame(slot);
    }

    public void LoadFromSlot(int slot)
    {
        // same thing is done in the LoadGame function in SaveManager script
        if (SaveManager.Instance.SaveExists(slot))
        {
            SaveManager.Instance.LoadGame(slot);
            LoadMenu.gameObject.SetActive(false);
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
}
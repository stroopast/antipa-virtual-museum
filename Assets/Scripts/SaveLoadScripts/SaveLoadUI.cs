using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    public void SaveToSlot(int slot)
    {
        SaveManager.Instance.SaveGame(slot);
    }

    public void LoadFromSlot(int slot)
    {
        SaveManager.Instance.LoadGame(slot);
    }

    public void DeleteSlot(int slot)
    {
        SaveManager.Instance.DeleteSave(slot);
    }
}
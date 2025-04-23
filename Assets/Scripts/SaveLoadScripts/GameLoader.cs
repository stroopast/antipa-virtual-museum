using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("SlotToLoad"))
        {
            int slot = PlayerPrefs.GetInt("SlotToLoad");
    
            SaveManager.Instance.LoadGame(slot);
    
            PlayerPrefs.DeleteKey("SlotToLoad");
        }
    }
}

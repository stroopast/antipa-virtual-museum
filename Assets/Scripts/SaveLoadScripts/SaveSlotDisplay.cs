using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveSlotDisplay : MonoBehaviour
{
    public int slotIndex; // Slot 1, 2, 3
    public TextMeshProUGUI infoText;

    private void OnEnable()
    {
        UpdateSlotDisplay();
    }

    public void UpdateSlotDisplay()
    {
        SaveData data = SaveManager.Instance.GetSlotData(slotIndex);

        if (data != null)
        {
            infoText.text = $"{data.playerName}\nProgres: {data.progressPercentage:F0}%";
        }
        else
        {
            infoText.text = "Slot gol";
        }
    }
}

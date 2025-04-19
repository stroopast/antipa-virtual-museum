using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class SaveSlotDisplay : MonoBehaviour
{
    public int slotIndex;
    public TextMeshProUGUI infoText;
  
    private void Update()
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

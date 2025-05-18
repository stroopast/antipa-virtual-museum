using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI saveStatusText;

    public void SaveToSlot(int slot)
    {
        SaveManager.Instance.SaveGame(slot);
        StartCoroutine(WaitForSavingProcess("save", slot));
    }

    private IEnumerator WaitForSavingProcess(string option, int slot)
    {
        switch (option)
        {
            case "save":
                saveStatusText.gameObject.SetActive(true);
                saveStatusText.text = $"Jocul a fost salvat cu succes în slotul {slot}!";
                yield return new WaitForSeconds(3f);
                saveStatusText.gameObject.SetActive(false);
                break;
            case "load":
                saveStatusText.gameObject.SetActive(true);
                saveStatusText.text = $"Jocul a fost încărcat cu succes din slotul {slot}!";
                yield return new WaitForSeconds(3f);
                saveStatusText.gameObject.SetActive(false);
                break;
            case "empty":
                saveStatusText.gameObject.SetActive(true);
                saveStatusText.text = $"Nu există o salvare în slotul {slot}!";
                yield return new WaitForSeconds(3f);
                saveStatusText.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}

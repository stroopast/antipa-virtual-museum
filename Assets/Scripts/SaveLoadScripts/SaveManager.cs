using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private string GetSlotPath(int slot) => $"{Application.persistentDataPath}/save_slot_{slot}.json";

    public void SaveGame(int slot)
    {
        SaveData data = new SaveData();

        data.playerName = PlayerPrefs.GetString("PlayerName", "Explorer");

        var player = GameObject.FindWithTag("Player");
        data.playerPosition = new Vector3Data(player.transform.position);

        data.unlockedAchievements = AchievementManager.Instance.GetUnlockedAchievements();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSlotPath(slot), json);

        Debug.Log($"Saved to slot {slot}");
    }

    public void LoadGame(int slot)
    {
        string path = GetSlotPath(slot);
        if (!File.Exists(path))
        {
            Debug.LogWarning($"No save in slot {slot}");
            return;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        PlayerPrefs.SetString("PlayerName", data.playerName);

        var player = GameObject.FindWithTag("Player");
        player.transform.position = data.playerPosition.ToVector3();

        AchievementManager.Instance.SetUnlockedAchievements(data.unlockedAchievements);

        Debug.Log($"Loaded from slot {slot}");
    }

    public void DeleteSave(int slot)
    {
        string path = GetSlotPath(slot);
        if (File.Exists(path)) File.Delete(path);
    }

    public bool SaveExists(int slot)
    {
        return File.Exists(GetSlotPath(slot));
    }
}

using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public string GetSlotPath(int slot) => $"{Application.persistentDataPath}/save_slot_{slot}.json";

    public void SaveGame(int slot)
    {
        SaveData data = new SaveData();

        data.playerName = GameModeManager.Instance.GetPlayerName();

        var player = GameObject.FindWithTag("Player");
        data.playerPosition = new Vector3Data(player.transform.position);

        data.unlockedAchievements = AchievementManager.Instance.GetUnlockedAchievements();

        int totalAchievements = AchievementManager.Instance.allAchievements.Count;
        int unlocked = data.unlockedAchievements.Count;
        data.progressPercentage = totalAchievements > 0 ? (unlocked * 100f) / totalAchievements : 0f;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetSlotPath(slot), json);
    }

    public void LoadGame(int slot)
    {
        string path = GetSlotPath(slot);

        if (!File.Exists(path))
        {
            return;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        GameModeManager.Instance.SetPlayerName(data.playerName);
        GameModeManager.Instance.SetPlayerGender(data.playerGender);

        var player = GameObject.FindWithTag("Player");
        player.transform.position = data.playerPosition.ToVector3();

        AchievementManager.Instance.SetUnlockedAchievements(data.unlockedAchievements);
    }

    public void DeleteSave(int slot)
    {
        string path = GetSlotPath(slot);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public bool DoesSaveExists(int slot)
    {
        return File.Exists(GetSlotPath(slot));
    }

    public SaveData GetSlotData(int slot)
    {
        string path = GetSlotPath(slot);
        if (!File.Exists(path)) return null;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }
}

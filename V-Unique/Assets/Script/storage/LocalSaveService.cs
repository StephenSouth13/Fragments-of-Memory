using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class LocalSaveService : ISaveService
{
    // Đường dẫn file lưu trữ: Application.persistentDataPath + "/savedata.json"
    private string savePath = Path.Combine(Application.persistentDataPath, "savedata.json");

    public async Task SaveGame(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        await File.WriteAllTextAsync(savePath, json);
        Debug.Log("Local Save Completed.");
    }

    public async Task<GameData> LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = await File.ReadAllTextAsync(savePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        return null;
    }
    
    public bool HasSaveFile()
    {
        return File.Exists(savePath);
    }
}
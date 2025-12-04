using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System; // Cần cho System.IO.Path.Combine

public class LocalSaveService : ISaveService
{
    private string savePath; 

    /// <summary>
    /// Hàm Khởi tạo: Tính toán đường dẫn lưu trữ an toàn.
    /// </summary>
    public LocalSaveService()
    {
        // Khắc phục lỗi: Gọi Application.persistentDataPath trong Constructor là an toàn.
        savePath = Path.Combine(Application.persistentDataPath, "savedata.json");
        Debug.Log("Save Path: " + savePath);
    }
    
    /// <summary>
    /// Lưu Game: Chuyển đổi GameData sang JSON và ghi vào file.
    /// </summary>
    public async Task SaveGame(GameData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data);
            await File.WriteAllTextAsync(savePath, json);
            Debug.Log("Local Save Completed.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving game: " + e.Message);
        }
    }

    /// <summary>
    /// Tải Game: Đọc file JSON và chuyển đổi ngược lại thành GameData.
    /// </summary>
    public async Task<GameData> LoadGame()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string json = await File.ReadAllTextAsync(savePath);
                return JsonUtility.FromJson<GameData>(json);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading game: File corrupted. " + e.Message);
                return null;
            }
        }
        return null;
    }
    
    /// <summary>
    /// Kiểm tra xem file lưu có tồn tại không.
    /// </summary>
    public bool HasSaveFile()
    {
        return File.Exists(savePath);
    }
}
using System.Threading.Tasks;

public interface ISaveService 
{
    /// <summary>
    /// Lưu đối tượng GameData hiện tại.
    /// </summary>
    Task SaveGame(GameData data); 

    /// <summary>
    /// Tải dữ liệu GameData từ nguồn lưu trữ.
    /// </summary>
    Task<GameData> LoadGame();

    /// <summary>
    /// Kiểm tra xem có file lưu hiện tại không.
    /// </summary>
    bool HasSaveFile();
}
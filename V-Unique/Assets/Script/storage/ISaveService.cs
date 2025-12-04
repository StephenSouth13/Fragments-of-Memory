using System.Threading.Tasks;

public interface ISaveService 
{
    // Cả Local và Cloud Save đều phải có 2 chức năng này
    Task SaveGame(GameData data); 
    Task<GameData> LoadGame();
    bool HasSaveFile();
}
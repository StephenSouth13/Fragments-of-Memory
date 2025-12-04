using UnityEngine;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    // **Đổi sang FirebaseSaveService() khi đã tích hợp Cloud Save**
    private ISaveService saveService = new LocalSaveService(); 
    
    // Dữ liệu game hiện tại đang chạy trong RAM
    public static GameData CurrentGameData { get; private set; }

    void Start()
    {
        // Chỉ để đảm bảo GameManager không bị hủy khi chuyển Scene
        DontDestroyOnLoad(gameObject);
    }

    // Gắn vào nút NEW GAME
    public void StartNewGame()
    {
        CurrentGameData = new GameData();
        // Tải Scene Chương I
    }

    // Gắn vào nút CONTINUE
    public async void ContinueGame()
    {
        if (saveService.HasSaveFile())
        {
            GameData loadedData = await saveService.LoadGame();
            if (loadedData != null)
            {
                CurrentGameData = loadedData;
                Debug.Log("Load Game thành công: Chapter " + CurrentGameData.currentChapter);
                // Tải Scene và đặt vị trí nhân vật
                return;
            }
        }
        
        Debug.Log("Không tìm thấy file lưu. Bắt đầu game mới.");
        StartNewGame(); // Bắt đầu game mới nếu không có file lưu
    }

    // Gọi khi thu thập Ký ức hoặc Quit Game
    public async void SaveCurrentProgress()
    {
        if (CurrentGameData != null)
        {
            // Cập nhật các biến (vị trí, thời gian) trước khi lưu
            // Ví dụ: CurrentGameData.posX = PlayerController.Instance.transform.position.x;
            
            await saveService.SaveGame(CurrentGameData);
        }
    }
}
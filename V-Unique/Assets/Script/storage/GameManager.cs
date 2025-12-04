using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // === CHỌN DỊCH VỤ LƯU TRỮ ===
    // Hiện tại là Local Save. Đổi thành 'new FirebaseSaveService()' khi chuyển sang Cloud Save.
    private ISaveService saveService; 
    
    // Dữ liệu game hiện tại (được giữ lại giữa các Scene)
    public static GameData CurrentGameData { get; private set; }

    void Awake() 
    {
        // Khắc phục lỗi: Khởi tạo dịch vụ ở đây là an toàn.
        saveService = new LocalSaveService(); 
        
        // Đảm bảo đối tượng này không bị hủy khi chuyển Scene
        DontDestroyOnLoad(gameObject);
    }

    // === CHỨC NĂNG GAMEPLAY ===

    /// <summary>
    /// Gắn vào nút NEW GAME.
    /// </summary>
    public void StartNewGame()
    {
        CurrentGameData = new GameData();
        Debug.Log("Bắt đầu game mới (Chapter " + CurrentGameData.currentChapter + ")");
        // TODO: Tải Scene Chapter I.
    }

    /// <summary>
    /// Gắn vào nút CONTINUE.
    /// </summary>
    public async void ContinueGame()
    {
        GameData loadedData = await saveService.LoadGame();
        
        if (loadedData != null)
        {
            CurrentGameData = loadedData;
            Debug.Log("Tải Game thành công! Chapter " + CurrentGameData.currentChapter + ". Vị trí X: " + CurrentGameData.posX);
            // TODO: Tải Scene tương ứng với CurrentGameData.currentChapter và đặt vị trí nhân vật.
        }
        else
        {
            Debug.Log("Không tìm thấy file lưu. Chuyển sang StartNewGame().");
            StartNewGame(); 
        }
    }

    /// <summary>
    /// Gọi khi thu thập Ký ức hoặc Save & Quit.
    /// </summary>
    public async void SaveCurrentProgress()
    {
        if (CurrentGameData != null)
        {
            // Cập nhật VỊ TRÍ hiện tại của người chơi trước khi lưu
            // Ví dụ: CurrentGameData.posX = PlayerController.Instance.transform.position.x;
            CurrentGameData.lastSaveTime = DateTime.Now;

            await saveService.SaveGame(CurrentGameData);
        }
    }
    
    /// <summary>
    /// Kiểm tra để ẩn/hiện nút CONTINUE.
    /// </summary>
    public bool CanContinueGame()
    {
        return saveService.HasSaveFile();
    }
}
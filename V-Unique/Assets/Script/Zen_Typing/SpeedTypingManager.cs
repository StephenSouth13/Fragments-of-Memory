using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SpeedTypingManager : MonoBehaviour
{
    // CẦN GÁN TRONG INSPECTOR
    public TextMeshProUGUI wordText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countText;
    
    // Tham chiếu đến Manager của Jigsaw Puzzle (để trao thưởng)
    public PuzzleManager puzzleManager; // <-- Gán đối tượng _PuzzleManager vào đây

    // Cấu hình Game
    public float maxTime = 20f; // Thời gian tối đa (20 giây)
    public int wordsToComplete = 10; // Số từ cần gõ để chiến thắng
    
    // Danh sách từ (Word Pool)
    // Dễ dàng thêm các từ ngữ yêu thương, tích cực như "HEART", "LOVE", "KINDNESS", "HOPE"
    public List<string> wordPool = new List<string>() { "HEART", "LOVE", "KINDNESS", "HOPE", "GENTLE", "CARE", "FRIEND", "POST", "DREAM", "LISTEN" };

    // Biến trạng thái
    private string currentWord;
    private int typeIndex = 0;
    private int wordsCompleted = 0;
    private float currentTime;
    private bool isGameActive = false;

    void Start()
    {
        // ... (Logic khởi động)
    }

    void Update()
    {
        if (!isGameActive) return;

        // Quản lý Timer
        currentTime -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.Max(0, currentTime):F2}s";

        if (currentTime <= 0)
        {
            GameOver(); // Hết giờ
            return;
        }

        // Lắng nghe phím bấm
        CheckInput();
    }
    
    // ... (Các hàm CheckInput, CheckLetter, UpdateWordDisplay như trước)

    void WordCompleted()
    {
        wordsCompleted++;
        // TODO: Phát âm thanh thỏa mãn và hiệu ứng lấp lánh mạnh
        
        SetNextWord();
    }

    // ✅ PHẦN TRAO THƯỞNG MẢNH GHÉP QUAN TRỌNG NHẤT
    void GameWon()
    {
        isGameActive = false;
        wordText.text = "🏆 HOÀN THÀNH TỐC ĐỘ! NHẬN MẢNH GHÉP! 🏆";
        
        // ----------------------------------------------------
        // ✅ GỌI HÀM TRAO THƯỞNG MẢNH GHÉP
        // Giả định rằng PuzzleManager có hàm để mở khóa một mảnh ghép.
        if (puzzleManager != null)
        {
            // Tùy chọn 1: Trao 1 mảnh ghép ngẫu nhiên
            // puzzleManager.UnlockRandomPiece(); 
            
            // Tùy chọn 2: Trao mảnh ghép số 0 (ví dụ)
            // Cần hàm cụ thể trong PuzzleManager để mở khóa Piece.
            // Ví dụ: puzzleManager.UnlockPiece(0); 
            
            Debug.Log("Đã gửi tín hiệu mở khóa mảnh ghép tới Puzzle Manager.");
        }
        // ----------------------------------------------------
    }

    void GameOver()
    {
        isGameActive = false;
        wordText.text = "HẾT GIỜ! THỬ LẠI NHÉ.";
    }
}
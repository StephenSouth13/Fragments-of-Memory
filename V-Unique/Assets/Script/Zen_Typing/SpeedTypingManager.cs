using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class SpeedTypingManager : MonoBehaviour
{
    // CẦN GÁN TRONG INSPECTOR
    public TextMeshProUGUI wordText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countText;
    
    // Tham chiếu đến Manager của Jigsaw Puzzle (để trao thưởng)
    public PuzzleManager puzzleManager; 

    // Cấu hình Game
    public float maxTime = 20f;
    public int wordsToComplete = 10;
    public List<string> wordList = new List<string>() { "KINDNESS", "HOPE", "PEACE", "CHILL", "GENTLE", "CARE", "LETTER", "POST", "FRIEND", "HELLO" };

    // Biến trạng thái
    private string currentWord;
    private int typeIndex = 0;
    private int wordsCompleted = 0;
    private float currentTime;
    private bool isGameActive = false;

    // =========================================================================
    // HÀM KHỞI ĐỘNG CỦA UNITY
    // =========================================================================

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!isGameActive) return;

        // Quản lý Timer
        currentTime -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.Max(0, currentTime):F2}s";

        if (currentTime <= 0)
        {
            GameOver();
            return;
        }

        // Lắng nghe phím bấm
        CheckInput(); // <--- ĐỊNH NGHĨA HÀM NÀY PHẢI CÓ
    }

    // =========================================================================
    // HÀM LOGIC GAME
    // =========================================================================

    void StartGame()
    {
        currentTime = maxTime;
        wordsCompleted = 0;
        isGameActive = true;
        SetNextWord(); // <--- ĐỊNH NGHĨA HÀM NÀY PHẢI CÓ
    }

    void CheckInput()
    {
        if (currentWord.Length == 0) return;

        // Lấy ký tự input từ người dùng
        foreach (char letter in Input.inputString)
        {
            if (char.IsLetter(letter))
            {
                CheckLetter(char.ToUpper(letter));
                return;
            }
        }
    }

    void CheckLetter(char typedLetter)
    {
        char targetLetter = currentWord[typeIndex];

        if (typedLetter == targetLetter)
        {
            // Gõ đúng
            typeIndex++;
            
            if (typeIndex >= currentWord.Length)
            {
                WordCompleted();
            }
            else
            {
                UpdateWordDisplay();
            }
        }
        else
        {
            Debug.Log("Gõ sai!");
        }
    }

    void SetNextWord()
    {
        if (wordsCompleted >= wordsToComplete)
        {
            GameWon();
            return;
        }

        // Chọn và hiển thị từ mới
        currentWord = wordList[Random.Range(0, wordList.Count)].ToUpper();
        typeIndex = 0;
        UpdateWordDisplay();
    }

    void WordCompleted()
    {
        wordsCompleted++;
        // TODO: Phát âm thanh thỏa mãn và hiệu ứng lấp lánh nhẹ
        
        // Chuyển sang từ tiếp theo hoặc kết thúc game
        SetNextWord();
    }

    void UpdateWordDisplay()
    {
        // Tạo chuỗi hiển thị: Tô màu phần đã gõ
        string typedPart = "<color=#69F0AE>" + currentWord.Substring(0, typeIndex) + "</color>";
        string remainingPart = currentWord.Substring(typeIndex);
        wordText.text = typedPart + remainingPart;
        
        countText.text = $"Words: {wordsCompleted}/{wordsToComplete}";
    }
    
    // =========================================================================
    // HÀM KẾT THÚC GAME
    // =========================================================================

    void GameWon()
    {
        isGameActive = false;
        wordText.text = "🏆 THẮNG! NHẬN VẬT PHẨM CHILL! 🏆";
        
        // LOGIC TRAO THƯỞNG MẢNH GHÉP
        if (puzzleManager != null)
        {
            Debug.Log("Đã gửi tín hiệu mở khóa mảnh ghép tới Puzzle Manager.");
            // puzzleManager.UnlockRandomPiece(); <-- Cần hàm này trong PuzzleManager
        }
    }

    void GameOver()
    {
        isGameActive = false;
        wordText.text = "HẾT GIỜ! THỬ LẠI NHÉ.";
    }
}
using UnityEngine;
using System;
using System.Collections.Generic; // Cần thiết cho List

// Đánh dấu để C# và JSONUtility có thể xử lý lớp này
[System.Serializable] 
public class GameData
{
    // === Dữ liệu Game Cốt lõi ===
    public int currentChapter = 1; // Bắt đầu từ Chương I
    public List<int> unlockedFragments = new List<int>(); // ID các Mảnh Ký ức đã thu thập (ví dụ: 101, 205, 303)

    // === Vị trí Nhân vật (cần khi Load Game) ===
    public float posX = 0f;
    public float posY = 0f;
    public float posZ = 0f;

    // === Tùy chọn: Thêm các biến khác ===
    public bool hasFinishedGame = false;
    public DateTime lastSaveTime; // Thường dùng cho Cloud Save để kiểm tra phiên bản mới nhất
    
    // Phương thức khởi tạo mặc định cho game mới
    public GameData()
    {
        currentChapter = 1;
        unlockedFragments = new List<int>();
        posX = 0f;
        posY = 0f;
        posZ = 0f;
        hasFinishedGame = false;
        lastSaveTime = DateTime.Now;
    }
}
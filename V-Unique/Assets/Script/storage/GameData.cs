using UnityEngine;
using System;
using System.Collections.Generic;

// Đánh dấu để C# và JSONUtility có thể xử lý
[System.Serializable] 
public class GameData
{
    // === Dữ liệu Game Cốt lõi ===
    public int currentChapter; 
    public List<int> unlockedFragments; // ID các Mảnh Ký ức đã thu thập
    
    // === Vị trí Nhân vật ===
    public float posX;
    public float posY;
    public float posZ;

    // Thời gian lưu (Hữu ích để biết file lưu nào mới nhất)
    public DateTime lastSaveTime; 
    
    /// <summary>
    /// Hàm Khởi tạo mặc định cho game mới (New Game).
    /// </summary>
    public GameData()
    {
        currentChapter = 1; // Luôn bắt đầu từ Chapter 1
        unlockedFragments = new List<int>();
        posX = 0f; // Vị trí xuất phát mặc định
        posY = 0f;
        posZ = 0f;
        lastSaveTime = DateTime.Now;
    }
}
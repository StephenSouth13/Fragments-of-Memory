using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ZenTypingManager : MonoBehaviour
{
    public static ZenTypingManager Instance; 
    
    // CẦN GÁN TRONG INSPECTOR:
    public ZenWordObject wordPrefab; 
    public Transform spawnPoint; // Điểm sinh từ (ví dụ: bên phải màn hình)
    
    // Danh sách từ (Word Pool)
    public List<string> wordPool = new List<string>() 
    { 
        "KINDNESS", "HOPE", "PEACE", "GENTLE", "CARE", "SERENITY", 
        "AFFIRMATION", "SUNSHINE", "WHISPER", "DREAM", "LISTEN", "COMFORT"
    };
    
    // Biến trạng thái
    private List<ZenWordObject> activeWords = new List<ZenWordObject>();
    private ZenWordObject currentTarget = null; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Bắt đầu sinh từ một cách đều đặn
        InvokeRepeating("SpawnWord", 2f, 4f); 
    }
    
    void SpawnWord()
    {
        if (wordPool.Count == 0 || wordPrefab == null || spawnPoint == null) return;

        string wordToSpawn = wordPool[Random.Range(0, wordPool.Count)];
        
        // Tính toán vị trí sinh: Random vị trí Y, Z cố định = 0
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.y += Random.Range(-2f, 2f); 
        spawnPos.z = 0f; // ✅ Đảm bảo sinh ở mặt phẳng 2D
        
        // Tạo và thiết lập từ
        ZenWordObject newWord = Instantiate(wordPrefab, spawnPos, Quaternion.identity);
        newWord.SetWord(wordToSpawn);
        activeWords.Add(newWord);
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        // Lấy ký tự input
        foreach (char letter in Input.inputString)
        {
            if (char.IsLetter(letter))
            {
                ProcessLetter(char.ToUpper(letter));
                return;
            }
        }
    }

    void ProcessLetter(char typedLetter)
    {
        if (currentTarget == null)
        {
            // 1. CHƯA CÓ MỤC TIÊU: Tìm từ bắt đầu bằng chữ cái vừa gõ để khóa mục tiêu
            currentTarget = FindTargetWord(typedLetter);
        }
        
        if (currentTarget != null)
        {
            // 2. CÓ MỤC TIÊU: Kiểm tra chữ cái tiếp theo
            if (currentTarget.GetNextLetter() == typedLetter)
            {
                currentTarget.TypeLetter(); 
                // Logic gõ đúng đã được xử lý trong ZenWordObject
            }
            // Logic gõ sai: Trong game Zen, ta chỉ cần bỏ qua.
        }
    }

    ZenWordObject FindTargetWord(char firstLetter)
    {
        // Tìm từ đầu tiên trong danh sách mà có chữ cái đầu tiên khớp
        return activeWords.FirstOrDefault(word => word.GetNextLetter() == firstLetter);
    }
    
    public void NotifyWordHarvested(ZenWordObject harvestedWord)
    {
        activeWords.Remove(harvestedWord);
        // Hủy khóa mục tiêu nếu từ này là mục tiêu hiện tại
        if (currentTarget == harvestedWord)
        {
            currentTarget = null;
        }
        
        // ✅ LOGIC NHẬN THƯỞNG: Đã gõ xong 1 từ.
        Debug.Log("Gõ xong 1 từ. Tích lũy điểm/vật phẩm Chill!");
    }
    
    public void NotifyWordLost(ZenWordObject lostWord)
    {
        activeWords.Remove(lostWord);
        if (currentTarget == lostWord)
        {
            currentTarget = null;
        }
        Debug.Log("Một từ đã trôi qua. Tiếp tục tập trung!");
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Giữ lại mặc dù chỉ dùng LevelLoader
using UnityEngine.UI;

public class Puzzlemanager : MonoBehaviour
{
    public static Puzzlemanager Instance;

    [Header("UI & Logic")]
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI timerText;

    [Header("Puzzle Configuration")]
    public Transform[] spawnPoints;
    private Puzzlepiece[] pieces; // Không cần public
    public float timeLimit = 60f;

    [Header("Rewards & Navigation")]
    public string currentLevelID = "Minigame1";
    public string defaultRoomScene = "Phòng ngủ";
    public string rewardLetterID = "Thu_1";

    private bool gameEnded = false;
    private int totalPieces;
    private int piecesLocked = 0;

    void Awake()
    {
        // Đảm bảo chỉ có một instance của Puzzlemanager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 🛠️ Sửa lỗi cảnh báo CS0618: Sử dụng FindObjectsByType thay thế
        pieces = FindObjectsByType<Puzzlepiece>(FindObjectsSortMode.None);


        totalPieces = pieces.Length;

        if (spawnPoints.Length < pieces.Length)
        {
            Debug.LogError("LỖI: Số lượng điểm Spawn ít hơn số mảnh ghép! Cần ít nhất " + totalPieces + " điểm.");
            // Nên Disable game hoặc kết thúc ngay nếu lỗi nghiêm trọng
            return;
        }

        ShuffleAndSpawn();
    }

    void ShuffleAndSpawn()
    {
        // Sử dụng một List để chọn ngẫu nhiên các điểm spawn mà không bị trùng lặp
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        foreach (var piece in pieces)
        {
            if (availablePoints.Count > 0)
            {
                int randomIndex = Random.Range(0, availablePoints.Count);
                Transform randomPoint = availablePoints[randomIndex];

                // Di chuyển mảnh ghép đến vị trí spawn
                piece.transform.position = randomPoint.position;
                
                // Đảm bảo mảnh ghép không bị khóa (nếu có reset)
                // piece.isLocked = false; 

                availablePoints.RemoveAt(randomIndex);
            }
        }
    }

    void Update()
    {
        if (gameEnded) return;

        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            if (timerText != null) 
            {
                // Format thời gian hiển thị (làm tròn số nguyên)
                timerText.text = "Time: " + Mathf.RoundToInt(timeLimit).ToString();
            }
        }
        else
        {
            GameOver();
        }
    }

    // Được gọi từ mỗi mảnh ghép khi nó khớp đúng vị trí
    public void CheckWinCondition()
    {
        piecesLocked++;
        Debug.Log("Đã xếp: " + piecesLocked + "/" + totalPieces);

        if (piecesLocked >= totalPieces)
        {
            Victory();
        }
    }

    void Victory()
    {
        gameEnded = true;
        Debug.Log("THẮNG RỒI! Đã hoàn thành Puzzle: " + currentLevelID);
        
        // --- Xử lý UI Panel ---
        UIPopupEffect effect = winPanel.GetComponent<UIPopupEffect>();
        if (effect != null)
        {
            effect.Show(); // Gọi hàm hiện từ từ (nếu có script UIPopupEffect)
        }
        else
        {
            // Cách thủ công
            winPanel.SetActive(true);
            CanvasGroup group = winPanel.GetComponent<CanvasGroup>();
            if (group != null)
            {
                group.alpha = 1f;
                group.blocksRaycasts = true;
            }
        }
        
        // --- Lưu Tiến Trình & Phần Thưởng ---
        // Đánh dấu minigame hiện tại là hoàn thành (Ví dụ: PlayerPrefs.SetInt("Minigame1", 1))
        PlayerPrefs.SetInt(currentLevelID, 1); 
        // Đánh dấu nhận được thư thưởng
        PlayerPrefs.SetInt("Letter_" + rewardLetterID, 1);
        PlayerPrefs.Save();
        Debug.Log("Đã lưu chiến thắng và thư thưởng!");
    }

    void GameOver()
    {
        gameEnded = true;
        Debug.Log("HẾT GIỜ! Bạn đã thua.");
        
        // --- Xử lý UI Panel ---
        UIPopupEffect effect = losePanel.GetComponent<UIPopupEffect>();
        if (effect != null)
        {
            effect.Show(); // Gọi hàm hiện từ từ (nếu có script UIPopupEffect)
        }
        else
        {
            // Cách thủ công
            losePanel.SetActive(true);
            CanvasGroup group = losePanel.GetComponent<CanvasGroup>();
            if (group != null)
            {
                group.alpha = 1f;
                group.blocksRaycasts = true;
            }
        }
    }

    public void ReturnToRoom()
    {
        // Lấy tên scene cuối cùng đã lưu, nếu không có thì dùng defaultRoomScene
        string sceneToLoad = PlayerPrefs.GetString("LastScene", defaultRoomScene);

        Time.timeScale = 1f;

        // Sử dụng LevelLoader (giả định đây là script tải scene tùy chỉnh của bạn)
        if (LevelLoader.Instance != null)
        {
            LevelLoader.Instance.LoadLevel(sceneToLoad);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        
        Debug.Log("Đang quay về: " + sceneToLoad);
    }
}
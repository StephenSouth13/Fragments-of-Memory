using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puzzlemanager : MonoBehaviour
{
    public static Puzzlemanager Instance;

    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI timerText;

    public Transform[] spawnPoints;
    public Puzzlepiece[] pieces;

    public float timeLimit = 60f;
    private bool gameEnded = false;
    private int totalPieces;
    private int piecesLocked = 0;

    public string currentLevelID = "Minigame1";
    public string defaultRoomScene = "Phòng ngủ";
    public string rewardLetterID = "Thu_1";
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pieces = FindObjectsOfType<Puzzlepiece>();

 
        totalPieces = pieces.Length;

        if (spawnPoints.Length < pieces.Length)
        {
            Debug.LogError("LỖI: Số lượng điểm Spawn ít hơn số mảnh ghép!");
            return;
        }

        ShuffleAndSpawn();
    }

    void ShuffleAndSpawn()
    {
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        foreach (var piece in pieces)
        {
            if (availablePoints.Count > 0)
            {
                int randomIndex = Random.Range(0, availablePoints.Count);
                Transform randomPoint = availablePoints[randomIndex];

                // Di chuyển mảnh ghép đến vị trí spawn
                piece.transform.position = randomPoint.position;

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
            if (timerText != null) timerText.text = "Time: " + Mathf.Round(timeLimit).ToString();
        }
        else
        {
            GameOver();
        }
    }

    public void CheckWinCondition()
    {
        piecesLocked++;
        // Debug để kiểm tra tiến độ
        Debug.Log("Đã xếp: " + piecesLocked + "/" + totalPieces);

        if (piecesLocked >= totalPieces)
        {
            Victory();
        }
    }

    void Victory()
    {
        gameEnded = true;
        Debug.Log("THẮNG RỒI!");
        if (winPanel != null) winPanel.SetActive(true);
        PlayerPrefs.SetInt(currentLevelID, 1);
        PlayerPrefs.SetInt("Letter_" + rewardLetterID, 1);
        PlayerPrefs.Save();
        Debug.Log("Đã lưu chiến thắng!");
    }

    void GameOver()
    {
        gameEnded = true;
        Debug.Log("HẾT GIỜ!");
        if (losePanel != null) losePanel.SetActive(true);
    }

    public void ReturnToRoom()
    {

        string sceneToLoad = PlayerPrefs.GetString("LastScene", defaultRoomScene);

        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("Đang quay về: " + sceneToLoad);
    }
}
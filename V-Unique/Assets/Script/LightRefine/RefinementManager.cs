using UnityEngine;
using System.Collections; // Cần cho Coroutine (GameTimer)
using System.Collections.Generic;
using TMPro; // ✅ CẦN THIẾT để sử dụng TextMeshProUGUI

public class RefinementManager : MonoBehaviour
{
    // CẦN GÁN TRONG INSPECTOR:
    public GameObject goodParticlePrefab;
    public GameObject badParticlePrefab;
    public Transform spawnPoint; // Điểm sinh hạt
    public Transform collectorPoint; // Điểm đích thu thập hạt
    
    // ✅ THAM CHIẾU UI (Cần kéo Text component vào)
    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI scoreText; 
    // public PuzzleManager puzzleManager; // Liên kết trao thưởng

    // Cấu hình Game
    public float maxGameTime = 30f; 
    public int scoreToWin = 20;
    
    // Biến trạng thái
    private int currentScore = 0;
    private bool isGameActive = false;
    private float timeRemaining; 

    void Start()
    {
        isGameActive = true;
        timeRemaining = maxGameTime; 
        
        // Khởi tạo UI hiển thị ban đầu
        if (scoreText != null) 
        {
            scoreText.text = $"Score: {currentScore}/{scoreToWin}";
        }
        
        StartCoroutine(GameTimer()); 
        InvokeRepeating("SpawnParticle", 1f, 0.5f); 
    }

    void SpawnParticle()
    {
        if (!isGameActive) return;

        bool isGood = Random.value > 0.5f;
        GameObject particlePrefab = isGood ? goodParticlePrefab : badParticlePrefab;

        Vector3 spawnPos = spawnPoint.position;
        spawnPos.y += Random.Range(-3f, 3f);
        
        GameObject newParticle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);
        newParticle.GetComponent<ParticleMovement>().SetTargetAndManager(collectorPoint.position, this);
    }
    
    // ✅ HÀM TÍNH ĐIỂM (CẬP NHẬT UI SCORE)
    public void ParticleScored(bool isGood)
    {
        if (!isGameActive) return;

        if (isGood)
        {
            currentScore++;
        }
        else
        {
            currentScore--; // Hạt xấu trừ điểm
        }
        
        // ✅ CẬP NHẬT UI SCORE
        if (scoreText != null) 
        {
            scoreText.text = $"Score: {currentScore}/{scoreToWin}";
        }
        Debug.Log($"Score: {currentScore}/{scoreToWin}");

        if (currentScore >= scoreToWin)
        {
            GameOver(true);
        }
    }

    // ✅ COROUTINE ĐẾM NGƯỢC THỜI GIAN (CẬP NHẬT UI TIMER)
    IEnumerator GameTimer()
    {
        while (timeRemaining > 0 && isGameActive)
        {
            timeRemaining -= Time.deltaTime;
            
            // ✅ CẬP NHẬT UI TIMER
            if (timerText != null) 
            {
                // Sử dụng Mathf.Max(0, ...) để tránh hiển thị số âm khi hết giờ
                timerText.text = $"Time: {Mathf.Max(0, timeRemaining):F1}s";
            }
            
            yield return null;
        }

        if (isGameActive)
        {
            GameOver(false); // Thua vì hết giờ
        }
    }

    void GameOver(bool hasWon)
    {
        isGameActive = false;
        CancelInvoke("SpawnParticle"); 
        StopAllCoroutines(); 
        
        if (hasWon)
        {
            Debug.Log("🎉 MẢNH KÝ ỨC ĐÃ ĐƯỢC THANH LỌC! (WIN)");
            // TODO: Logic trao thưởng mảnh ký ức
        }
        else
        {
            Debug.Log("⌛ HẾT GIỜ! THỬ LẠI. (LOSE)");
        }
        
        // TODO: Xóa tất cả hạt còn lại trong Scene
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class RefinementManager : MonoBehaviour
{
    // CẦN GÁN TRONG INSPECTOR:
    public GameObject goodParticlePrefab;
    public GameObject badParticlePrefab;
    public Transform spawnPoint; // Điểm sinh hạt
    public Transform collectorPoint; // Điểm đích thu thập hạt
    
    // THAM CHIẾU UI
    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI scoreText; 
    // public PuzzleManager puzzleManager; // Liên kết trao thưởng (Gán sau)

    // Cấu hình Game
    public float maxGameTime = 30f; 
    public int scoreToWin = 20;
    
    // Biến trạng thái
    private int currentScore = 0;
    private bool isGameActive = false;
    private float timeRemaining; 

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        isGameActive = true;
        timeRemaining = maxGameTime; 
        currentScore = 0;

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
        
        // Gắn script Movement và truyền Manager
        newParticle.GetComponent<ParticleMovement>().SetTargetAndManager(collectorPoint.position, this);
    }
    
    // HÀM TÍNH ĐIỂM (CẬP NHẬT UI SCORE VÀ KIỂM TRA WIN)
    public void ParticleScored(bool isGood)
    {
        if (!isGameActive) return;

        if (isGood)
        {
            currentScore++;
            Debug.Log("GOOD Particle Collected!");
        }
        else
        {
            currentScore--; // Hạt xấu trừ điểm
            Debug.Log("BAD Particle Collected! Penalty.");
        }
        
        // CẬP NHẬT UI SCORE
        if (scoreText != null) 
        {
            scoreText.text = $"Score: {currentScore}/{scoreToWin}";
        }

        // ✅ KÍCH HOẠT KẾT THÚC GAME KHI ĐỦ ĐIỂM
        if (currentScore >= scoreToWin)
        {
            GameOver(true);
        }
    }

    // COROUTINE ĐẾM NGƯỢC THỜI GIAN
    IEnumerator GameTimer()
    {
        while (timeRemaining > 0 && isGameActive)
        {
            timeRemaining -= Time.deltaTime;
            
            // CẬP NHẬT UI TIMER
            if (timerText != null) 
            {
                timerText.text = $"Time: {Mathf.Max(0, timeRemaining):F1}s";
            }
            
            yield return null;
        }

        if (isGameActive)
        {
            GameOver(false); // Thua vì hết giờ
        }
    }

    // HÀM KẾT THÚC GAME CHÍNH XÁC
    void GameOver(bool hasWon)
    {
        // 1. KHÓA TRẠNG THÁI GAME
        if (!isGameActive) return; // Bảo vệ: Tránh gọi hàm 2 lần
        isGameActive = false;
        
        // 2. DỪNG TẤT CẢ LỖI
        CancelInvoke("SpawnParticle"); 
        StopAllCoroutines();

        // 3. ✅ XÓA CÁC HẠT CÒN LẠI TRONG SCENE (Fix Lỗi Tồn đọng)
        //ParticleMovement[] remainingParticles = FindObjectsOfType<ParticleMovement>();
        ParticleMovement[] remainingParticles = FindObjectsByType<ParticleMovement>(FindObjectsSortMode.None);
        foreach (ParticleMovement particle in remainingParticles)
        {
            Destroy(particle.gameObject);
        }

        // 4. HIỂN THỊ KẾT QUẢ VÀ TRAO THƯỞNG
        if (hasWon)
        {
            Debug.Log("🎉 MẢNH KÝ ỨC ĐÃ ĐƯỢC THANH LỌC! (WIN)");
            // TODO: Gọi logic trao thưởng mảnh ký ức (Nếu puzzleManager được gán)
        }
        else
        {
            // Hiển thị 0.0s trên đồng hồ khi thua hết giờ
            if (timerText != null) timerText.text = "Time: 0.0s"; 
            Debug.Log("⌛ HẾT GIỜ! THỬ LẠI. (LOSE)");
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

public class RefinementManager : MonoBehaviour
{
    // CẦN GÁN TRONG INSPECTOR:
    public GameObject goodParticlePrefab;
    public GameObject badParticlePrefab;
    public Transform spawnPoint; // Điểm sinh hạt
    public Transform collectorPoint; // Điểm đích thu thập hạt
    
    // Cấu hình Game
    public int scoreToWin = 20;
    private int currentScore = 0;
    private bool isGameActive = false;

    void Start()
    {
        isGameActive = true;
        // Bắt đầu sinh hạt sau 1 giây, lặp lại mỗi 0.5 giây
        InvokeRepeating("SpawnParticle", 1f, 0.5f); 
    }

    void SpawnParticle()
    {
        if (!isGameActive) return;

        // Sinh ra hạt ngẫu nhiên (50/50 Good/Bad)
        bool isGood = Random.value > 0.5f;
        GameObject particlePrefab = isGood ? goodParticlePrefab : badParticlePrefab;

        Vector3 spawnPos = spawnPoint.position;
        spawnPos.y += Random.Range(-3f, 3f); // Random vị trí Y
        
        GameObject newParticle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);
        // Gắn script Movement (sẽ tạo ở Bước 3)
        newParticle.GetComponent<ParticleMovement>().SetTarget(collectorPoint.position);
    }
    
    // Hàm này được gọi từ script hạt khi hạt đến đích
    public void ParticleCollected(bool isGood)
    {
        if (!isGameActive) return;

        if (isGood)
        {
            currentScore++;
            // TODO: Phát âm thanh ASMR "ting" thỏa mãn
        }
        else
        {
            currentScore--; // Trừ điểm nếu hạt xấu đi vào vùng thu thập (tùy chọn)
        }
        
        Debug.Log($"Score: {currentScore}/{scoreToWin}");

        if (currentScore >= scoreToWin)
        {
            GameWon();
        }
    }

    void GameWon()
    {
        isGameActive = false;
        // TODO: Kích hoạt hiệu ứng ánh sáng lớn và âm thanh chữa lành
        Debug.Log("MẢNH KÝ ỨC ĐÃ ĐƯỢC THANH LỌC!");
        // Gọi logic trao thưởng mảnh ký ức tại đây
    }
}
using UnityEngine;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour
{
    // Tham chiếu cần gán trong Inspector:
    public TrailRenderer trailPrefab; // Prefab của dòng ánh sáng (sẽ tạo ở Bước 3)
    
    private TrailRenderer currentTrail;
    private HarvestManager manager;

    void Start()
    {
        manager = FindAnyObjectByType<HarvestManager>();
        if (manager == null) Debug.LogError("HarvestManager not found in scene!");
        
        // Ẩn đối tượng Sweeper khi game bắt đầu
        gameObject.SetActive(false); 
    }

    // Bắt đầu vẽ khi click chuột
    void OnMouseDown()
    {
        if (trailPrefab == null) return;

        // 1. Kích hoạt đối tượng Sweeper và tạo dòng Trail mới
        gameObject.SetActive(true);
        
        // 2. Tạo một bản sao của Trail Renderer
        currentTrail = Instantiate(trailPrefab, transform.parent);
        
        // Đặt Sweeper vào vị trí chuột
        UpdatePosition(Input.mousePosition);
    }

    // Đang vẽ khi kéo chuột
    void OnMouseDrag()
    {
        if (currentTrail != null)
        {
            UpdatePosition(Input.mousePosition);
        }
    }

    // Dừng vẽ khi nhả chuột
    void OnMouseUp()
    {
        // Dừng dòng Trail hiện tại
        if (currentTrail != null)
        {
            currentTrail.transform.parent = null; // Tách khỏi Sweeper
            currentTrail = null;
        }

        // Tắt đối tượng Sweeper
        gameObject.SetActive(false); 
        
        // Gọi hàm kiểm tra kết quả từ Manager
        // manager.CheckHarvestCompletion(); 
    }

    // Hàm chung để cập nhật vị trí Sweeper theo chuột
    void UpdatePosition(Vector3 screenPoint)
    {
        // Chuyển đổi vị trí chuột từ màn hình sang thế giới
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        
        // Đặt Z = 0 để đảm bảo nó ở mặt phẳng 2D
        transform.position = new Vector3(worldPoint.x, worldPoint.y, 0f);
    }
}
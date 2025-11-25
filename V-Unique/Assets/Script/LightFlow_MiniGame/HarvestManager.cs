using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    // Biến
    public int totalItemsToHarvest = 5;
    private int itemsHarvested = 0;
    
    // Tham chiếu đến UI (Giao diện người dùng)
    // public TextMeshProUGUI scoreText;

    void Start()
    {
        // Khởi tạo trạng thái game
        itemsHarvested = 0;
    }

    // HÀM QUAN TRỌNG: LineDrawer sẽ gọi hàm này sau khi người chơi nhả chuột
    public void CheckHarvestCompletion(TrailRenderer trail)
    {
        // 1. Kiểm tra va chạm: Kiểm tra xem Trail này có va chạm với 
        //    tất cả các vật phẩm cần thu thập hay không.
        
        // 2. Cập nhật: Nếu thành công
        // itemsHarvested++;
        // UpdateScoreUI(); 

        // 3. Kiểm tra thắng cuộc
        // if (itemsHarvested >= totalItemsToHarvest)
        // {
        //     EndGame(true); // Thắng
        // }
    }

    private void EndGame(bool success)
    {
        // Xử lý khi game kết thúc (hiện thông báo, chuyển cảnh,...)
    }
}
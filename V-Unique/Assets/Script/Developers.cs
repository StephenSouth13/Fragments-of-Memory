using UnityEngine;
using UnityEngine.UI;

// Đổi tên class để phản ánh đúng chức năng mới
public class DevelopersInfo : MonoBehaviour
{
    // === Các Tham chiếu UI ===
    // Kéo Panel 'DEVELOPERS INFO' từ Hierarchy vào đây
    public GameObject developersPanel; // Đã đổi tên biến Panel cho đúng mục đích

    // Kéo Image Component 'DeveloperPhotoDisplay' vào đây
    public Image developerImageDisplay; // Đã đổi tên biến hiển thị ảnh

    // Kéo các Button Component vào đây
    public Button nextButton;
    public Button prevButton;
    public Button closeButton; // Nút X

    // === Tài nguyên & Logic ===
    // Kéo tất cả các Sprites (Ảnh thành viên/Sơ đồ) vào mảng này theo thứ tự
    public Sprite[] developerSlides; // Đã đổi tên mảng Sprite

    private int currentPage = 0; // Index của trang hiện tại (bắt đầu từ 0)

    // Hàm 1: Mở Thông tin (Gắn vào OnClick() của nút 'DEVELOPERS' trên Main Menu)
    public void OpenDevelopersInfo() // Đã đổi tên hàm
    {
        // Bảo vệ: không có ảnh thì thoát
        if (developerSlides.Length == 0) return;

        // CẦN SỬA LỖI: howToPlayPanel không tồn tại
        developersPanel.SetActive(true); 
        
        currentPage = 0; // Luôn bắt đầu từ trang đầu tiên
        UpdateDeveloperDisplay();
    }

    // Hàm 2: Đóng Thông tin (Gắn vào OnClick() của nút 'CloseButton')
    public void CloseDevelopersInfo() // Đã đổi tên hàm
    {
        // CẦN SỬA LỖI: howToPlayPanel không tồn tại
        developersPanel.SetActive(false); 
    }

    // Hàm 3: Chuyển trang Tiếp theo (Gắn vào OnClick() của nút 'NextButton')
    public void NextPage()
    {
        if (currentPage < developerSlides.Length - 1)
        {
            currentPage++;
            UpdateDeveloperDisplay();
        }
    }

    // Hàm 4: Chuyển trang Trước đó (Gắn vào OnClick() của nút 'PrevButton')
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateDeveloperDisplay();
        }
    }

    // Hàm Cốt lõi: Cập nhật hình ảnh và trạng thái nút
    private void UpdateDeveloperDisplay()
    {
        // 1. Cập nhật Sprite
        developerImageDisplay.sprite = developerSlides[currentPage];

        // 2. Cập nhật trạng thái nút Next/Prev
        // Tắt nút Previous nếu đang ở trang đầu
        prevButton.gameObject.SetActive(currentPage > 0);
        
        // Tắt nút Next nếu đang ở trang cuối
        nextButton.gameObject.SetActive(currentPage < developerSlides.Length - 1);
    }
}
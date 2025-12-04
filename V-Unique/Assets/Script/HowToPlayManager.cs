using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    // === Các Tham chiếu UI ===
    // Kéo Panel 'HOW TO PLAY' từ Hierarchy vào đây
    public GameObject howToPlayPanel; 
    
    // Kéo Image Component 'InstructionImageDisplay' vào đây
    public Image instructionImageDisplay; 

    // Kéo các Button Component vào đây
    public Button nextButton;
    public Button prevButton;
    public Button closeButton; // Nút X

    // === Tài nguyên & Logic ===
    // Kéo tất cả các Sprites (Ảnh hướng dẫn) vào mảng này theo thứ tự
    public Sprite[] instructionImages; 

    private int currentPage = 0; // Index của trang hiện tại (bắt đầu từ 0)

    // Hàm 1: Mở Hướng dẫn (Gắn vào OnClick() của nút 'HOW TO PLAY' trên Main Menu)
    public void OpenHowToPlay()
    {
        if (instructionImages.Length == 0) return; // Bảo vệ: không có ảnh thì thoát

        howToPlayPanel.SetActive(true); 
        currentPage = 0; // Luôn bắt đầu từ trang đầu tiên
        UpdateInstructionDisplay();
    }

    // Hàm 2: Đóng Hướng dẫn (Gắn vào OnClick() của nút 'CloseButton')
    public void CloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    // Hàm 3: Chuyển trang Tiếp theo (Gắn vào OnClick() của nút 'NextButton')
    public void NextPage()
    {
        if (currentPage < instructionImages.Length - 1)
        {
            currentPage++;
            UpdateInstructionDisplay();
        }
    }

    // Hàm 4: Chuyển trang Trước đó (Gắn vào OnClick() của nút 'PrevButton')
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateInstructionDisplay();
        }
    }

    // Hàm Cốt lõi: Cập nhật hình ảnh và trạng thái nút
    private void UpdateInstructionDisplay()
    {
        // 1. Cập nhật Sprite
        instructionImageDisplay.sprite = instructionImages[currentPage];

        // 2. Cập nhật trạng thái nút Next/Prev
        prevButton.gameObject.SetActive(currentPage > 0);
        nextButton.gameObject.SetActive(currentPage < instructionImages.Length - 1);
    }
}
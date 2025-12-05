using UnityEngine;
using UnityEngine.UI;
using TMPro; // Sử dụng thư viện TextMeshPro

// Giả định class PageData đã được định nghĩa là độc lập (không nằm trong class nào khác)
// như trong giải pháp trước cho LaptopDataManager.cs

public class PageInteractionLaptop : MonoBehaviour
{
    [Header("UI Components")]
    // Hình ảnh hiển thị nội dung chính của trang
    public Image PageImage;
    
    // Văn bản hiển thị ghi chú/tiêu đề của trang (Sử dụng TextMeshPro)
    public TextMeshProUGUI PageNoteText; 
    
    // Nút để tải/khởi động cấp độ (Tùy chọn)
    public Button LoadLevelButton; 
    
    // Văn bản hiển thị ID cấp độ (Tùy chọn)
    public TextMeshProUGUI LevelIDText; 

    // Dữ liệu trang hiện tại
    private PageData currentPageData;

    // --- SETUP PAGE ---
    // Phương thức này nhận kiểu dữ liệu PageData độc lập
    public void SetupPage(PageData data)
    {
        // 1. Lưu trữ dữ liệu
        currentPageData = data;
        
        // 2. Cập nhật hình ảnh
        if (PageImage)
        {
            PageImage.sprite = data.image;
            PageImage.color = Color.white; 
        }

        // 3. Cập nhật văn bản ghi chú
        if (PageNoteText)
        {
            PageNoteText.text = data.note;
        }

        // 4. Cập nhật ID Level và nút (nếu có)
        bool levelAvailable = !string.IsNullOrEmpty(data.levelID);

        if (LevelIDText)
        {
            LevelIDText.text = data.levelID;
        }
        
        if (LoadLevelButton)
        {
            // Xóa các listener cũ và thêm hành động mới (nếu cần)
            LoadLevelButton.onClick.RemoveAllListeners();
            // Ví dụ: LoadLevelButton.onClick.AddListener(() => LoadScene(data.sceneName));
            
            // Hiển thị nút nếu có LevelID
            LoadLevelButton.gameObject.SetActive(levelAvailable);
        }
    }

    // --- SETUP EMPTY PAGE ---
    // Thiết lập trang trống (ví dụ: trang cuối cùng không có nội dung)
    public void SetupEmpty()
    {
        currentPageData = null;
        
        if (PageImage)
        {
            PageImage.sprite = null;
            PageImage.color = new Color(1, 1, 1, 0); // Làm trong suốt
        }

        if (PageNoteText)
        {
            PageNoteText.text = "";
        }

        if (LoadLevelButton)
        {
            LoadLevelButton.gameObject.SetActive(false);
        }
    }

    // Hàm Start và Update mặc định (giữ nguyên không đổi)
    void Start()
    {
        // Khởi tạo logic nếu cần
    }

    void Update()
    {
        // Logic cập nhật khung hình nếu cần
    }
    
    // Ví dụ về hàm tải scene
    // private void LoadScene(string sceneName)
    // {
    //     if (!string.IsNullOrEmpty(sceneName))
    //     {
    //         UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    //     }
    // }
}
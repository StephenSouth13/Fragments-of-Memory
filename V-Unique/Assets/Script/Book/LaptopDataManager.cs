using UnityEngine;
using System.Collections.Generic;

// ĐÃ DI CHUYỂN PageData ra khỏi class LaptopDataManager
[System.Serializable]
public class PageData // Loại bỏ tiền tố LaptopDataManager.
{
    public string note = "Trang số ...";
    public Sprite image;
    public string levelID;
    public string sceneName;
}

public class LaptopDataManager : MonoBehaviour
{
    [Header("Cấu hình Laptop")] 
    public Laptop laptopController; 

    // Kéo 2 cái object con "PuzzleContent" vào đây
    // Giả sử PageInteraction đã được sửa để chấp nhận kiểu PageData độc lập
    public PageInteractionLaptop rightPageContent; // <--- SỬA LỖI NẰM Ở ĐÂY
    public PageInteractionLaptop leftPageContent;

    [Header("Danh sách Dữ liệu")]
    // Vẫn sử dụng PageData (kiểu độc lập)
    public List<PageData> allPages;

    private int lastPageIndex = -1;

    void Start()
    {
        // --- SỬA LỖI 1: KHÔNG ĐƯỢC NẠP HÌNH PUZZLE VÀO BOOK PAGES ---
        // Xóa đoạn foreach gán sprite vào bookController đi.
        // Hãy để Laptop.cs tự lo phần hình ảnh tờ giấy cũ.

        UpdatePageContent();
    }

    void Update()
    {
        if (laptopController.currentPage != lastPageIndex)
        {
            UpdatePageContent();
            lastPageIndex = laptopController.currentPage;
        }
    }

    void UpdatePageContent()
    {
        int current = laptopController.currentPage;

        // 1. Cập nhật trang PHẢI (Right) - Index hiện tại
        if (current < allPages.Count)
        {
            // Bây giờ rightPageContent.SetupPage() sẽ nhận kiểu PageData độc lập
            rightPageContent.SetupPage(allPages[current]);
        }
        else
        {
            rightPageContent.SetupEmpty();
        }

        // Phần code còn lại (đã bị comment)
        //// Trang trái là trang trước đó (Index - 1)
        //int prevIndex = current - 1;

        //if (prevIndex >= 0 && prevIndex < allPages.Count)
        //{
        //    leftPageContent.SetupPage(allPages[prevIndex]);
        //}
        //else
        //{
        //    leftPageContent.SetupEmpty();
        //}
    }
}
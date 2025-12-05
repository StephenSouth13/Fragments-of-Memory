using UnityEngine;
using System.Collections.Generic;

public class BookDataManager : MonoBehaviour
{
    [System.Serializable]
    public class PageData
    {
        public string note = "Trang số ...";
        public Sprite image;
        public string levelID;
        public string sceneName;
    }

    [Header("Cấu hình Sách")]
    public Book bookController;

    // Kéo 2 cái object con "PuzzleContent" vào đây
    public PageInteraction rightPageContent;
    public PageInteraction leftPageContent;

    [Header("Danh sách Dữ liệu")]
    public List<PageData> allPages;

    private int lastPageIndex = -1;

    void Start() // Hoặc Awake
    {
        // --- SỬA LỖI 1: KHÔNG ĐƯỢC NẠP HÌNH PUZZLE VÀO BOOK PAGES ---
        // Xóa đoạn foreach gán sprite vào bookController đi.
        // Hãy để Book.cs tự lo phần hình ảnh tờ giấy cũ.

        UpdatePageContent();
    }

    void Update()
    {
        if (bookController.currentPage != lastPageIndex)
        {
            UpdatePageContent();
            lastPageIndex = bookController.currentPage;
        }
    }

    void UpdatePageContent()
    {
        int current = bookController.currentPage;

        // 1. Cập nhật trang PHẢI (Right) - Index hiện tại
        if (current < allPages.Count)
        {
            rightPageContent.SetupPage(allPages[current]);
        }
        else
        {
            rightPageContent.SetupEmpty();
        }

        //// Trang trái là trang trước đó (Index - 1)
        //int prevIndex = current - 1;

        //if (prevIndex >= 0 && prevIndex < allPages.Count)
        //{
        //    leftPageContent.SetupPage(allPages[prevIndex]);
        //}
        //else
        //{
        //    leftPageContent.SetupEmpty();
        //}
    }
}
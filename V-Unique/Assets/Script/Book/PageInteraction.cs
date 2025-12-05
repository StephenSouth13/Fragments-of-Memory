using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageInteraction : MonoBehaviour
{
    [Header("Hiển thị")]
    public Image contentImage; // Vẫn kéo cái khung ảnh vào đây

    // Các biến này giờ sẽ để private, được nạp vào từ Manager
    private string currentLevelID;
    private string currentSceneName;
    private bool isEmpty = true;

    [Header("Màu sắc")]
    public Color lockedColor = new Color(0.4f, 0.4f, 0.4f, 1f);
    public Color unlockedColor = Color.white;

    // --- HÀM NÀY ĐỂ MANAGER GỌI ---
    public void SetupPage(BookDataManager.PageData data)
    {
        isEmpty = false;
        currentLevelID = data.levelID;
        currentSceneName = data.sceneName;

        if (contentImage != null)
        {
            contentImage.sprite = data.image;
            contentImage.preserveAspect = true;
        }

        CheckWinStatus();
    }

    public void SetupEmpty()
    {
        isEmpty = true;
        if (contentImage != null) contentImage.color = Color.clear; // Ẩn đi
    }

    void CheckWinStatus()
    {
        if (isEmpty) return;

        bool isWin = PlayerPrefs.GetInt(currentLevelID, 0) == 1;

        if (isWin)
            contentImage.color = unlockedColor;
        else
            contentImage.color = lockedColor;
    }

    // --- HÀM CLICK GẮN VÀO NÚT ---
    public void OnPageClicked()
    {
        if (isEmpty) return;

        Debug.Log("Vào game: " + currentLevelID);
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(currentSceneName);
        LevelLoader.Instance.LoadLevel(currentSceneName);
    }
}
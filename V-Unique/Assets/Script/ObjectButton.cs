using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    public static ObjectButton instance;
    public MiniGameBanner miniGameBanner; // Tham chiếu đến MiniGameBanner

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (miniGameBanner == null)
        {
            miniGameBanner = Object.FindFirstObjectByType<MiniGameBanner>();
            Debug.LogError("MiniGameBanner reference is not set in ObjectButton.");
        }
    }


    private void OnMouseUpAsButton()
    {
        if (miniGameBanner != null)
        {
            miniGameBanner.ShowBanner(); // Gọi phương thức để hiển thị banner quảng cáo
        }
        else
        {
            Debug.LogError("MiniGameBanner reference is not set in ObjectButton.");
        }
    }

    void CloseBanner()
    {
               if (miniGameBanner != null && miniGameBanner.banner != null)
        {
            miniGameBanner.banner.SetActive(false); // Ẩn banner quảng cáo
            Debug.Log("Banner quảng cáo đã được ẩn.");
        }
    }
}

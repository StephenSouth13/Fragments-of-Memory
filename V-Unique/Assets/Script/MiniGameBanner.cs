using UnityEngine;

public class MiniGameBanner : MonoBehaviour
{
    public GameObject banner; // Tham chiếu đến banner quảng cáo
    public void ShowBanner()
    {
        if (banner != null)
        {
            banner.SetActive(true); // Hiển thị banner quảng cáo
            Debug.Log("Banner quảng cáo đã được hiển thị.");
        }
    }
}

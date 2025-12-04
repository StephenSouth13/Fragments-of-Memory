using UnityEngine;

public class CheckClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Bắn thử một tia 2D xem trúng cái gì
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Đang bấm trúng: " + hit.transform.name);
            }
            else
            {
                Debug.Log("Không bấm trúng gì cả (Có thể do UI chặn hoặc thiếu Collider)");
            }
        }
    }
}
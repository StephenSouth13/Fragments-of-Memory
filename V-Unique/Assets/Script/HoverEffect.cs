using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public Vector3 hoverOffset = new Vector3(-0.2f, 0f, 0f); // Chỉnh nhỏ lại chút cho đỡ lố
    public float movespeed = 10f;

    private Vector3 originalPosition;
    private bool isHovering = false;
    private Camera cam;

    // Biến toàn cục để bật tắt chức năng này từ script Zoom
    public static bool hoverEnabled = false;

    void Start()
    {
        originalPosition = transform.localPosition;
        cam = Camera.main; // Cache camera để tối ưu hiệu năng
    }

    void Update()
    {
        // Nếu chưa Zoom vào kệ sách thì không làm gì cả (tiết kiệm hiệu năng)
        if (!hoverEnabled) return;

        // Nếu object đã bị kéo đi xa (đang chơi xếp hình) thì không hover nữa
        if (Vector3.Distance(transform.localPosition, originalPosition) > 0.05f)
            return;

        CheckHover();
        MoveObject();
    }

    void CheckHover()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // --- TỐI ƯU RAYCAST ---
        // Chỉ bắn tia vào Layer chứa sách (để không bị nhân vật che mất)
        int layerMask = LayerMask.GetMask("Default", "Puzzle");

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask))
        {
            // Kiểm tra xem cái bị bắn trúng có phải là chính mình không
            isHovering = (hit.transform == transform);
        }
        else
        {
            isHovering = false;
        }
    }

    void MoveObject()
    {
        Vector3 target = isHovering ? originalPosition + hoverOffset : originalPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, movespeed * Time.deltaTime);
    }
}
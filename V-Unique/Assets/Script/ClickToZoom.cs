using UnityEngine;
using UnityEngine.AI; // <--- BẮT BUỘC CÓ để dùng NavMeshAgent

public class ClickToZoom : MonoBehaviour
{
    [Header("Cài đặt chung")]
    public Camera mainCamera;
    public float ZoomSpeed = 3f;
    public float triggerDistance = 2.0f;
    public float maxWaitTime = 2.0f; // Thời gian chờ tối đa nếu bị kẹt

    [Header("Nhân vật")]
    public Transform playerTransform;
    public SkinnedMeshRenderer[] characterParts;

    // Các biến nội bộ (Private)
    private Transform targetZoomPoint; // Điểm sẽ zoom tới
    private bool isZoomed = false;
    private bool isWaitingForPlayer = false;
    private float currentWaitTime = 0f; // Biến đếm giờ

    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    private void Start()
    {
        // Lưu vị trí camera ban đầu để lát nữa zoom out về
        if (mainCamera != null)
        {
            originalCameraPosition = mainCamera.transform.position;
            originalCameraRotation = mainCamera.transform.rotation;
        }
    }

    private void Update()
    {
        // 1. Xử lý Click chuột
        if (Input.GetMouseButtonDown(0) && !isZoomed)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Kiểm tra xem vật bị bấm có script ZoomTarget không
                ZoomTarget zoomTarget = hit.transform.GetComponent<ZoomTarget>();

                // TRƯỜNG HỢP 1: Bấm trúng đồ vật CÓ script ZoomTarget
                if (zoomTarget != null && zoomTarget.zoomPoint != null)
                {
                    targetZoomPoint = zoomTarget.zoomPoint;
                    isWaitingForPlayer = true;
                    currentWaitTime = 0f; // Reset đồng hồ đếm giờ

                    // Lệnh cho nhân vật đi tới đó
                    var agent = playerTransform.GetComponent<NavMeshAgent>();
                    if (agent != null) agent.SetDestination(hit.point);
                }
                // TRƯỜNG HỢP 2: Bấm trúng Sàn nhà (hoặc vật KHÔNG CÓ ZoomTarget)
                else
                {
                    // ---> HỦY LỆNH CHỜ CŨ NGAY LẬP TỨC <---
                    isWaitingForPlayer = false;
                    targetZoomPoint = null;
                    currentWaitTime = 0f;

                    // Code ClickToMove (nếu có) sẽ tự lo việc di chuyển, ở đây chỉ cần hủy zoom thôi
                }
            }
        }

        // 2. Logic Chờ đợi (Kiểm tra khoảng cách hoặc Time-out)
        if (isWaitingForPlayer && targetZoomPoint != null)
        {
            currentWaitTime += Time.deltaTime; // Đếm giờ
            float distance = Vector3.Distance(playerTransform.position, targetZoomPoint.position);
            var agent = playerTransform.GetComponent<NavMeshAgent>();

            // Điều kiện Zoom: Hoặc đến gần, hoặc chờ quá lâu, hoặc đứng im
            if (distance < triggerDistance)
            {
                StartZoomIn();
            }
            else if (currentWaitTime >= maxWaitTime) // Hết kiên nhẫn
            {
                StartZoomIn();
            }
            else if (agent != null && agent.velocity.sqrMagnitude == 0f && currentWaitTime > 0.5f) // Đứng im
            {
                StartZoomIn();
            }
        }

        // 3. Xử lý Thoát Zoom (Phím ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopZoomOut();
        }

        // 4. Thực hiện hiệu ứng Zoom (Lerp Camera)
        if (isZoomed && targetZoomPoint != null)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetZoomPoint.position, ZoomSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetZoomPoint.rotation, ZoomSpeed * Time.deltaTime);
        }
        else if (!isZoomed) // Trả về vị trí cũ
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalCameraPosition, ZoomSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, originalCameraRotation, ZoomSpeed * Time.deltaTime);
        }
    }

    // --- CÁC HÀM PHỤ (Nằm ngoài Update) ---

    void StartZoomIn()
    {
        isWaitingForPlayer = false;
        isZoomed = true;

        HoverEffect.hoverEnabled = true;

        // Tàng hình nhân vật
        foreach (var part in characterParts)
        {
            if (part != null) part.enabled = false;
        }
    }

    void StopZoomOut()
    {
        targetZoomPoint = null;
        isZoomed = false;
        isWaitingForPlayer = false;
        HoverEffect.hoverEnabled = false;

        // Hiện lại nhân vật
        foreach (var part in characterParts)
        {
            if (part != null) part.enabled = true;
        }
    }
}
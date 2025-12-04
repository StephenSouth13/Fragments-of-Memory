using UnityEngine;

public class ClickToZoom: MonoBehaviour
{
    public Camera mainCamera;
    public float ZoomSpeed = 3f;
    public float triggerDistance = 2.0f; //delay đợi nv di chuyển đến gần vật thể

    public Transform playerTransform; // Kéo nhân vật vào đây
    public SkinnedMeshRenderer[] characterParts; // Kéo cái Mesh (da) nhân vật vào đây để tàng hình

    private Transform target;
    private bool isZoomed = false;
    private bool isWaitingForPlayer = false;

    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private void Start()
    {
        originalCameraPosition = mainCamera.transform.position;
        originalCameraRotation = mainCamera.transform.rotation;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isZoomed)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ZoomTarget zoomTarget = hit.transform.GetComponent<ZoomTarget>();
                if (zoomTarget != null && zoomTarget.zoomPoint != null)
                {
                    target = zoomTarget.zoomPoint;
                    isWaitingForPlayer = true;
                }
            }
        }

        if (isWaitingForPlayer && target != null)
        {
            float distance = Vector3.Distance(playerTransform.position, target.position);

            Debug.Log("Khoảng cách: " + distance); 

            if (distance < triggerDistance)
            {
                StartZoomIn();
            }
            else if (playerTransform.GetComponent<UnityEngine.AI.NavMeshAgent>().velocity.sqrMagnitude < 0.1f
                     && distance < triggerDistance + 2.0f)
            {
                StartZoomIn();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopZoomOut();
        }

        if (isZoomed && target != null)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, target.position, ZoomSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, target.rotation, ZoomSpeed * Time.deltaTime);
        }
        else if (!isZoomed)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, originalCameraPosition, ZoomSpeed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, originalCameraRotation, ZoomSpeed * Time.deltaTime);
        }
    }

    void StartZoomIn()
    {
        isWaitingForPlayer = false; // Ngừng chờ
        isZoomed = true;
        HoverEffect.hoverEnabled = false;

        // Tàng hình nhân vật để không bị che tủ sách
        foreach (var part in characterParts)
        {
            if (part != null) part.enabled = false;
        }
    }

    void StopZoomOut()
    {
        target = null;
        isZoomed = false;
        isWaitingForPlayer = false;
        HoverEffect.hoverEnabled = true;

        // Hiện lại nhân vật
        foreach (var part in characterParts)
        {
            if (part != null) part.enabled = true;
        }
    }
}

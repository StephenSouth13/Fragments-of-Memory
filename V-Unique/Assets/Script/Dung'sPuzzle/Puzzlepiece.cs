using UnityEngine;

public class Puzzlepiece : MonoBehaviour
{
    private Vector3 correctPosition; // Vị trí đúng (Trạng thái thắng)
    private bool isLocked = false;
    private bool isDragging = false;
    private Vector3 offset;

    public float snapDistance = 0.5f;

    void Awake()
    {
 
        correctPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (isLocked) return;


        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isLocked) return;
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        if (isLocked) return;
        isDragging = false;

        // Check khoảng cách
        if (Vector3.Distance(transform.position, correctPosition) < snapDistance)
        {
            transform.position = correctPosition; // Hút vào
            isLocked = true;
            GetComponent<Collider2D>().enabled = false;

            // Gọi Manager
            Puzzlemanager.Instance.CheckWinCondition();
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0; // Game 2D thì Z thường là 0, hoặc dùng Camera.main.nearClipPlane
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
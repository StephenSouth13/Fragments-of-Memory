using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    // Các biến công khai (được gán bởi PuzzleManager)
    public Vector3 correctPosition;
    public bool isPlaced = false;

    // Phạm vi "hít" (Snap Range) - Có thể điều chỉnh trong Editor nếu cần
    private float snapRange = 5.0f; 

    // Biến lưu trữ sự bù trừ (offset) để kéo thả mượt mà
    private Vector3 offset; 
    
    // Lưu trữ khoảng cách Z cố định từ Camera đến vật thể (Fix lỗi kéo thả 2D trong 3D)
    private float zDepth; 

    private PuzzleManager puzzleManager;

    void Start()
    {
        // Tìm và lưu tham chiếu đến PuzzleManager
        puzzleManager = FindAnyObjectByType<PuzzleManager>(); 
        
        if (puzzleManager == null)
        {
            Debug.LogError("Error: PuzzleManager object not found in the scene.");
        }
    }

    // Xử lý khi người chơi bắt đầu kéo (chuột xuống)
    private void OnMouseDown()
    {
        // 1. Tính toán Offset (Đảm bảo điểm click không phải là tâm mảnh ghép)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePosition; 
        
        // 2. Tính toán Z-DEPTH (Khoảng cách Z từ Camera đến vật thể)
        zDepth = transform.position.z - Camera.main.transform.position.z; 
        
        Debug.Log("Piece Clicked! Now dragging..."); 
        
        isPlaced = false;
        // Đặt độ sâu Z về -1f để đảm bảo mảnh ghép đang kéo luôn nằm trên
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
    }

    // Xử lý khi người chơi đang kéo (chuột di chuyển)
    private void OnMouseDrag()
    {
        // Lấy vị trí chuột trên màn hình
        Vector3 screenPoint = Input.mousePosition;
        
        // BẮT BUỘC: Gán Z-depth cố định vào vị trí chuột để fix lỗi 2D trong 3D
        screenPoint.z = zDepth; 

        // Chuyển đổi sang tọa độ thế giới (World Point)
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        
        // Cập nhật vị trí mảnh ghép + offset
        transform.position = new Vector3(worldPoint.x + offset.x, worldPoint.y + offset.y, transform.position.z);
    }

    // Xử lý khi người chơi thả (chuột lên)
    private void OnMouseUp()
    {
        // Đặt lại Z về 0f
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        bool snapped = false;

        // 1. KIỂM TRA SNAP VÀO VỊ TRÍ TUYỆT ĐỐI CỦA LƯỚI
        if (Vector3.Distance(transform.position, correctPosition) < snapRange)
        {
            transform.position = correctPosition;
            snapped = true;
        } 
        
        // 2. KIỂM TRA SNAP VÀO CÁC MẢNH GHÉP LÂN CẬN (PEER-TO-PEER SNAP)
        if (!snapped)
        {
            // Tìm tất cả các mảnh ghép khác trong scene
            PuzzlePiece[] allOtherPieces = FindObjectsByType<PuzzlePiece>(FindObjectsSortMode.None);
            
            foreach (PuzzlePiece otherPiece in allOtherPieces)
            {
                // Bỏ qua chính mảnh ghép này và các mảnh chưa được đặt đúng vị trí (chưa isPlaced=true)
                if (otherPiece == this || !otherPiece.isPlaced)
                    continue;
                
                // Kiểm tra khoảng cách so với vị trí đúng (đã được snap) của mảnh ghép lân cận
                if (Vector3.Distance(transform.position, otherPiece.correctPosition) < snapRange)
                {
                    // Snap mảnh ghép hiện tại vào vị trí đúng của mảnh ghép lân cận
                    transform.position = otherPiece.correctPosition;
                    snapped = true;
                    break; // Thoát khỏi vòng lặp sau khi đã snap thành công
                }
            }
        }

        // --- CẬP NHẬT TRẠNG THÁI VÀ KẾT THÚC ---
        if (snapped)
        {
            isPlaced = true;
            Debug.Log("Piece Snapped! Checking for completion...");
            
            if (puzzleManager != null)
            {
                 puzzleManager.CheckCompletion();
            }
        }
        else
        {
             Debug.Log("Piece dropped, not in the correct spot.");
        }
    }
}
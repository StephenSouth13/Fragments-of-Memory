using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    // CẦN GÁN: Prefab mảnh ghép
    public GameObject piecePrefab;
    // CẦN GÁN: Mảng chứa 9 mảnh ghép (Sprites)
    public Sprite[] allPuzzlePieces; 
    
    // CẦN GÁN: Kéo một trong 9 ô Hint Cố định từ Hierarchy vào đây để lấy kích thước
    // Sẽ là SpriteRenderer của một ô Khuôn đã tạo sẵn
    public SpriteRenderer hintReference; 
    
    public int cols = 3;
    public int rows = 3;

    private List<PuzzlePiece> allPieces = new List<PuzzlePiece>();
    private float pieceWidth;
    private float pieceHeight;
    // Phạm vi X, Y ngẫu nhiên cho vị trí ban đầu
    private float scatterRange = 3.0f; 

    void Start()
    {
        GeneratePuzzle();
    }

    void GeneratePuzzle()
    {
        if (allPuzzlePieces == null || allPuzzlePieces.Length == 0)
        {
            Debug.LogError("Lỗi: Mảng 'All Puzzle Pieces' trống!");
            return;
        }
        
        // ✅ BƯỚC MỚI: KIỂM TRA VÀ LẤY KÍCH THƯỚC TỪ KHUÔN ĐÃ TẠO SẴN
        if (hintReference == null)
        {
            Debug.LogError("Lỗi: Cần gán 'Hint Reference' (Sprite Renderer của một ô Khuôn) trong Inspector!");
            return;
        }
        
        // Lấy kích thước mảnh ghép dựa trên kích thước của ô Hint mẫu đã tạo trong Scene
        pieceWidth = hintReference.bounds.size.x;
        pieceHeight = hintReference.bounds.size.y;
        
        Debug.Log($"Kích thước mảnh ghép (được lấy từ Khuôn): {pieceWidth}x{pieceHeight}");

        // Tính toán tổng kích thước lưới và điều chỉnh vị trí PuzzleManager về trung tâm
        float totalWidth = cols * pieceWidth;
        float totalHeight = rows * pieceHeight;
        
        // Vị trí (0,0) của PuzzleManager sẽ là tâm của toàn bộ lưới Hint
        transform.position = new Vector3(-totalWidth / 2 + pieceWidth / 2, -totalHeight / 2 + pieceHeight / 2, 0);

        // Trộn ngẫu nhiên danh sách các mảnh ghép
        Sprite[] pieceSpritesToShuffle = allPuzzlePieces.OrderBy(x => Random.value).ToArray();

        int pieceIndex = 0;
        
        // *** KHÔNG CẦN TẠO HINT NỮA VÌ CHÚNG TA ĐÃ TẠO NÓ TRONG SCENE ***
        
        // --- BƯỚC TẠO VÀ XÁO TRỘN CÁC MẢNH GHÉP ---
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (pieceIndex >= pieceSpritesToShuffle.Length) continue;

                // Vị trí ĐÚNG (Correct Position) - trùng với tâm của từng ô Khuôn
                float correctX = c * pieceWidth;
                float correctY = r * pieceHeight;
                Vector3 correctPos = new Vector3(correctX, correctY, 0);

                // Tạo đối tượng mảnh ghép
                GameObject newPieceGO = Instantiate(piecePrefab, transform);
                
                // Gán Sprite và vị trí ĐÚNG
                SpriteRenderer sr = newPieceGO.GetComponent<SpriteRenderer>();
                sr.sprite = pieceSpritesToShuffle[pieceIndex];
                
                PuzzlePiece pieceScript = newPieceGO.GetComponent<PuzzlePiece>();
                pieceScript.correctPosition = correctPos;
                allPieces.Add(pieceScript);

                // Đặt vị trí BAN ĐẦU (Ngẫu nhiên gần trung tâm hơn)
                float startX = Random.Range(-scatterRange, scatterRange); 
                float startY = Random.Range(-scatterRange, scatterRange); 
                newPieceGO.transform.position = new Vector3(startX, startY, 0);

                pieceIndex++;
            }
        }
    }

    public void CheckCompletion()
    {
        int piecesInPlace = 0;
        foreach (var piece in allPieces)
        {
            if (piece.isPlaced)
            {
                piecesInPlace++;
            }
        }

        if (piecesInPlace == allPieces.Count)
        {
            Debug.Log("PUZZLE COMPLETED! CONGRATULATIONS!");
            // Kích hoạt màn hình Win Screen Panel tại đây (nếu đã gán)
            // if (winScreenPanel != null) winScreenPanel.SetActive(true); 
        }
    }
}
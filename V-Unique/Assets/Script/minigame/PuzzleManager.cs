using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    // CẦN GÁN: Prefab mảnh ghép
    public GameObject piecePrefab;
    // CẦN GÁN: Mảng chứa 9 mảnh ghép (Sprites)
    public Sprite[] allPuzzlePieces; 
    // CẦN GÁN: Prefab cho ô gợi ý (Tạo ở bước 3)
    public GameObject hintPrefab; 
    
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

        // Lấy kích thước mảnh ghép từ Sprite đầu tiên
        pieceWidth = allPuzzlePieces[0].bounds.size.x;
        pieceHeight = allPuzzlePieces[0].bounds.size.y;

        // Tính toán tổng kích thước lưới và điều chỉnh vị trí PuzzleManager về trung tâm
        float totalWidth = cols * pieceWidth;
        float totalHeight = rows * pieceHeight;
        transform.position = new Vector3(-totalWidth / 2 + pieceWidth / 2, -totalHeight / 2 + pieceHeight / 2, 0);

        // Trộn ngẫu nhiên danh sách các mảnh ghép
        Sprite[] pieceSpritesToShuffle = allPuzzlePieces.OrderBy(x => Random.value).ToArray();

        int pieceIndex = 0;
        
        // --- BƯỚC 1: TẠO LƯỚI GỢI Ý (Hint Grid) ---
        if (hintPrefab != null)
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    float hintX = c * pieceWidth;
                    float hintY = r * pieceHeight;

                    // Tạo hint, đặt Z cao hơn nền nhưng thấp hơn mảnh ghép
                    GameObject hintGO = Instantiate(hintPrefab, transform);
                    hintGO.transform.position = new Vector3(hintX, hintY, 0.5f); 
                    
                    // Thiết lập kích thước hint khớp với mảnh ghép
                    hintGO.transform.localScale = new Vector3(pieceWidth, pieceHeight, 1);
                }
            }
        }
        
        // --- BƯỚC 2: TẠO VÀ XÁO TRỘN CÁC MẢNH GHÉP ---
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (pieceIndex >= pieceSpritesToShuffle.Length) continue;

                // Vị trí ĐÚNG (Correct Position)
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
        // ... (Hàm này giữ nguyên)
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
        }
    }
}
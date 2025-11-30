using UnityEngine;
using TMPro;

public class ZenWordObject : MonoBehaviour
{
    // CẦN GÁN TRONG PREFAB: Component TextMeshPro
    public TextMeshPro textDisplay; 
    
    // Tùy chỉnh
    [SerializeField] private float flowSpeed = 0.6f; // Tốc độ trôi nhẹ nhàng
    
    // Thuộc tính
    private string word;
    private int typeIndex = 0;
    
    // Hàm khởi tạo, được gọi bởi Manager khi sinh ra
    public void SetWord(string newWord)
    {
        word = newWord.ToUpper();
        textDisplay.text = word;
        typeIndex = 0;
    }

    // Trả về chữ cái tiếp theo cần gõ
    public char GetNextLetter()
    {
        if (typeIndex < word.Length)
        {
            return word[typeIndex];
        }
        return (char)0; 
    }

    // Được gọi bởi Manager khi người chơi gõ đúng
    public void TypeLetter()
    {
        typeIndex++;
        
        // Cập nhật hiển thị: Tô màu phần đã gõ (Hiệu ứng Zen)
        string typedPart = "<color=#80CBC4>" + word.Substring(0, typeIndex) + "</color>";
        string remainingPart = word.Substring(typeIndex);
        textDisplay.text = typedPart + remainingPart;
        
        // TODO: Phát âm thanh 'Pop' nhẹ nhàng khi gõ đúng phím
        
        // Kiểm tra hoàn thành
        if (typeIndex >= word.Length)
        {
            WordCompleted();
        }
    }

    void WordCompleted()
    {
        // TODO: Kích hoạt hiệu ứng hạt lấp lánh (Harvest Effect)
        
        Debug.Log($"[Zen Typing] Đã thu hoạch từ: {word}");
        
        // Thông báo cho Manager và xóa đối tượng
        ZenTypingManager.Instance.NotifyWordHarvested(this);
        Destroy(gameObject); 
    }
    
    void Update()
    {
        // VỊ TRÍ Z ĐÃ ĐƯỢC CỐ ĐỊNH = 0 KHI SINH RA.
        // Di chuyển từ trôi chậm rãi từ phải sang trái
        transform.Translate(Vector3.left * Time.deltaTime * flowSpeed); 
        
        // Tự hủy nếu trôi ra khỏi màn hình
        if (transform.position.x < -15f) 
        {
            ZenTypingManager.Instance.NotifyWordLost(this);
            Destroy(gameObject);
        }
    }
}
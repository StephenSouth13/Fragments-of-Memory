using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreathingEffect : MonoBehaviour
{
    public TextMeshProUGUI textTarget; 
    public float speed = 2.0f;         // Tốc độ nhấp nháy
    public float minAlpha = 0.3f;      // Độ mờ tối đa 
    public float maxAlpha = 1.0f;      // Độ rõ tối đa

    void Start()
    {
        if (textTarget == null)
        {
            textTarget = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (textTarget != null)
        {
            // Dùng hàm Sin để tạo dao động nhịp nhàng từ -1 đến 1
            float wave = Mathf.Sin(Time.time * speed);

            // Chuyển đổi dao động đó sang khoảng từ 0 đến 1
            float normalizedWave = (wave + 1f) / 2f;

            // Tính toán độ mờ  hiện tại
            float currentAlpha = Mathf.Lerp(minAlpha, maxAlpha, normalizedWave);

            // Gán lại màu cho chữ (giữ nguyên màu RGB, chỉ thay đổi Alpha)
            Color currentColor = textTarget.color;
            currentColor.a = currentAlpha;
            textTarget.color = currentColor;
        }
    }
}
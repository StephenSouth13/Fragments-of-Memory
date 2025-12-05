using UnityEngine;
using System.Collections;

public class UIPopupEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duration = 0.3f;

    public bool useScaleEffect = true;

    public void Show()
    {
        gameObject.SetActive(true); // Đảm bảo object đang bật
        StartCoroutine(Fade(0, 1)); // Fade từ 0 lên 1
    }

    public void Hide()
    {
        StartCoroutine(Fade(1, 0)); // Fade từ 1 về 0
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;

        // Nếu hiện lên -> Bật tương tác
        if (endAlpha == 1)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        // Nếu ẩn đi -> Tắt tương tác ngay lập tức
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime; // Dùng unscaled để vẫn chạy khi game Pause
            float progress = timer / duration;

            // 1. Xử lý độ mờ
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

            // 2. Xử lý phóng to/thu nhỏ (Hiệu ứng nảy)
            if (useScaleEffect)
            {
                // Nếu đang hiện: Scale từ 0.8 lên 1
                if (endAlpha == 1)
                    transform.localScale = Vector3.Lerp(Vector3.one * 0.8f, Vector3.one, progress);
                // Nếu đang ẩn: Scale từ 1 về 0.8
                else
                    transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.8f, progress);
            }

            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        // Nếu ẩn xong thì tắt hẳn GameObject cho nhẹ
        if (endAlpha == 0) gameObject.SetActive(false);
    }
}
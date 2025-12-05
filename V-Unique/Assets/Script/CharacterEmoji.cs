using UnityEngine;
using System.Collections;

public class CharacterEmoji : MonoBehaviour
{
    [Header("Cài đặt Tài nguyên")]
    public SpriteRenderer emojiDisplay;
    public Sprite[] emojiList;          

    [Header("Cài đặt Hiệu ứng")]
    public float showTime = 2f;         // Thời gian hiện (giây)
    public float popSpeed = 5f;         // Tốc độ nảy
    public float floatSpeed = 0.5f;     // Tốc độ bay lên nhẹ
    public Vector3 offset = new Vector3(0, 2.2f, 0); // Vị trí so với chân nhân vật

    private Coroutine currentRoutine;
    private Vector3 initialScale;
    private Vector3 initialPos;

    void Start()
    {
        if (emojiDisplay != null)
        {
            // Lưu lại vị trí gốc để mỗi lần hiện thì reset về đây
            initialPos = emojiDisplay.transform.localPosition;
            initialScale = Vector3.one; // Hoặc kích thước bạn muốn (VD: 0.5, 0.5, 0.5)

            // Ẩn đi lúc đầu
            emojiDisplay.gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        // Khi click vào người nhân vật
        ShowRandomEmoji();
    }

    public void ShowRandomEmoji()
    {
        if (emojiList.Length == 0 || emojiDisplay == null) return;

        //Chọn ngẫu nhiên
        int index = Random.Range(0, emojiList.Length);
        emojiDisplay.sprite = emojiList[index];

        //Reset trạng thái (nếu đang chạy cái cũ thì dừng lại làm cái mới)
        if (currentRoutine != null) StopCoroutine(currentRoutine);

        emojiDisplay.gameObject.SetActive(true);
        emojiDisplay.transform.localPosition = initialPos;
        emojiDisplay.transform.localScale = Vector3.zero; // Bắt đầu từ số 0 (thu nhỏ)

        //Chạy hiệu ứng
        currentRoutine = StartCoroutine(AnimatePopUp());
    }

    IEnumerator AnimatePopUp()
    {
        float timer = 0f;

        //POP 
        while (timer < 1f)
        {
            timer += Time.deltaTime * popSpeed;
            // Hiệu ứng Elastic (nảy nảy) dùng AnimationCurve hoặc Lerp đơn giản
            float scale = Mathf.Lerp(0f, 1f, timer);
            emojiDisplay.transform.localScale = Vector3.one * scale; // Giả sử scale chuẩn là 1
            yield return null;
        }
        emojiDisplay.transform.localScale = Vector3.one;

        // FLOAT 
        float waitTimer = 0f;
        while (waitTimer < showTime)
        {
            waitTimer += Time.deltaTime;

            
            emojiDisplay.transform.localPosition += Vector3.up * floatSpeed * Time.deltaTime;

            emojiDisplay.transform.rotation = Camera.main.transform.rotation;

            yield return null;
        }


        emojiDisplay.gameObject.SetActive(false);
    }
    void LateUpdate()
    {
        if (emojiDisplay.gameObject.activeInHierarchy)
        {
            emojiDisplay.transform.rotation = Camera.main.transform.rotation;
        }
    }
}
using System.Collections;
using UnityEngine;

public class colorLerp : MonoBehaviour
{
    public static colorLerp Instance;

    public Material[] materials;          // 0 = xám, 1 = có màu
    public float fadeDuration = 2f;       // thời gian fade khi hoàn thành minigame

    public GameObject familyPic;

    private Renderer rend;
    private int index = 0;

    private bool autoChanged = false;
    private bool isFading = false;
    private float fadeTimer = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (materials == null || materials.Length < 2)
        {
            Debug.LogError("Cần ít nhất 2 Material (xám và có màu)!");
            enabled = false;
            return;
        }

        rend.material = materials[0]; // bắt đầu bằng màu xám

        familyPic.SetActive(false);
    }

    void Update()
    {
        AutoFadeColor();   // hiệu ứng fade khi xong 4 minigame
        Press01Change();   // bấm phím 1 đổi màu ngay lập tức
        Press02ClosePanel(); // bấm phím 2 đóng bảng hình gia đình
    }


    // =============================
    // ⭐ AUTO FADE KHI ĐỦ 4 MINIGAME
    // =============================
    void AutoFadeColor()
    {
        if (MinigameProgress.AllFinished() && !autoChanged)
        {
            // bắt đầu fade
            isFading = true;
            fadeTimer = 0f;
            autoChanged = true;
        }

        if (isFading)
        {
            fadeTimer += Time.deltaTime;

            float t = fadeTimer / fadeDuration;

            // blend màu
            Color startColor = materials[0].color;
            Color targetColor = materials[1].color;
            Color lerped = Color.Lerp(startColor, targetColor, t);

            rend.material.color = lerped;

            if (t >= 1f)
            {
                isFading = false;
                rend.material = materials[1]; // khóa lại, chuyển sang material hoàn chỉnh

                //StartCoroutine(ShowFamilyPic());
                familyPic.SetActive(true);
            }

           
        }
    }


    // =============================
    // ⭐ BẤM PHÍM 1 — ĐỔI LẬP TỨC (KHÔNG FADE)
    // =============================
    void Press01Change()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            index = (index + 1) % materials.Length;
            rend.material = materials[index];

            //StartCoroutine(ShowFamilyPic());
            familyPic.SetActive(true);
        }       
    }

    void Press02ClosePanel()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            familyPic.SetActive(false);
        }
    }    

    //IEnumerator ShowFamilyPic()
    //{
    //    yield return new WaitForSeconds(3f);

    //    if (familyPic != null)
    //        familyPic.SetActive(true);
    //}

}


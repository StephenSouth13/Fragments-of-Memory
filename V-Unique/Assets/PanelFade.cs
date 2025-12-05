using UnityEngine;

public class PanelFade : MonoBehaviour
{
    public CanvasGroup panelGroup;
    public float fadeTime = 1.5f;

    private float timer = 0f;
    private bool fading = false;

    void Start()
    {
        if (panelGroup == null)
            panelGroup = GetComponent<CanvasGroup>();

        panelGroup.alpha = 0f;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;
    }

    void Update()
    {
        if (!fading) return;

        timer += Time.deltaTime;
        float t = timer / fadeTime;

        panelGroup.alpha = Mathf.Lerp(0f, 1f, t);

        if (t >= 1f)
        {
            fading = false;
            panelGroup.interactable = true;
            panelGroup.blocksRaycasts = true;
        }
    }

    public void ShowPanel()
    {
        Debug.Log("Shơ panel");
        fading = true;
        timer = 0f;
    }
}

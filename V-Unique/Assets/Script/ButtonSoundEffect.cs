using UnityEngine;
using UnityEngine.EventSystems; 

public class ButtonSoundEffect : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Cài đặt âm thanh")]
    public AudioClip hoverSound;    
    public AudioClip clickSound;    

    //chuột RÊ VÀO nút
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (AudioManager.Instance != null && hoverSound != null)
        {
            AudioManager.Instance.PlaySFX(hoverSound);
        }
    }

    //chuột BẤM VÀO nút
    public void OnPointerClick(PointerEventData eventData)
    {
        if (AudioManager.Instance != null && clickSound != null)
        {
            AudioManager.Instance.PlaySFX(clickSound);
        }
    }
}
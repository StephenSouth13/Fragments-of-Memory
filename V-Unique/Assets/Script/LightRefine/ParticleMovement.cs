using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveSpeed = 5f; 
    
    public bool isGoodParticle = true;
    private RefinementManager manager; // THAM CHIẾU MỚI: Liên kết với Manager

    // ✅ HÀM MỚI: Nhận điểm đích VÀ Manager khi được sinh ra
    public void SetTargetAndManager(Vector3 target, RefinementManager mgr)
    {
        targetPosition = target;
        manager = mgr;
    }

    void Update()
    {
        // Di chuyển về phía đích
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        
        // ✅ KIỂM TRA ĐẠT ĐÍCH (Tính điểm và Biến mất)
        // Nếu khoảng cách đến đích nhỏ hơn 0.1f (gần như chạm)
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if(manager != null)
            {
                // Báo cáo điểm cho Manager
                manager.ParticleScored(isGoodParticle); 
            }
            // ✅ TỰ HỦY: Hạt biến mất ngay lập tức
            Destroy(gameObject); 
        }
    }
}
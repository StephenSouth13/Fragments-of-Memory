using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveSpeed = 5f; 
    
    public bool isGoodParticle = true; // Cần thiết lập trên Prefab

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    void Update()
    {
        // Tạo dòng chảy đơn giản: Di chuyển từ trái sang phải (hoặc theo hướng bạn muốn)
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        
        // Tùy chọn: Nếu hạt đi quá xa, tự hủy
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class GateController : MonoBehaviour
{
    // CẦN GÁN TRONG INSPECTOR:
    // Đảm bảo bạn đã tạo 1 đối tượng trống tên là '_DeflectPoint' và gán vào đây
    public Transform deflectPoint; 
    
    // Trạng thái hiện tại của Cổng Lọc
    // True: Cổng đang ở trạng thái ưu tiên hạt TỐT đi qua (deflect hạt XẤU)
    // False: Cổng đang ở trạng thái ưu tiên hạt XẤU đi qua (deflect hạt TỐT)
    private bool isGoodPathOpen = true; 
    
    // Material để đổi màu Cổng Lọc khi chuyển trạng thái (Tùy chọn)
    public Material goodStateMat;
    public Material badStateMat;
    private Renderer gateRenderer;
    
    void Start()
    {
        // Khởi tạo Material và trạng thái
        gateRenderer = GetComponent<Renderer>();
        if (gateRenderer != null && goodStateMat != null)
        {
            gateRenderer.material = goodStateMat;
        }
    }

    void Update()
    {
        // ✅ LOGIC TƯƠNG TÁC: Nhấn chuột (hoặc giữ phím) để chuyển đổi trạng thái
        if (Input.GetMouseButtonDown(0)) 
        {
            ToggleFilterState();
        }
    }

    void ToggleFilterState()
    {
        isGoodPathOpen = !isGoodPathOpen; // Đảo ngược trạng thái
        
        Debug.Log("Gate State Toggled: Good Path Open = " + isGoodPathOpen);
        
        // Thay đổi màu sắc Cổng Lọc để người chơi dễ nhận biết
        if (gateRenderer != null)
        {
            if (isGoodPathOpen && goodStateMat != null)
            {
                gateRenderer.material = goodStateMat;
            }
            else if (!isGoodPathOpen && badStateMat != null)
            {
                gateRenderer.material = badStateMat;
            }
        }
        
        // TODO: Phát âm thanh "click" hoặc "swoosh" khi chuyển trạng thái
    }

    // ✅ LOGIC VA CHẠM (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        ParticleMovement particle = other.GetComponent<ParticleMovement>();
        
        if (particle == null) return;

        bool shouldDeflect = false;

        // Tình huống 1: Đang ưu tiên hạt TỐT đi qua (isGoodPathOpen = True)
        if (isGoodPathOpen)
        {
            // Nếu hạt va chạm là hạt XẤU -> Cần chuyển hướng!
            if (!particle.isGoodParticle)
            {
                shouldDeflect = true;
            }
        }
        // Tình huống 2: Đang ưu tiên hạt XẤU đi qua (isGoodPathOpen = False)
        else 
        {
            // Nếu hạt va chạm là hạt TỐT -> Cần chuyển hướng!
            if (particle.isGoodParticle)
            {
                shouldDeflect = true;
            }
        }

        // Thực hiện chuyển hướng nếu cần
        if (shouldDeflect && deflectPoint != null)
        {
            // Thay đổi điểm đích của hạt về DeflectPoint (vùng hủy)
            particle.SetTargetAndManager(deflectPoint.position, null); 
            // NOTE: Truyền 'null' cho Manager để hạt không tính điểm khi bị hủy
        }
    }
}
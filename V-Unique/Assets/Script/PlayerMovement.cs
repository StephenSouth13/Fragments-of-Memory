using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Tốc độ chạy
    public Animator animator; // Link tới cái Animator

    void Update()
    {
        // 1. Nhận nút bấm từ bàn phím (WASD hoặc Mũi tên)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 2. Tạo vector hướng đi
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // 3. Nếu có bấm nút (độ dài vector > 0)
        if (movement.magnitude > 0)
        {
            // Di chuyển nhân vật
            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            // Xoay mặt nhân vật theo hướng đi (cho tự nhiên)
            transform.rotation = Quaternion.LookRotation(movement);

            // Bật animation Đi bộ
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // Tắt animation Đi bộ (về Idle)
            animator.SetBool("IsWalking", false);
        }
    }
}
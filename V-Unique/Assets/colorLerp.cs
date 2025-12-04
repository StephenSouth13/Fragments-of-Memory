using UnityEngine;

public class colorLerp : MonoBehaviour
{
    public Material[] materials;
    private int index = 0;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (materials == null || materials.Length == 0)
        {
            Debug.LogError("Bạn chưa kéo Material vào mảng materials!");
            enabled = false;
            return;
        }

        rend.material = materials[0];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            index = (index + 1) % materials.Length;
            rend.material = materials[index];
        }
    }
}

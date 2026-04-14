using UnityEngine;

public class FPSLook : MonoBehaviour
{
    public Transform fpsCamera;
    public float sensitivity = 2f;

    private float xRotation = 0f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        xRotation = 0f;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Quaternion deltaRot = Quaternion.Euler(0f, mouseX, 0f);
        rb.MoveRotation(rb.rotation * deltaRot);
    }
}

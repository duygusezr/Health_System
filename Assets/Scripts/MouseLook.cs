using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Karakterin ana govdesi

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Fareyi ekranda kilitle
    }

    void Update()
    {
        // Mouse hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yukari-asagi hareket (kamera rotasyonu)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Dik aci siniri

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Sola-saga hareket (karakter rotasyonu)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
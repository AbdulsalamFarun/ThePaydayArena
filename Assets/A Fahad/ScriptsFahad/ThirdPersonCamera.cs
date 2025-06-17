using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    [Header("Zoom")]
    public float distance = 6f;
    public float minDistance = 1f;
    public float maxDistance = 3f;
    public float zoomSpeed = 10f;

    [Header("Look")]
    public float mouseSensitivity = 3f;
    public float minY = 5f;
    public float maxY = 80f;

    private float yaw = 0f;
    private float pitch = 20f;

    private Vector2 lookInput;
    private float zoomInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Look rotation
        yaw += lookInput.x * mouseSensitivity;
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        // Zoom
        distance -= zoomInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Reset zoom input so it only applies one frame
        zoomInput = 0;

        // Camera position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 direction = rotation * Vector3.back * distance;
        Vector3 cameraPosition = target.position + direction;

        transform.position = cameraPosition;
        transform.LookAt(target.position);
    }

    // Callbacks from Input System
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        zoomInput = context.ReadValue<Vector2>().y; // scroll is usually Vector2 (0, scrollY)
    }
}

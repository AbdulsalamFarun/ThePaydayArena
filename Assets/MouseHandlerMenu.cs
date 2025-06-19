using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHandlerMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
